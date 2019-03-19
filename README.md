# Autodesk Fusion 360 Force Sign In
The purpose of the program is to encapsulate the Autodesk Fusion
360 launcher to remove login sessions of users. It is intended 
to be used as an executable on Windows on public/shared accounts.
No additional functionality is added to Fusion 360 since it does
not modify the main application.

## Setup
The intended location of the executable is in the start menu
programs (`%appdata%\Microsoft\Windows\Start Menu\Programs\Autodesk`).
It shouldn't replace the existing shortcut in case the location of the
executable changes from the current location
(`%localappdata%\Autodesk\webdeploy\production\6a0c9611291d45bb9226980209917c3d`). 

## Documentation
Due to the small scope of the project, the README is the only
documentation outside of the code. This project only has code
in the `Program.cs` file in the root directory.

## Contributing
Both issues and pull requests are accepted for this project.
If pull requests include changes to the functionality, please
make sure to follow the existing code style.

## License
The Autodesk Fusion 360 Force Sign In is available under the terms
of the MIT Licence. See [LICENSE](LICENSE) for details.