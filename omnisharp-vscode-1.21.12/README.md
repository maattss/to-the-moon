## C# for Visual Studio Code (powered by OmniSharp)

|Master|Release|
|:--:|:--:|
|[![Master Build Status](https://travis-ci.org/OmniSharp/omnisharp-vscode.svg?branch=master)](https://travis-ci.org/OmniSharp/omnisharp-vscode)|[![Release Build Status](https://travis-ci.org/OmniSharp/omnisharp-vscode.svg?branch=release)](https://travis-ci.org/OmniSharp/omnisharp-vscode)|

[![Wallaby.js](https://img.shields.io/badge/wallaby.js-configured-green.svg)](https://wallabyjs.com)

Welcome to the C# extension for Visual Studio Code! This extension provides the following features inside VS Code:

* Lightweight development tools for [.NET Core](https://dotnet.github.io).
* Great C# editing support, including Syntax Highlighting, IntelliSense, Go to Definition, Find All References, etc.
* Debugging support for .NET Core (CoreCLR). NOTE: Mono debugging is not supported. Desktop CLR debugging has [limited support](https://github.com/OmniSharp/omnisharp-vscode/wiki/Desktop-.NET-Framework).
* Support for project.json and csproj projects on Windows, macOS and Linux.

The C# extension is powered by [OmniSharp](https://github.com/OmniSharp/omnisharp-roslyn).

### Get Started Writing C# in VS Code

* [Documentation](https://code.visualstudio.com/docs/languages/csharp)
* [Video Tutorial compiling with .NET Core](https://channel9.msdn.com/Blogs/dotnet/Get-started-VSCode-Csharp-NET-Core-Windows)

## What's new in 1.21.12
* Fixed out of bounds exception in line mapping ([#3485](https://github.com/OmniSharp/omnisharp-vscode/issues/3485), PR: [omnisharp-roslyn/#1707](https://github.com/OmniSharp/omnisharp-roslyn/pull/1707))
* Added support for aliases in project references ([omnisharp-roslyn/#1685](https://github.com/OmniSharp/omnisharp-roslyn/issues/1685), PR: [omnisharp-roslyn/#1701](https://github.com/OmniSharp/omnisharp-roslyn/pull/1701))
* Raised the lowest discovered VS2019 version to 16.3 ([omnisharp-roslyn/#1700](https://github.com/OmniSharp/omnisharp-roslyn/issues/1700), PR: (#1713)(https://github.com/OmniSharp/omnisharp-roslyn/pull/1713))
* Fixed a bug where organizing usings clashed with other formatting settings (PR: [omnisharp-roslyn/#1715](https://github.com/OmniSharp/omnisharp-roslyn/pull/1713))

## What's new in 1.21.11
* Updated the bundled to Mono 6.8.0 and MSBuild to be copied from Mono 6.8.0 ([omnisharp-roslyn/#1693](https://github.com/OmniSharp/omnisharp-roslyn/issues/1693), PR: [omnisharp-roslyn/#1697](https://github.com/OmniSharp/omnisharp-roslyn/pull/1697))
* Included NugetSDKResolver in the minimal MSBuild, which introduces support for Nuget based project SDKs like Arcade ([omnisharp-roslyn/#1678](https://github.com/OmniSharp/omnisharp-roslyn/issues/1678), PR: [omnisharp-roslyn/#1696](https://github.com/OmniSharp/omnisharp-roslyn/pull/1696))
* Added option (`csharp.supressBuildAssetsNotification`) to surpress missing build asset notifications (PR:[#3538](https://github.com/OmniSharp/omnisharp-vscode/pull/3538))
* The minimum Mono version required to run OmniSharp on has been increased to 6.4.0

## What's new in 1.21.10
* Updated Razor support (PR:[#3524](https://github.com/OmniSharp/omnisharp-vscode/pull/3524))
    * Added quick info (hover) support for TagHelper and Blazor components. You can now hover over TagHelpers, Components and their attributes to understand what associated C# type you're hovering over in addition to an attributes expected value type.
    * Migrated Razor's project understanding from the VSCode extension into the Language Server. This enables the language server to reboot without extra assistance (reliability) from an LSP client and also enables future Razor LSP clients to have richer functionality with less "work".
    * Added C# light bulbs to enable users to "Fully Qualify" members that are causing errors.
    * Expanded Razor's TextMate grammar colorization support to understand email addresses and `await foreach`.
    * Several bug fixes
* Updated Debugger support (PR:[#3515](https://github.com/OmniSharp/omnisharp-vscode/pull/3515))
    * Added option to search the NuGet.org Symbol Server
    * Added options to control logging Process and Thread exits while debugging
* Fixed a bug where completion items didn't decode symbols corectly (impacted, for example, object initializer completion quality) ([#3465](https://github.com/OmniSharp/omnisharp-vscode/issues/3465), PR:[omnisharp-roslyn/#1670](https://github.com/OmniSharp/omnisharp-roslyn/pull/1670))
* Updated to MsBuild 16.4.0 on Linux/MacOS (PR:[omnisharp-roslyn/#1669](https://github.com/OmniSharp/omnisharp-roslyn/pull/1669))
* Added support for implement type options - it is now possible to define whether code-fix/refactoring generated properties should be auto- or throwing-properties and at which place in the class should newly generated members be inserted. They can be set via OmniSharp configuration, such as `omnisharp.json` file. (PR: [omnisharp-roslyn/#1672](https://github.com/OmniSharp/omnisharp-roslyn/pull/1672))
* Added support for organizing usings on format. This can be set via OmniSharp configuration, such as `omnisharp.json` file. (PR:[omnisharp-roslyn/#1686](https://github.com/OmniSharp/omnisharp-roslyn/pull/1686))
* Improved support for .NET Core 3.1

## What's new in 1.21.9
* Use the base filename instead of 'ClassName' in ctor snippet (PR:[#3385](https://github.com/OmniSharp/omnisharp-vscode/pull/3385))
* Added command to re-run code analysis on single project or all projects (PR:[#3089](https://github.com/OmniSharp/omnisharp-vscode/pull/3089))
* Updated Razor support (PR:[#3445](https://github.com/OmniSharp/omnisharp-vscode/pull/3445))
  * Rename support
  * Go to definition support
  * Go to implementation support
  * Find all references support
  * CodeLens support
  * Several mainline bug fixes
* Updated Razor grammar for better colorization (PR:[#3448](https://github.com/OmniSharp/omnisharp-vscode/pull/3448))
* Updated to MsBuild 16.4.0 (PR:[omnisharp-roslyn/#1662](https://github.com/OmniSharp/omnisharp-roslyn/pull/1662))
* Line pragma is now respected in find references ([omnisharp-roslyn/#1649](https://github.com/OmniSharp/omnisharp-roslyn/issues/1649), PR:[omnisharp-roslyn/#1660](https://github.com/OmniSharp/omnisharp-roslyn/pull/1660))
* Do not set mono paths when running in standalone mode ([omnisharp-vscode#3410](https://github.com/OmniSharp/omnisharp-vscode/issues/3410), [omnisharp-vscode#3340](https://github.com/OmniSharp/omnisharp-vscode/issues/3340), [omnisharp-roslyn/#1650](https://github.com/OmniSharp/omnisharp-roslyn/issues/1650), PR:[omnisharp-roslyn/#1656](https://github.com/OmniSharp/omnisharp-roslyn/pull/1656))
* Fixed a bug where OmniSharp would crash on startup if the path contained `=` sign ([omnisharp-vscode#3436](https://github.com/OmniSharp/omnisharp-vscode/issues/3436), PR:[omnisharp-roslyn/#1661](https://github.com/OmniSharp/omnisharp-roslyn/pull/1661))
* Improved support for .NET Core 3.1

### Supported Operating Systems for Debugging

* Currently, the C# debugger officially supports the following operating systems:

  * X64 operating systems:
    * Windows 7 SP1 and newer
    * macOS 10.12 (Sierra) and newer
    * Linux: see [.NET Core documentation](https://github.com/dotnet/core/blob/master/release-notes/2.2/2.2-supported-os.md#linux) for the list of supported distributions. Note that other Linux distributions will likely work as well as long as they include glibc and OpenSSL.
  * ARM operating systems:
    * Linux is supported as a remote debugging target

### Found a Bug?

To file a new issue to include all the related config information directly from vscode by entering the command pallette with <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>P</kbd>
(<kbd>Cmd</kbd>+<kbd>Shift</kbd>+<kbd>P</kbd> on macOS) and running `CSharp: Report an issue` command. This will open a browser window with all the necessary information related to the installed extensions, dotnet version, mono version, etc. Enter all the remaining information and hit submit. More information can be found on the [wiki](https://github.com/OmniSharp/omnisharp-vscode/wiki/Reporting-Issues).

Alternatively you could visit https://github.com/OmniSharp/omnisharp-vscode/issues and file a new one.

### Development

First install:
* Node.js (8.11.1 or later)
* Npm (5.6.0 or later)

To **run and develop** do the following:

* Run `npm i`
* Run `npm run compile`
* Open in Visual Studio Code (`code .`)
* *Optional:* run `npm run watch`, make code changes
* Press <kbd>F5</kbd> to debug

To **test** do the following: `npm run test` or <kbd>F5</kbd> in VS Code with the "Launch Tests" debug configuration.

### License
The Microsoft C# extension is subject to [these license terms](RuntimeLicenses/license.txt).
The source code to this extension is available on [https://github.com/OmniSharp/omnisharp-vscode](https://github.com/OmniSharp/omnisharp-vscode) and licensed under the [MIT license](LICENSE.txt).
