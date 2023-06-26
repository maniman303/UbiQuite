# UbiQuite

UbiQuite is a simple program made to make sure Ubisoft Launcher is closed after you finish a game and the game completes save synchronization.

## Usage

Simply make a shortcut to UbiQuite.exe and as a first parameter provide full path to the game exe file. You can add second argument (literally anything) if you want the console of this program to remain minimized.

Works on Steam Deck (under Wine), too!

## Build

You will need Visual Studio 2022 and .Net 6. For best result and compatibility I recommend to publish locally the exe with *make single file* and *include runtime* options checked.

## ToDo

⬜ Add ability to provide just an exe file name, so program will try to locate it from current active directory.
⬜ Support passing arguments from UbiQuite to the game itself.