using System;
using System.Linq;
using LightBDD.Core.Configuration;
using LightBDD.Core.Execution.Coordination;
using LightBDD.Core.Extensibility;
using LightBDD.Core.Reporting;
using LightBDD.Framework.Reporting.Configuration;

namespace LightBDD.Integration.MsTest
{
    internal class MsTestFeatureCoordinator : FeatureCoordinator
    {
        public static FeatureCoordinator GetInstance()
        {
            if (Instance == null)
                throw new InvalidOperationException(string.Format("LightBDD integration is not initialized. Please ensure that following class is defined in test assembly: \n[TestClass] class LightBddIntegration\n{{\n    [AssemblyInitialize] public static void Setup(TestContext testContext){{ {0}.{1}(); }}\n    [AssemblyCleanup] public static void Cleanup(){{ {0}.{2}(); }}\n}}", nameof(LightBddScope), nameof(LightBddScope.Initialize), nameof(LightBddScope.Cleanup)));
            if (Instance.IsDisposed)
                throw new InvalidOperationException("LightBdd scenario test execution is already finished. Please ensure that no tests are executed outside of assembly execution scope.");
            return Instance;
        }

        public MsTestFeatureCoordinator(FeatureBddRunnerFactory runnerFactory, IFeatureAggregator featureAggregator) : base(runnerFactory, featureAggregator)
        {
        }

        internal static void InstallSelf(LightBddConfiguration configuration)
        {
            Install(new MsTestFeatureCoordinator(new MsTestFeatureBddRunnerFactory(configuration), new FeatureReportGenerator(configuration.ReportWritersConfiguration().ToArray())));
        }
    }
}