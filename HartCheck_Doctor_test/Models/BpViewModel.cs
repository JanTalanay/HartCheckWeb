using Microsoft.AspNetCore.Mvc.Rendering;

namespace HartCheck_Doctor_test.Models;

public class BpViewModel
{
    public int PatientID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}