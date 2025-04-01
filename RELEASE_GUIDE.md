
# Release Guide

## Build application

Run the Azure Pipeline "FSimMan-Build-release".
This creates a "releaseTest" directory for the TestTool.
In it, you will find the raw binaries (including debug files),
a zipped version of the binaries (portable edition),and the installer, both without debug files.

## Test installation

Install the application through the built installer on your local machine.

## Test application

Test the locally installed application.

## Create GitHub release

 - Make sure to include all important changes in the release notes.
 - Upload the installer and the portable edition to the release.
 - Publish the release.

## Prepare updated winget manifest

 - Run the following command in the repo directory:
`wingetcreate update OliverFida.FSimMan -v x.x.x -u 'https://github.com/OliverFida/FSimMan/releases/download/vx.x.x/FSimMan-vx.x.x.exe|x64'`

 - Manually check the generated manifest files
	 - Compare to previous version
	 - Make sure it includes everything that is needed (.NET packages, etc.)

 - Test-Install through the local manifest using this command:
`winget install --manifest .\manifests\o\OliverFida\FSimMan\x.x.x\`

 - Submit the new manifest files using the first command with an extra trailing parameter: `-s`
