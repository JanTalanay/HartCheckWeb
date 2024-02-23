namespace HartCheck_Doctor_test.DTO;

public class ConditionDto
{
    public int patientID { get; set; }
    public int consultationID { get; set; }
    //public string Condition { get; set; }
    
    public List<string> Condition { get; set; } = new List<string>();
}