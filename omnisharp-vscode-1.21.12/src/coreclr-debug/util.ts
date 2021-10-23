/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

import * as path from 'path';
import * as fs from 'fs';
import * as semver from 'semver';
import * as os from 'os';
import { execChildProcess } from './../common';

const MINIMUM_SUPPORTED_DOTNET_CLI: string = '1.0.0';

export class DotnetInfo
{
    public Version: string;
    public OsVersion: string;
    public RuntimeId: string;
}

export class DotNetCliError extends Error {
    public ErrorMessage: string; // the message to display to the user
    public ErrorString: string; // the string to log for this error
}

export class CoreClrDebugUtil
{
    private _extensionDir: string = '';
    private _debugAdapterDir: string = '';
    private _installCompleteFilePath: string = '';

    constructor(extensionDir: string) {
        this._extensionDir = extensionDir;
        this._debugAdapterDir = path.join(this._extensionDir, '.debugger');
        this._installCompleteFilePath = path.join(this._debugAdapterDir, 'install.complete');
    }

    public extensionDir(): string {
        if (this._extensionDir === '')
        {
            throw new Error('Failed to set extension directory');
        }
        return this._extensionDir;
    }

    public debugAdapterDir(): string {
        if (this._debugAdapterDir === '') {
            throw new Error('Failed to set debugadpter directory');
        }
        return this._debugAdapterDir;
    }

    public installCompleteFilePath(): string {
        if (this._installCompleteFilePath === '')
        {
            throw new Error('Failed to set install complete file path');
        }
        return this._installCompleteFilePath;
    }

    public static async writeEmptyFile(path: string) : Promise<void> {
        return new Promise<void>((resolve, reject) => {
            fs.writeFile(path, '', (err) => {
                if (err) {
                    reject(err.code);
                } else {
                    resolve();
                }
            });
        });
    }

    public defaultDotNetCliErrorMessage(): string {
        return 'Failed to find up to date dotnet cli on the path.';
    }

    // This function checks for the presence of dotnet on the path and ensures the Version
    // is new enough for us. 
    // Returns: a promise that returns a DotnetInfo class
    // Throws: An DotNetCliError() from the return promise if either dotnet does not exist or is too old. 
    public async checkDotNetCli(): Promise<DotnetInfo>
    {
        let dotnetInfo = new DotnetInfo();

        return execChildProcess('dotnet --info', process.cwd())
        .then((data: string) => {
            let lines: string[] = data.replace(/\r/mg, '').split('\n');
            lines.forEach(line => {
                let match: RegExpMatchArray;
                if (match = /^\ Version:\s*([^\s].*)$/.exec(line)) {
                    dotnetInfo.Version = match[1];
                } else if (match = /^\ OS Version:\s*([^\s].*)$/.exec(line)) {
                    dotnetInfo.OsVersion = match[1];
                } else if (match = /^\ RID:\s*([\w\-\.]+)$/.exec(line)) {
                    dotnetInfo.RuntimeId = match[1];
                }
            });
        }).catch((error) => {
            // something went wrong with spawning 'dotnet --info'
            let dotnetError = new DotNetCliError();
            dotnetError.ErrorMessage = 'The .NET Core SDK cannot be located. .NET Core debugging will not be enabled. Make sure the .NET Core SDK is installed and is on the path.';
            dotnetError.ErrorString = "Failed to spawn 'dotnet --info'";
            throw dotnetError;
        }).then(() => {
            // succesfully spawned 'dotnet --info', check the Version
            if (semver.lt(dotnetInfo.Version, MINIMUM_SUPPORTED_DOTNET_CLI))
            {
                let dotnetError = new DotNetCliError();
                dotnetError.ErrorMessage = 'The .NET Core SDK located on the path is too old. .NET Core debugging will not be enabled. The minimum supported version is ' + MINIMUM_SUPPORTED_DOTNET_CLI + '.';
                dotnetError.ErrorString = "dotnet cli is too old";
                throw dotnetError;
            }

            return dotnetInfo;
        });
    }

    public static isMacOSSupported() : boolean {
        // .NET Core 2.0 requires macOS 10.12 (Sierra), which is Darwin 16.0+
        // Darwin version chart: https://en.wikipedia.org/wiki/Darwin_(operating_system)
        return semver.gte(os.release(), "16.0.0");
    }

    public static existsSync(path: string) : boolean {
        try {
            fs.accessSync(path, fs.constants.F_OK);
            return true;
        } catch (err) {
            if (err.code === 'ENOENT' || err.code === 'ENOTDIR') {
                return false;
            } else {
                throw Error(err.code);
            }
        }
    }

    public static getPlatformExeExtension() : string {
        if (process.platform === 'win32') {
            return '.exe';
        }

        return '';
    }
}
