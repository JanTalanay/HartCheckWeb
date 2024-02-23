using Microsoft.AspNetCore.Mvc.Rendering;

namespace HartCheck_Doctor_test.Models;

public class BpThresholdViewModel
{
    public int SelectedPatientId { get; set; }
    public IEnumerable<SelectListItem> Patients { get; set; }
    public int SystolicLevel { get; set; }
    public int DiastolicLevel { get; set; }
}