[Setup]
AppName=Filename Randomizer
AppVersion=1.3.0
AppPublisher=xdpirate
DefaultDirName={autopf}\Filename Randomizer
DefaultGroupName=Filename Randomizer
OutputBaseFilename=FilenameRandomizer_Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
; Require admin so we can write to HKLM and Program Files
PrivilegesRequired=admin

[Files]
; Relative to this script: the Release build output of Filename Randomizer.sln
Source: "bin\Release\Filename Randomizer.exe"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\Filename Randomizer"; Filename: "{app}\Filename Randomizer.exe"
Name: "{group}\Uninstall Filename Randomizer"; Filename: "{uninstallexe}"

[Registry]
; Add right-click context menu entry for folders
Root: HKLM; Subkey: "SOFTWARE\Classes\Directory\shell\RandomizeFilenames"; ValueType: string; ValueName: ""; ValueData: "Randomize Filenames"; Flags: uninsdeletekey
Root: HKLM; Subkey: "SOFTWARE\Classes\Directory\shell\RandomizeFilenames\command"; ValueType: string; ValueName: ""; ValueData: """{app}\Filename Randomizer.exe"" ""%1"""; Flags: uninsdeletekey

[Code]
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usPostUninstall then
  begin
    RegDeleteKeyIncludingSubkeys(HKLM, 'SOFTWARE\Classes\Directory\shell\RandomizeFilenames');
  end;
end;
