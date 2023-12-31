@echo off
git pull
::::: ---- defining the assign macro ---- ::::::::
setlocal DisableDelayedExpansion
(set LF=^
%=EMPTY=%
)
set ^"\n=^^^%LF%%LF%^%LF%%LF%^^"

::set argv=Empty
set assign=for /L %%n in (1 1 2) do ( %\n%
   if %%n==2 (%\n%
      setlocal enableDelayedExpansion%\n%
      for /F "tokens=1,2 delims=," %%A in ("!argv!") do (%\n%
         for /f "tokens=* delims=" %%# in ('%%~A') do endlocal^&set "%%~B=%%#" %\n%
      ) %\n%
   ) %\n%
) ^& set argv=,

::::: -------- ::::::::

%assign% "git describe --tags --abbrev=0",version
set version_num=%version:~1%%
echo %version%
echo %version_num%
echo Restoring
dotnet restore
echo Testing
dotnet test --no-build --verbosity normal
echo Publishing
dotnet publish -p:Version="%version_num%" -p:PublishProfile=FolderProfile -c Release --no-restore
set zipfile=".\WinBGMute\bin\publish\WinBGMuter.zip"
del %zipfile%
"C:\Program Files\7-Zip\7z.exe" a -tzip %zipfile% ".\WinBGMute\bin\publish\WinBGMuter\*"