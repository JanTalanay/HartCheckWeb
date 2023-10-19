using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using System;

namespace Hart_Check_Official.Repository
{
    public class MedicalHistoryRepository : IMedicalHistoryRepository
    {
        private readonly datacontext _context;
        public MedicalHistoryRepository(datacontext context)
        {
            _context = context;
        }
        public ICollection<MedicalHistory> GetMedicalHistories()
        {
            return _context.MedicalHistory.OrderBy(e => e.medicalHistoryID).ToList();
        }

        public MedicalHistory GetMedicalHistory(int medicalHistoryID)
        {
            return _context.MedicalHistory.Where(e => e.medicalHistoryID == medicalHistoryID).FirstOrDefault();
        }

        public MedicalHistory GetMedicalHistoryPatientID(int patientID)
        {
            return _context.MedicalHistory.Where(e => e.patientID == patientID).FirstOrDefault();
        }

        public bool medHistoryExsistPatientID(int patientID)
        {
            return _context.MedicalHistory.Any(e => e.patientID == patientID);
        }


        public bool medHistoryExsist(int medicalHistoryID)
        {
            return _context.MedicalHistory.Any(e => e.medicalHistoryID == medicalHistoryID);
        }



        public bool UpdatemedHistory(MedicalHistory medicalHistory)
        {
            _context.Update(medicalHistory);
            return Save();
        }
        public MedicalHistory CreatemedHistory(MedicalHistory medicalHistory)
        {
            _context.Add(medicalHistory);
            _context.SaveChanges();
            return (medicalHistory);
        }

        public bool DeletemedHistory(MedicalHistory medicalHistory)
        {
            _context.Remove(medicalHistory);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
