using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerShellModuleTest.Cmdlet;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowerShellModuleTest.UnitTests
{
    [TestClass]
    public class TestSampleCmdletCommandTest
    {
        private Runspace _runspace;

        [TestInitialize]
        public void Init()
        {
            var initialSessionState = InitialSessionState.CreateDefault();
            initialSessionState.Commands.Add(
              new SessionStateCmdletEntry("Test-SampleCmdlet", typeof(TestSampleCmdletCommand), null)
            );

            _runspace = RunspaceFactory.CreateRunspace(initialSessionState);
            _runspace.Open();
        }

        [TestMethod]
        public void Run()
        {
            using var powershell = PowerShell.Create(_runspace);

            // Configure Command
            var command = new Command("Test-SampleCmdlet");
            command.Parameters.Add("FavoriteNumber", 51);
            command.Parameters.Add("FavoritePet", "Cat");
            powershell.Commands.AddCommand(command);
            
            // Run Command
            var result = powershell.Invoke<FavoriteStuff>()[0];

            // Assert
            Assert.IsInstanceOfType(result, typeof(FavoriteStuff));
            Assert.AreEqual(result.FavoriteNumber, 51);
            Assert.AreEqual(result.FavoritePet, "Cat");
        }
    }
}