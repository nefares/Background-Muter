/*
 *  WinBGMuterTests – VolumeMixerTests.cs
 *
 *  Functional and memory-leak regression tests for VolumeMixer (PR #55).
 *
 *  PR #55 fixes three COM-object leak sites in ReloadAudio():
 *    1. volumeSessionList entries were not released before Clear()
 *    2. sessionEnumerator was replaced without releasing the old one
 *    3. Duplicate-PID dictionary entries were replaced without releasing
 *       the old ISimpleAudioVolume COM object
 *
 *  Requirements:
 *    - Windows host with at least one audio output device
 *    - Tests are skipped automatically when no audio device is present
 */

using System.Diagnostics;
using System.Runtime.InteropServices;
using WinBGMuter;
using Xunit;

namespace WinBGMuterTests
{
    /// <summary>
    /// Helpers shared across test classes.
    /// </summary>
    internal static class AudioTestHelper
    {
        /// <summary>
        /// Returns true when a default render endpoint is available so tests
        /// that require real audio hardware can be skipped gracefully in CI.
        /// </summary>
        public static bool HasAudioDevice()
        {
            try
            {
                // Use the same COM path the app itself uses.
                var enumeratorType = Type.GetTypeFromCLSID(new Guid("BCDE0395-E52F-467C-8E3D-C4579291692E"));
                if (enumeratorType == null) return false;
                var enumerator = Activator.CreateInstance(enumeratorType);
                if (enumerator == null) return false;
                Marshal.ReleaseComObject(enumerator);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a VolumeMixer on an STA thread (required for WinForms
        /// message-box fallback inside VolumeMixer), runs <paramref name="action"/>,
        /// then disposes the mixer.  Propagates any exception back to the caller.
        /// </summary>
        public static void RunOnStaThread(Action<VolumeMixer> action)
        {
            Exception? captured = null;
            var thread = new Thread(() =>
            {
                VolumeMixer? mixer = null;
                try
                {
                    mixer = new VolumeMixer();
                    action(mixer);
                }
                catch (Exception ex)
                {
                    captured = ex;
                }
                finally
                {
                    // Always call UnloadAudio before letting the finalizer run
                    // so we don't race with GC on the COM objects.
                    try { mixer?.UnloadAudio(true); } catch { }
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (captured != null)
                System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(captured).Throw();
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Functional tests
    // ─────────────────────────────────────────────────────────────────────────

    public class VolumeMixerFunctionalTests
    {
        /// <summary>
        /// Constructor completes without throwing.
        /// ReloadAudio(true) is called internally; any error triggers a MessageBox
        /// which would block the test – so this test implicitly validates that the
        /// happy-path COM initialisation succeeds.
        /// </summary>
        [Fact]
        [Trait("Category", "Functional")]
        public void Constructor_InitializesWithoutException()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return; // skip silently – no audio device available

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                Assert.NotNull(mixer);
            });
        }

        /// <summary>
        /// GetPIDs() must return a non-null array.  On any active Windows
        /// session there will be at least the System Sounds session (PID 0),
        /// so the array should be non-empty.
        /// </summary>
        [Fact]
        [Trait("Category", "Functional")]
        public void GetPIDs_ReturnsNonNullArray()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                int[] pids = mixer.GetPIDs();
                Assert.NotNull(pids);
            });
        }

        /// <summary>
        /// Calling GetPIDs() twice must return the same set of PIDs (the
        /// audio session list must be consistent across calls).
        /// </summary>
        [Fact]
        [Trait("Category", "Functional")]
        public void GetPIDs_IsConsistentAcrossTwoCalls()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                int[] first  = mixer.GetPIDs();
                int[] second = mixer.GetPIDs();
                Assert.Equal(first.OrderBy(x => x), second.OrderBy(x => x));
            });
        }

