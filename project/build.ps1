$buildFolder = "..\Build"
$bepinexFolder = "..\Build\BepInEx"
$bepinexPatchFolder = "..\Build\BepInEx\patchers"
$bepinexPluginFolder = "..\Build\BepInEx\plugins"
$bepinexSptFolder = "..\Build\BepInEx\plugins\spt"
$projReleaseFolder = ".\bin\Release\net471"
$licenseFile = "..\..\LICENSE.md"

# Delete build folder and contents to make sure it's clean
if (Test-Path "$buildFolder") { Remove-Item -Path "$buildFolder" -Recurse -Force }

# Create build folder and subfolders if they don't exist
$foldersToCreate = @("$buildFolder", "$bepinexFolder", "$bepinexPatchFolder", "$bepinexPluginFolder", "$bepinexSptFolder")
foreach ($folder in $foldersToCreate) {
    if (-not (Test-Path "$folder")) { New-Item -Path "$folder" -ItemType Directory }
}

# Move DLLs from project's bin-release folder to the build folder
Copy-Item "$projReleaseFolder\spt-common.dll" -Destination "$bepinexSptFolder"
Copy-Item "$projReleaseFolder\spt-reflection.dll" -Destination "$bepinexSptFolder"
Copy-Item "$projReleaseFolder\spt-prepatch.dll" -Destination "$bepinexPatchFolder"
Copy-Item "$projReleaseFolder\spt-core.dll" -Destination "$bepinexSptFolder"
Copy-Item "$projReleaseFolder\spt-custom.dll" -Destination "$bepinexSptFolder"
Copy-Item "$projReleaseFolder\spt-debugging.dll" -Destination "$bepinexSptFolder"
Copy-Item "$projReleaseFolder\spt-singleplayer.dll" -Destination "$bepinexSptFolder"
# If any new DLLs need to be copied, add here

# Write the contents of the license file to a txt
Get-Content "$licenseFile" | Out-File "$buildFolder\LICENSE-Modules.txt" -Encoding UTF8
