#define AppName "FSimMan"
#define AppExeName "OF." + AppName + ".exe"
#define AppVersion "0.2.0"
#define AppPublisher "Oliver Fida"

#define AssocFsmmpName AppName + " Modpack"
#define AssocFsmmpExt ".fsmmp"
#define AssocFsmmpKey StringChange(AssocFsmmpName, " ", "") + AssocFsmmpExt

[Setup]
AppId={{1370E40A-AD93-4EE6-9784-C343811282C7}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#AppPublisher}
DefaultDirName={autopf}\{#AppPublisher}\{#AppName}
DisableDirPage=no
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
UninstallDisplayName={#AppName}
UninstallDisplayIcon={app}\{#AppExeName},0
;ChangesAssociations=yes
DisableProgramGroupPage=yes
PrivilegesRequired=admin
OutputDir=C:\Users\Oliver Fida\Desktop\FSimMan
OutputBaseFilename={#AppName}-v{#AppVersion}
SetupIconFile=X:\Oliver Fida\FSimMan\OF.FSimMan.Resources\Logos\Logo22.ico
Compression=lzma2
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Files]
Source: "publish\{#AppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "publish\{#AppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "publish\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "publish\*.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion

;[Registry]
;Root: HKLM; Subkey: "Software\Classes\{#AssocFsmmpExt}\OpenWithProgids"; ValueType: string; ValueName: "{#AssocFsmmpKey}"; ValueData: ""; Flags: uninsdeletevalue
;Root: HKLM; Subkey: "Software\Classes\{#AssocFsmmpKey}"; ValueType: string; ValueName: ""; ValueData: "{#AssocFsmmpName}"; Flags: uninsdeletevalue
;Root: HKLM; Subkey: "Software\Classes\{#AssocFsmmpKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#AppExeName},0"; Flags: uninsdeletevalue
;Root: HKLM; Subkey: "Software\Classes\{#AssocFsmmpKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#AppExeName}"" ""%1"""; Flags: uninsdeletevalue
;Root: HKLM; Subkey: "Software\Classes\Applications\{#AppExeName}\SupportedTypes"; ValueType: string; ValueName: "{#AssocFsmmpExt}"; ValueData: ""; Flags: uninsdeletevalue

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Icons]
Name: "{autoprograms}\{#AppPublisher}\{#AppName}"; Filename: "{app}\{#AppExeName}"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#AppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(AppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent