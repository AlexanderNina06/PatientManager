namespace PatientMgmt.Core.Domain;

public class TestResult
{
public string Result { get; set; }
public ApptAndResultStatus ResultStatus { get; set; }

//Navegation Properties
public int PatientId { get; set;}
public Patient patient { get; set; }

public int LabTestId { get; set; }
public LabTest labTest{ get; set; }


}
