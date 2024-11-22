using Hospital_Management__System.Services.Interfaces;


namespace Hospital_Management__System.Services.Implementations
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly AppDbContext _context;

        public MedicalRecordService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MedicalRecord> GetAll()
        {
            return _context.MedicalRecords.AsNoTracking().Include(d => d.Doctor).ToList();
        }

        public IEnumerable<SelectListItem> SelectedItems()
        {
            return _context.Doctors.AsNoTracking().Select(d => new SelectListItem
            {
                Value = d.DoctorId.ToString(),
                Text = $"{d.DoctorName}"
            }).ToList();
        }

        public MedicalRecord GetOne(int id)
        {
            return _context.MedicalRecords.Include(d => d.Doctor).FirstOrDefault(m => m.MedicalRecordId == id);
        }

        public bool isExist(int id)
        {
            return _context.MedicalRecords.Any(m => m.MedicalRecordId == id);
        }

        public bool Create(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            return _context.SaveChanges() > 0;
        }
        public bool ACreate(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            return _context.SaveChanges() > 0;
        }
        public bool DCreate(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            return _context.SaveChanges() > 0;
        }

        public bool Update(int id, MedicalRecord medicalRecord)
        {
            if (!isExist(id))
                return false;

            // Fetch the record to update without AsNoTracking
            var data = _context.MedicalRecords.FirstOrDefault(m => m.MedicalRecordId == id);
            if (data != null)
            {
                // Update properties
                data.PatiantName = medicalRecord.PatiantName;
                data.PatiantAge = medicalRecord.PatiantAge;
                data.PatiantPhone = medicalRecord.PatiantPhone;
                data.PatiantAddress = medicalRecord.PatiantAddress;
                data.Department = medicalRecord.Department;
                data.Appointment = medicalRecord.Appointment;
                data.DoctorId = medicalRecord.DoctorId;
                data.Description = medicalRecord.Description;
                data.IsFinished = medicalRecord.IsFinished;
                data.PatiantEmailnew = medicalRecord.PatiantEmailnew;

                // No need to call _context.Update(data) because EF tracks changes if the entity is attached
                return _context.SaveChanges() > 0;
            }

            return false;
        }
        public bool DUpdate(int id, MedicalRecord medicalRecord)
        {
            if (!isExist(id))
                return false;

            // Fetch the record to update without AsNoTracking
            var data = _context.MedicalRecords.FirstOrDefault(m => m.MedicalRecordId == id);
            if (data != null)
            {
                // Update properties
                data.PatiantName = medicalRecord.PatiantName;
                data.PatiantAge = medicalRecord.PatiantAge;
                data.PatiantPhone = medicalRecord.PatiantPhone;
                data.PatiantAddress = medicalRecord.PatiantAddress;
                data.Department = medicalRecord.Department;
                data.Appointment = medicalRecord.Appointment;
                data.DoctorId = medicalRecord.DoctorId;
                data.Description = medicalRecord.Description;
                data.IsFinished = medicalRecord.IsFinished;
                data.PatiantEmailnew = medicalRecord.PatiantEmailnew;

                // No need to call _context.Update(data) because EF tracks changes if the entity is attached
                return _context.SaveChanges() > 0;
            }

            return false;
        }

        public bool Delete(int id)
        {
            if (!isExist(id))
                return false;

            var medicalRecord = GetOne(id);
            if (medicalRecord != null)
            {
                _context.MedicalRecords.Remove(medicalRecord);
                return _context.SaveChanges() > 0;
            }

            return false;
        }
        public IEnumerable<SelectListItem> GetDoctorsByDepartment(string department)
        {
            return _context.Doctors
                .Where(d => d.Department.Equals(department, StringComparison.OrdinalIgnoreCase))
                .Select(d => new SelectListItem
                {
                    Value = d.DoctorId.ToString(),
                    Text = d.DoctorName
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetSelectableDoctors() // Renamed for clarity
        {
            return _context.Doctors.AsNoTracking()
                .Select(n => new SelectListItem
                {
                    Value = n.DoctorId.ToString(),
                    Text = n.DoctorName
                }).ToList();
        }


        public IEnumerable<SelectListItem> GetDoctorsForSelection()
        {
            return _context.Doctors.AsNoTracking()
                .Select(n => new SelectListItem
                {
                    Value = n.DoctorId.ToString(),
                    Text = n.DoctorName
                }).ToList();
        }

        public bool CheckDoctorAppointmentConflict(int doctorId, DateTime appointmentDate)
        {
            // Query the database for any appointments for this doctor at the same time
            return _context.MedicalRecords
                .Any(m => m.DoctorId == doctorId && m.Appointment == appointmentDate);
        }

    }
}
