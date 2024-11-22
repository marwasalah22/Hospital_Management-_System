namespace Hospital_Management__System.Servieces.Implementations
{
    public class DoctorServices : IDoctorServices
    {
        private readonly AppDbContext _context;
        public DoctorServices(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.AsNoTracking().Include(n => n.Nurse).ToList();
        }
        public Doctor GetOne(int id)
        {
            if (IsExist(id))
                return _context.Doctors.Include(n => n.Nurse).FirstOrDefault(d => d.DoctorId == id);
            return null;
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
        public IEnumerable<SelectListItem> GetDoctorsByDepartment(string department)
        {
            return _context.Doctors
                .AsNoTracking()
                .Where(n => n.Department == department) // Filter by department
                .Select(n => new SelectListItem
                {
                    Value = n.DoctorId.ToString(),
                    Text = n.DoctorName
                }).ToList();
        }
        public IEnumerable<SelectListItem> GetDoctorsForSelection() // Renamed for clarity
        {
            return _context.Doctors.AsNoTracking()
                .Select(n => new SelectListItem
                {
                    Value = n.DoctorId.ToString(),
                    Text = n.DoctorName
                }).ToList();
        }
        public bool Create(Doctor doctor)
        {
            _context.Doctors.Add(doctor);


            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
        public bool Update(int id, Doctor doctor)
        {
            if (!IsExist(id))
                return false;

            var data = GetOne(id);
            data.DoctorName = doctor.DoctorName;
            data.DoctorEmail = doctor.DoctorEmail;
            data.Department = doctor.Department;
            data.Schedule = doctor.Schedule;
            data.NurseId = doctor.NurseId;

            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
        public bool Delete(int id)
        {
            if (!IsExist(id))
                return false;
            _context.Doctors.Remove(GetOne(id));
            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
        public bool IsExist(int id)
        {
            if (_context.Doctors.Any(d => d.DoctorId == id))
                return true;
            return false;
        }
        public IEnumerable<Nurse> GetNursesByDepartment(string department)
        {
            return _context.Nurses
                .Where(n => n.NurseDepartment == department)
                .Select(n => new Nurse
                {
                    NurseId = n.NurseId,
                    NurseName = n.NurseName,
                    NurseDepartment = n.NurseDepartment
                }).ToList();
        }

    }
}
