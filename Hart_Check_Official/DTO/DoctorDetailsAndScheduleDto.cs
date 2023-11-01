namespace Hart_Check_Official.DTO
{
    public class DoctorDetailsAndScheduleDto
    {
        public HealthCareProfessionalDto Doctor { get; set; }
        public HealthCareProfessionalName DoctorName { get; set; }
        public List<DateTime> DoctorSchedule { get; set; }
    }
}
