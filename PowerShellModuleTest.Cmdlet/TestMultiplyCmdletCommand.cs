using System.Management.Automation;

namespace PowerShellModuleTest.Cmdlet
{
    [Cmdlet(VerbsDiagnostic.Test, "MultiplyCmdlet")]
    [OutputType(typeof(TestMultiplyCmdletCommandResponse))]
    public class TestMultiplyCmdletCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        public int FirstValue { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
        public int SecondValue { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(new TestMultiplyCmdletCommandResponse
            {
                Result = FirstValue * SecondValue
            });
        }
    }

    public class TestMultiplyCmdletCommandResponse
    {
        public int Result { get; set; }
    }
}