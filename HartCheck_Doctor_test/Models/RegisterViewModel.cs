using HartCheck_Doctor_test.DTO;

namespace HartCheck_Doctor_test.Models;

public class RegisterViewModel
{
    public UserDto User { get; set; }
    public DoctorLicenseDTO License { get; set; }
}