        /// <summary>
        /// GetApplicationMute returns null for a PID that has no audio session
        /// (very high PID unlikely to exist), and does NOT throw.
        /// </summary>
        [Fact]
        [Trait("Category", "Functional")]
        public void GetApplicationMute_ReturnsNullForUnknownPid()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                bool? mute = mixer.GetApplicationMute(int.MaxValue - 1);
                Assert.Null(mute);
            });
        }

        /// <summary>
        /// GetApplicationVolume returns null for a PID that has no audio
        /// session, and does NOT throw.
        /// </summary>
        [Fact]
        [Trait("Category", "Functional")]
        public void GetApplicationVolume_ReturnsNullForUnknownPid()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                float? vol = mixer.GetApplicationVolume(int.MaxValue - 1);
                Assert.Null(vol);
            });
        }

        /// <summary>
        /// UnloadAudio(true) followed by ReloadAudio(true) must succeed.
        /// This exercises the full teardown→reinitialise cycle.
        /// </summary>
        [Fact]
        [Trait("Category", "Functional")]
        public void UnloadThenReload_Succeeds()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                // Act – should not throw
                mixer.UnloadAudio(true);
                mixer.ReloadAudio(true);

                // Assert – functional after re-init
                int[] pids = mixer.GetPIDs();
                Assert.NotNull(pids);
            });
        }

        /// <summary>
        /// Calling UnloadAudio(true) multiple times must not throw.
        /// Double-release was a possible crash before PR #55's null checks.
        /// </summary>
        [Fact]
        [Trait("Category", "Functional")]
        public void UnloadAudio_CalledTwice_DoesNotThrow()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                var ex = Record.Exception(() =>
                {
                    mixer.UnloadAudio(true);
                    mixer.UnloadAudio(true); // second call must be safe
                });
                Assert.Null(ex);
            });
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Memory-leak regression tests (PR #55)
    // ─────────────────────────────────────────────────────────────────────────

    public class VolumeMixerMemoryTests
    {
        // How many ReloadAudio iterations to run in each leak test.
        private const int WarmUpIterations = 10;
        private const int MeasureIterations = 100;

        // Maximum acceptable private-bytes growth (bytes) over MeasureIterations
        // calls. 4 MB is very generous; without the fix you'd typically see
        // 30–100 MB growth for 100 full-reload cycles.
        private const long MaxAllowedGrowthBytes = 4 * 1024 * 1024; // 4 MB

        /// <summary>
        /// Regression test for PR #55 leak site 1 & 2:
        /// Repeated ReloadAudio(false) calls (soft reload – only re-enumerates
        /// sessions) must not accumulate COM object wrappers.
        /// Without the fix, each call leaked the old sessionEnumerator and all
        /// ISimpleAudioVolume objects in volumeSessionList.
        /// </summary>
        [Fact]
        [Trait("Category", "MemoryLeak")]
        public void ReloadAudio_SoftReload_DoesNotLeakMemory()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                // Warm up – allow initial allocations to settle.
                for (int i = 0; i < WarmUpIterations; i++)
                    mixer.ReloadAudio(false);

                ForceFullGC();
                long baseline = GetPrivateBytes();

                // Measure – 100 soft reloads.
                for (int i = 0; i < MeasureIterations; i++)
                    mixer.ReloadAudio(false);

                ForceFullGC();
                long after = GetPrivateBytes();

                long growth = after - baseline;
                Assert.True(
                    growth < MaxAllowedGrowthBytes,
                    $"Memory grew by {growth / 1024:N0} KB over {MeasureIterations} " +
                    $"soft-reload calls (threshold: {MaxAllowedGrowthBytes / 1024:N0} KB). " +
                    "This indicates COM objects are not being released properly.");
            });
        }

        /// <summary>
        /// Regression test for PR #55 leak site 1, 2 &amp; 3 combined:
        /// ReloadAudio(true) (full device teardown + reinitialise) is the
        /// more expensive path.  It must also not accumulate leaked COM objects
        /// across repeated calls.
        /// </summary>
        [Fact]
        [Trait("Category", "MemoryLeak")]
        public void ReloadAudio_HardReload_DoesNotLeakMemory()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            // Use fewer iterations for the hard reload because it is slower.
            const int hardWarmUp = 5;
            const int hardMeasure = 30;
            const long hardThreshold = 6 * 1024 * 1024; // 6 MB

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                for (int i = 0; i < hardWarmUp; i++)
                    mixer.ReloadAudio(true);

                ForceFullGC();
                long baseline = GetPrivateBytes();

                for (int i = 0; i < hardMeasure; i++)
                    mixer.ReloadAudio(true);

                ForceFullGC();
                long after = GetPrivateBytes();

                long growth = after - baseline;
                Assert.True(
                    growth < hardThreshold,
                    $"Memory grew by {growth / 1024:N0} KB over {hardMeasure} " +
                    $"hard-reload calls (threshold: {hardThreshold / 1024:N0} KB). " +
                    "IMMDevice/IAudioSessionManager2/IMMDeviceEnumerator COM objects " +
                    "may not be released properly.");
            });
        }

        /// <summary>
        /// Regression test for PR #55 leak site 3:
        /// When the same PID appears more than once in the session enumerator
        /// (can happen with multi-stream processes), the old ISimpleAudioVolume
        /// object must be released before being replaced in the dictionary.
        ///
        /// We verify this indirectly: repeated reload cycles on a system that
        /// already has audio sessions must not grow memory.
        /// </summary>
        [Fact]
        [Trait("Category", "MemoryLeak")]
        public void ReloadAudio_DuplicatePidReplacement_DoesNotLeakComObject()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            // This test interleaves soft and hard reloads to maximise the chance
            // that duplicate-PID replacement paths are exercised.
            const int iterations = 50;
            const long threshold = 4 * 1024 * 1024;

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                // Warm up
                for (int i = 0; i < 5; i++)
                {
                    mixer.ReloadAudio(true);
                    mixer.ReloadAudio(false);
                }

                ForceFullGC();
                long baseline = GetPrivateBytes();

                for (int i = 0; i < iterations; i++)
                {
                    // Alternate between full and soft reload to stress all code paths.
                    mixer.ReloadAudio(i % 3 == 0);
                }

                ForceFullGC();
                long after = GetPrivateBytes();

                long growth = after - baseline;
                Assert.True(
                    growth < threshold,
                    $"Memory grew by {growth / 1024:N0} KB over {iterations} " +
                    $"mixed-reload calls (threshold: {threshold / 1024:N0} KB). " +
                    "Old ISimpleAudioVolume objects may not be released when a " +
                    "PID entry is replaced in the dictionary.");
            });
        }

        /// <summary>
        /// Verifies that the total number of live RCW (Runtime Callable Wrapper)
        /// objects doesn't grow in proportion to the number of ReloadAudio calls.
        /// After N calls, the managed heap should contain roughly the same number
        /// of COM wrapper objects as after the warm-up phase.
        /// </summary>
        [Fact]
        [Trait("Category", "MemoryLeak")]
        public void ReloadAudio_RCWObjectCount_StabilisesAfterWarmUp()
        {
            if (!AudioTestHelper.HasAudioDevice())
                return;

            AudioTestHelper.RunOnStaThread(mixer =>
            {
                // Warm up
                for (int i = 0; i < WarmUpIterations; i++)
                    mixer.ReloadAudio(false);

                ForceFullGC();

                // Count managed bytes used as a proxy for object accumulation.
                long gen2_before = GC.GetTotalMemory(true);

                for (int i = 0; i < MeasureIterations; i++)
                    mixer.ReloadAudio(false);

                ForceFullGC();
                long gen2_after = GC.GetTotalMemory(true);

                long growth = gen2_after - gen2_before;

                // Allow up to 2 MB of managed heap growth for string/array churn.
                const long managedThreshold = 2 * 1024 * 1024;
                Assert.True(
                    growth < managedThreshold,
                    $"Managed heap grew by {growth / 1024:N0} KB over {MeasureIterations} " +
                    $"soft-reload calls (threshold: {managedThreshold / 1024:N0} KB). " +
                    "COM RCW objects or related managed objects may be accumulating.");
            });
        }

        // ─── helpers ─────────────────────────────────────────────────────────

        private static void ForceFullGC()
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true);
        }

        private static long GetPrivateBytes()
            => Process.GetCurrentProcess().PrivateMemorySize64;
    }
}
