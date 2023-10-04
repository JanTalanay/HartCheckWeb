﻿using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class HealthCareClinic
    {
        [Key]
        public int healthCareClinicID { get; set; }
        public int doctorID { get; set; }
        public int clinicID { get; set; }

        public HealthCareProfessional doctor { get; set; }
        public ICollection<Clinic> clinic { get; set; }

    }
}
