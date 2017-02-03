﻿using System.Threading.Tasks;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.Scenarios.Extended.UnitTests.Helpers;
using NUnit.Framework;

namespace LightBDD.Scenarios.Extended.UnitTests
{
    [TestFixture]
    public class Parameterized_scenario_runner_execution_flow_tests : ParameterizedScenariosTestBase<NoContext>
    {
        [Test]
        public void It_should_allow_to_run_synchronous_scenarios()
        {
            ExpectSynchronousScenarioRun();

            Runner.RunScenario(
                _ => Step_one(),
                _ => Step_two());

            MockRunner.Verify();
            MockScenarioRunner.Verify();
        }

        [Test]
        public async Task It_should_allow_to_run_asynchronous_scenarios()
        {
            ExpectAsynchronousScenarioRun();

            await Runner.RunScenarioAsync(
                _ => Step_one_async(),
                _ => Step_two_async());

            MockRunner.Verify();
            MockScenarioRunner.Verify();
        }
    }
}
