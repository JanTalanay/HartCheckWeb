namespace HartCheck_Doctor_test.Models;

public class PatientDoctorViewModel
{
    public int PatientID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public long Phone { get; set; }
    
    public int DiagnosisID  {get; set; }
    public string Diagnosis {get; set; }
    
    public int DoctorScheduleID { get; set; }
    
    public DateTime SchedDateTime { get; set; }
    
    public string CurrentCondition { get; set; }
    
    public string PreviousCondition { get; set; }
    
    public string PreviousMedications { get; set; }
    
    public string SurgicalHistory { get; set; }
}