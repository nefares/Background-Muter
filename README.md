
# Background Muter     
![image](https://img.shields.io/github/license/nefares/Background-Muter) ![image](https://img.shields.io/github/issues/nefares/Background-Muter) [![.NET](https://github.com/nefares/Background-Muter/actions/workflows/dotnet.yml/badge.svg)](https://github.com/nefares/Background-Muter/actions/workflows/dotnet.yml) ![GitHub all releases](https://img.shields.io/github/downloads/nefares/Background-Muter/total)

-------
### Now gathering your whitelists :cat: and blacklists :dog: here => (https://github.com/nefares/Background-Muter/discussions/28)
-------

This tool automatically mutes applications in the background, and unmutes them once they are switched to foreground.
You can add exceptions for which applications are never muted.

[comment]: ![image](https://user-images.githubusercontent.com/8545128/170842100-7c0d6dbd-acf8-4d28-b605-8a7abbbc106c.png)

![image](https://github.com/user-attachments/assets/eb353683-71df-4cc5-a833-60c150c3a528)

# Features
* Works out of the box with default settings
* Add exceptions for applications to never be muted
* Minimize to tray icon
* Dark Mode 

# Requirements
* Requires DotNet 8.0 to work (install here https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

# Getting Started
 - Download **WinBGMuter.zip** from the [Releases](https://github.com/nefares/Background-Muter/releases/latest) page, extract it and open the extracted folder (see image below)
 - Run **WinBGMuter.exe**. The application will automatically start muting background processes with default settings.
 - To **add or remove application exceptions** either:
	 - Directly add/remove items in the text box under **"Mute Exceptions"**
	 - Or under **"Mute Exception Changer"**: The left list shows detected processes with an audio channel; the right list shows processes part of the exception list. Simply use the arrow buttons < and > to move processes from one list to the other.
 - **Activate Logger**: Enables or disables logging
 - **Enable Console**: spawns a windows console for logging and debugging
 - **Restore Defaults**: overrides existing settings and restores default settings
 - **Enable Dark Mode**: enables an experimental dark mode
 - **Mute Condition**: Modifies the mute condition. "Background" is default, and mutes apps in the background. "Minimized" is an alternative mode, which only mutes apps when they are minimized.
 - **Minimize to Tray** : minimize the application and it will automatically minimize to tray. Double clicking restores the window, and mouse right click shows a context menu. 

<img width="1027" height="418" alt="image" src="https://github.com/user-attachments/assets/9d9cceb9-3d16-400b-88e1-afbd90a5834d" />

# License

This program is licensed under GNU GPL v3 (https://choosealicense.com/licenses/gpl-3.0/)
