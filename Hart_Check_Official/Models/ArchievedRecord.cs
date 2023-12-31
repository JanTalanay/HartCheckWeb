﻿using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class ArchievedRecord
    {
        [Key]
        public int archivedRecordID { get; set; }
        public int patientID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime birthdate { get; set; }
        public double systolic { get; set; }
        public double diastolic { get; set; }

        public Patients patients { get; set; }
    }
}
