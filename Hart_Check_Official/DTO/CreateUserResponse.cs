namespace Hart_Check_Official.DTO
{
    public class CreateUserResponse
    {
        public int UsersID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string OTPHash { get; set; }
    }
}
