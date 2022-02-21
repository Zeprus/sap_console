# Zeprus' SAP Console Enabler
-a Super Auto Pets mod-
## Features
* Open developer console by ctrl clicking the Achievements button 10 times

## Installation
1. Download the latest BepInEx Build from [here](https://builds.bepis.io/projects/bepinex_be).
2. Follow the installation instructions for Il2Cpp Unity [here](https://docs.bepinex.dev/master/articles/user_guide/installation/unity_il2cpp.html).
3. Download the latest release [here](https://github.com/Zeprus/sap_sandbox/releases).
4. Move it to "Super Auto Pets\BepInEx\plugins\"

## Development
If you want to continue working on this project make sure to check [the project file](https://github.com/Zeprus/sap_console/blob/main/sap_console.csproj) and set the GameDir property to the root directory of your Super Auto Pets installation.

Build the project with 'dotnet publish' for automatic deployment.

If you are running into unresolved references during build you most likely did not configure the GameDir correctly forgot to run BepInEx once.
