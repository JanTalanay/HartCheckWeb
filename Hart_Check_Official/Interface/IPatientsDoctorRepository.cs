﻿using Hart_Check_Official.DTO;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IPatientsDoctorRepository
    {
        ICollection<PatientsDoctor> GetPatientsDoctors();

        PatientsDoctor GetPatientsDoctor(int patientID);

        bool PatientsDoctorExist(int patientDoctorID);

        List<HealthCareProfessionalName> GetHealthCareProfessionals(int patientID);

        PatientsDoctor CreatePatientsDoctor(PatientsDoctor PatientsDoctor);
        bool DeletepPatientsDoctor(PatientsDoctor PatientsDoctor);
        bool UpdatePatientsDoctort(PatientsDoctor PatientsDoctor);

        bool Save();
    }
}
