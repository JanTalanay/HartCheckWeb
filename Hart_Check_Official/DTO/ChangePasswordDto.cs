namespace Hart_Check_Official.DTO
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string OtpHash { get; set; }
        public string NewPassword { get; set; }
    }
}
