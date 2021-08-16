using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerShellModuleTest.Cmdlet;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowerShellModuleTest.UnitTests
{
    [TestClass]
    public class TestMultiplyCmdletCommandTest
    {
        private Runspace _runspace;

        [TestInitialize]
        public void Init()
        {
            var initialSessionState = InitialSessionState.CreateDefault();
            initialSessionState.Commands.Add(
              new SessionStateCmdletEntry("Test-MultiplyCmdlet", typeof(TestMultiplyCmdletCommand), null)
            );

            _runspace = RunspaceFactory.CreateRunspace(initialSessionState);
            _runspace.Open();
        }

        [TestMethod]
        public void Run()
        {
            using var powershell = PowerShell.Create(_runspace);

            // Configure Command
            var command = new Command("Test-MultiplyCmdlet");
            command.Parameters.Add("FirstValue", 4);
            command.Parameters.Add("SecondValue", 8);
            powershell.Commands.AddCommand(command);
            
            // Run Command
            var result = powershell.Invoke<TestMultiplyCmdletCommandResponse>()[0];

            // Assert
            Assert.IsInstanceOfType(result, typeof(TestMultiplyCmdletCommandResponse));
            Assert.AreEqual(result.Result, 32);
        }
    }
}