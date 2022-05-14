
# Background Muter     
![image](https://img.shields.io/github/license/nefares/Background-Muter) ![image](https://img.shields.io/github/issues/nefares/Background-Muter) [![.NET](https://github.com/nefares/Background-Muter/actions/workflows/dotnet.yml/badge.svg)](https://github.com/nefares/Background-Muter/actions/workflows/dotnet.yml)

This tool automatically mutes applications in the background, and unmutes them once they are switched to foreground.
You can add exceptions for which applications are never muted.

![image](https://user-images.githubusercontent.com/8545128/167307164-3da4a968-c7fa-408d-a90a-458c1d62d5ec.png)

# Features
* Works out of the box with default settings
* Add exceptions for applications to never be muted
* Minimize to tray icon
* Dark Mode (Experimental) 

# Requirements
* Requires DotNet 6.0 to work (install here https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

# Getting Started
 - Run **WinBGMuter.exe**. The application will automatically start muting background processes with default settings.
 - To **add or remove application exceptions** either:
	 - Directly add/remove items in the text box under **"Mute Exceptions"**
	 - Or under **"Mute Exception Changer"**: The left list shows detected processes with an audio channel; the right list shows processes part of the exception list. Simply use the arrow buttons < and > to move processes from one list to the other.
 - **Activate Logger**: Enables or disables logging
 - **Enable Console**: spawns a windows console for logging and debugging
 - **Restore Defaults**: overrides existing settings and restores default settings
 - **Enable Dark Mode**: enables a (very) experimental dark mode
 - **Minimize to Tray** : minimize the application and it will automatically minimize to tray. Double clicking restores the window, and mouse right click shows a context menu. 

# License

This program is licensed under GNU GPL v3 (https://choosealicense.com/licenses/gpl-3.0/)
