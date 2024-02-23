namespace Hart_Check_Official.DTO
{
    public class UserDto
    {
        public int usersID { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public DateTime birthdate { get; set; }
        public int gender { get; set; }
        public bool isPregnant { get; set; }
        public long phoneNumber { get; set; }
        public int role { get; set; }
    }
}
