namespace HartCheck_Doctor_test.Models;

public class ConsultationViewModel
{
    public int consultationID { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public long Phone { get; set; }
    
    public DateTime schedDateTime { get; set; }
    
}