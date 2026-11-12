# Filename Randomizer

A lightweight Windows utility that renames all files in a selected folder to random alphanumeric strings (`a`–`z`, `0`–`9`) of a chosen length (16–40 characters, default 32), preserving file extensions.

## Features

- Randomizes filenames in a selected folder to base36 strings (e.g. `k3f7x2q1...4e9z.jpg`)
- **Name length slider** — choose 16–40 characters; the setting is remembered between sessions
- **Recent folders** — the last 3 folders you renamed are listed; click one to reuse it
- **Pin** — keep one folder at the top of the list permanently; recent folders keep rotating underneath it
- **Windows context menu integration** — right-click any folder and select "Randomize Filenames" to launch with that folder pre-filled
- Optional: mark renamed files as hidden after renaming
- Clean uninstall via Add/Remove Programs

## Requirements

- Windows 10 or 11
- .NET Framework 4.8 (included with Windows 10/11)

## Installation

Download `FilenameRandomizer_Setup.exe` from [Releases](../../releases) and run it. Accept the UAC prompt. That's it.

> **Note:** Windows SmartScreen may show a warning on first run because the installer is not code-signed. Click "More info" → "Run anyway." The source code is here if you want to verify it yourself.

## Usage

**From the context menu (recommended):**
Right-click any folder in Windows Explorer → "Randomize Filenames" → the app opens with that folder pre-filled → click Rename.

**Standalone:**
Launch from Start Menu or `Program Files\Filename Randomizer\`. Browse to a folder, click Rename.

**Recent folders and Pin:**
Every folder you rename goes to the top of the *Recent folders* list, which keeps the last 3; older ones drop off. Click an entry to load it.

To keep a folder permanently, select it (or browse to it) and click **Pin**. The pinned folder sits at the top of the list marked `[Pinned]`, never ages out, and is what the app opens to on launch. Recent folders keep rotating underneath it. Pinning a different folder moves the old one back into the recent list; **Unpin** does the same for the current one. Browse stays available at all times.

## Warning

Renaming is **immediate and irreversible.** There is no undo. The original filenames are gone. Do not run this on folders containing files whose names matter unless you are sure.

## Building from Source

### Automatic (GitHub Actions)

Every push and pull request is built on a Windows runner by `.github/workflows/build.yml`. Open the run under the **Actions** tab and download:

- `filename-randomizer-exe` — the bare `Filename Randomizer.exe`
- `filename-randomizer-setup` — the Inno Setup installer

To publish a release, bump `AppVersion` in `FilenameRandomizer.iss` (and the versions in `My Project/AssemblyInfo.vb`), commit, then push a matching tag:

```
git tag v1.2.0
git push origin v1.2.0
```

The workflow refuses to publish if the tag and `AppVersion` disagree.

### Local (Visual Studio)

1. Open `Filename Randomizer.sln` in Visual Studio (Community edition is free)
2. Target framework: .NET Framework 4.8
3. Build → Rebuild Solution (Release | x86)
4. Output: `bin\Release\Filename Randomizer.exe`

To build the installer, open `FilenameRandomizer.iss` in [Inno Setup](https://jrsoftware.org/isinfo.php) and Build → Compile. It picks up `bin\Release\Filename Randomizer.exe` relative to the script and writes `Output\FilenameRandomizer_Setup.exe`.

## Version History

**v1.3.0** — Replaced the single Pin Folder toggle with a *Recent folders* list (last 3 renamed folders, click to reuse) plus one pinned folder that stays at the top and never ages out; Browse is no longer disabled while pinned. Window widened so paths are readable. The Browse For Folder window now opens as a tall 325×810 column (shrunk to fit smaller screens) and remembers the size you leave it at. Existing pinned/last-used settings migrate automatically. Settings now carry over across version updates (previously each new version started with default settings).

**v1.2.0** — Expanded the character pool from hex (`0`–`9`, `a`–`f`; 16 symbols) to lowercase alphanumeric (`0`–`9`, `a`–`z`; 36 symbols). Name space grows from 16³² ≈ 3.4×10³⁸ to 36³² ≈ 6.3×10⁴⁹ at the same 32-char length. Uppercase excluded because NTFS filenames are case-insensitive. Added a name-length slider (16–40, default 32, persisted in settings). Renaming now regenerates a name if it is already taken instead of aborting the batch. Added GitHub Actions build (exe + installer on every push, Release on `v*` tags), solution file and `app.config`; assembly version synced to 1.2.0.

**v1.1.1** — Fixed off-by-one error in name generation (was producing 33-char names); fixed excluded character (`9` was never selected due to incorrect upper bound); command-line argument support for context menu integration

**v1.1.0** — Replaced broken IniFile dependency with native My.Settings; added Pin Folder feature; last-used path now saves on successful rename rather than on close; fixed dead code path in protected-file message; retargeted to .NET Framework 4.8

**v1.0.1** — Original release by xdpirate (2012)

## License

MIT
