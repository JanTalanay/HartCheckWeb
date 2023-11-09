namespace HartCheck_Doctor_test.DTO
{
    public class DoctorDetailsAndScheduleDto
    {
        public HealthCareProfessionalDto Doctor { get; set; }
        public HealthCareProfessionalName DoctorName { get; set; }
        public List<DateTime> DoctorSchedule { get; set; }
    }
}
