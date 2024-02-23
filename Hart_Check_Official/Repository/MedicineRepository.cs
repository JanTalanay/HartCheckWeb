﻿using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly datacontext _context;
        public MedicineRepository(datacontext context)
        {
            _context = context;
        }
        public Medicine GetMedicine(int medicineID)
        {
            return _context.Medicine.Where(e => e.medicineID == medicineID).FirstOrDefault();
        }

        public ICollection<Medicine> GetMedicinesByPatientID(int patientID, int doctorID)
        {
            return _context.Medicine
                .Join(_context.Consultation,
                    medicine => medicine.consultationID,
                    consultation => consultation.consultationID,
                    (medicine, consultation) => new { medicine, consultation })
                .Join(_context.DoctorSchedule,
                    medicineConsultation => medicineConsultation.consultation.doctorSchedID,
                    doctorSchedule => doctorSchedule.doctorSchedID,
                    (medicineConsultation, doctorSchedule) => new { medicineConsultation.medicine, medicineConsultation.consultation, doctorSchedule })
                .Where(x => x.consultation.patientID == patientID && x.doctorSchedule.doctorID == doctorID)
                .Select(x => x.medicine)
                .ToList();
        }

        public ICollection<Medicine> GetMedicines()
        {
            return _context.Medicine.OrderBy(e => e.medicineID).ToList();
        }

        public bool MedicineExist(int medicineID)
        {
            return _context.Medicine.Any(e => e.medicineID == medicineID);
        }
    }
}