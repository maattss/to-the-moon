/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

'use strict';

import * as path from 'path';
import { commandLineOptions } from './commandLineArguments';

export const rootPath = path.resolve(__dirname, '..');

export const vscodeignorePath = path.join(rootPath, '.vscodeignore');
export const offlineVscodeignorePath = path.join(rootPath, 'offline.vscodeignore');
export const onlineVscodeignorePath = path.join(rootPath, 'release.vscodeignore');

export const nodeModulesPath = path.join(rootPath, 'node_modules');
export const vscePath = path.join(nodeModulesPath, 'vsce', 'out', 'vsce');
export const nycPath = path.join(nodeModulesPath, 'nyc', 'bin', 'nyc.js');
export const mochaPath = path.join(nodeModulesPath, 'mocha', 'bin', 'mocha');
export const istanbulPath = path.join(nodeModulesPath, 'istanbul', 'lib', 'cli.js');
export const codecovPath = path.join(nodeModulesPath, 'codecov', 'bin', 'codecov');
export const vscodeTestHostPath = path.join(nodeModulesPath, 'vscode', 'bin', 'test');

export const packageJsonPath = path.join(rootPath, "package.json");

export const packedVsixOutputRoot = commandLineOptions.outputFolder || rootPath;
export const unpackedVsixPath = path.join(rootPath, "vsix");
export const unpackedExtensionPath = path.join(unpackedVsixPath, "extension");

export const codeExtensionPath = commandLineOptions.codeExtensionPath || rootPath;
export const codeExtensionSourcesPath = path.join(codeExtensionPath, "dist");

export const testRootPath = path.join(rootPath, "out", "test");
export const testAssetsRootPath = path.join(rootPath, "test", "integrationTests", "testAssets");

export const coverageRootPath = path.join(rootPath, 'coverage');
export const unitTestCoverageRootPath = path.join(coverageRootPath, 'unit');
export const integrationTestCoverageRootPath = path.join(coverageRootPath, 'integration');

export const nycOutputPath = path.join(rootPath, '.nyc_output');
export const integrationTestNycOutputPath = path.join(nycOutputPath, 'integration');

export const nodePath = path.join(process.env.NVM_BIN
    ? `${process.env.NVM_BIN}${path.sep}`
    : '', 'node');

