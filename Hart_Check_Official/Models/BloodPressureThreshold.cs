﻿using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class BloodPressureThreshold
    {
        [Key]
        public int thresholdID { get; set; }
        public int patientID { get; set; }
        public int doctorID { get; set; }
        public double systolicLevel { get; set; }
        public double diastolicLevel { get; set; }

        public Patients patients { get; set; }
        public HealthCareProfessional doctor { get; set; }
    }
}
