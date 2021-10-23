/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
import { should, expect } from 'chai';
import { TelemetryObserver } from '../../../src/observers/TelemetryObserver';
import { PlatformInformation } from '../../../src/platform';
import { PackageInstallation, InstallationFailure, InstallationSuccess, TestExecutionCountReport, TelemetryEventWithMeasures, OmnisharpDelayTrackerEventMeasures, OmnisharpStart, TelemetryEvent, ProjectConfiguration } from '../../../src/omnisharp/loggingEvents';
import { getNullTelemetryReporter } from '../testAssets/Fakes';
import { Package } from '../../../src/packageManager/Package';
import { PackageError } from '../../../src/packageManager/PackageError';

const chai = require('chai');
chai.use(require('chai-arrays'));

suite('TelemetryReporterObserver', () => {
    suiteSetup(() => should());
    let platformInfo = new PlatformInformation("platform", "architecture");
    let name = "";
    let property: { [key: string]: string } = null;
    let measure: { [key: string]: number }[] = [];
    let observer = new TelemetryObserver(platformInfo, () => {
        return {
            ...getNullTelemetryReporter,
            sendTelemetryEvent: (eventName: string, properties?: { [key: string]: string }, measures?: { [key: string]: number }) => {
                name += eventName;
                property = properties;
                measure.push(measures);
            }
        };
    });

    setup(() => {
        name = "";
        property = null;
        measure = [];
    });

    test('PackageInstallation: AcquisitionStart is reported', () => {
        let event = new PackageInstallation("somePackage");
        observer.post(event);
        expect(name).to.be.not.empty;
    });

    test('InstallationSuccess: Telemetry props contain installation stage', () => {
        let event = new InstallationSuccess();
        observer.post(event);
        expect(name).to.be.equal("AcquisitionSucceeded");
        expect(property).to.have.property("installStage", "completeSuccess");
    });

    test(`${ProjectConfiguration.name}: Telemetry props contains project id and target framework`, () => {
        const targetFrameworks = new Array("tfm1", "tfm2");
        const projectId = "sample";
        const references = new Array("ref1", "ref2");
        const fileExtensions = new Array(".cs", ".cshtml");
        let event = new ProjectConfiguration({
            TargetFrameworks: targetFrameworks,
            ProjectId: projectId,
            References: references,
            FileExtensions: fileExtensions
        });

        observer.post(event);
        expect(property["TargetFrameworks"]).to.be.equal("tfm1|tfm2");
        expect(property["ProjectId"]).to.be.equal(projectId);
        expect(property["References"]).to.be.equal("ref1|ref2");
        expect(property["FileExtensions"]).to.be.equal(".cs|.cshtml");
    });

    [
        new OmnisharpDelayTrackerEventMeasures("someEvent", { someKey: 1 }),
        new OmnisharpStart("startEvent", { someOtherKey: 2 })
    ].forEach((event: TelemetryEventWithMeasures) => {
        test(`${event.constructor.name}: SendTelemetry event is called with the name and measures`, () => {
            observer.post(event);
            expect(name).to.contain(event.eventName);
            expect(measure).to.be.containingAllOf([event.measures]);
        });
    });

    test(`${TelemetryEvent.name}: SendTelemetry event is called with the name, properties and measures`, () => {
        let event = new TelemetryEvent("someName", { "key": "value" }, { someKey: 1 });
        observer.post(event);
        expect(name).to.contain(event.eventName);
        expect(measure).to.be.containingAllOf([event.measures]);
        expect(property).to.be.equal(event.properties);
    });

    suite('InstallationFailure', () => {
        test("Telemetry Props contains platform information, install stage and an event name", () => {
            let event = new InstallationFailure("someStage", "someError");
            observer.post(event);
            expect(name).to.be.equal("AcquisitionFailed");
            expect(property).to.have.property("platform.architecture", platformInfo.architecture);
            expect(property).to.have.property("platform.platform", platformInfo.platform);
            expect(property).to.have.property("installStage");
        });

        test(`Telemetry Props contains message and packageUrl if error is package error`, () => {
            let error = new PackageError("someError", <Package>{ "description": "foo", "url": "someurl" });
            let event = new InstallationFailure("someStage", error);
            observer.post(event);
            expect(name).to.be.equal("AcquisitionFailed");
            expect(property).to.have.property("error.message", error.message);
            expect(property).to.have.property("error.packageUrl", error.pkg.url);
        });
    });

    suite('TestExecutionCountReport', () => {
        test('SendTelemetryEvent is called for "RunTest" and "DebugTest"', () => {
            let event = new TestExecutionCountReport({ "framework1": 20 }, { "framework2": 30 });
            observer.post(event);
            expect(name).to.contain("RunTest");
            expect(name).to.contain("DebugTest");
            expect(measure).to.be.containingAllOf([event.debugCounts, event.runCounts]);
        });

        test('SendTelemetryEvent is not called for empty run count', () => {
            let event = new TestExecutionCountReport({ "framework1": 20 }, null);
            observer.post(event);
            expect(name).to.not.contain("RunTest");
            expect(name).to.contain("DebugTest");
            expect(measure).to.be.containingAllOf([event.debugCounts]);
        });

        test('SendTelemetryEvent is not called for empty debug count', () => {
            let event = new TestExecutionCountReport(null, { "framework1": 20 });
            observer.post(event);
            expect(name).to.contain("RunTest");
            expect(name).to.not.contain("DebugTest");
            expect(measure).to.be.containingAllOf([event.runCounts]);
        });

        test('SendTelemetryEvent is not called for empty debug and run counts', () => {
            let event = new TestExecutionCountReport(null, null);
            observer.post(event);
            expect(name).to.be.empty;
            expect(measure).to.be.empty;
        });
    });
});
