namespace Hospital_Management__System.Servieces.Implementations
{
    public class NurseServices : INurseServices
    {
        private readonly AppDbContext _context;
        public NurseServices(AppDbContext context)
        {
            _context = context;
        }

        public bool Create(Nurse nurse)
        {
            _context.Nurses.Add(nurse);


            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
        public IEnumerable<Nurse> GetAll()
        {
            return _context.Nurses.AsNoTracking().ToList();
        }

        public IEnumerable<SelectListItem> GetSelectableNurses() // Renamed for clarity
        {
            return _context.Nurses.AsNoTracking()
                .Select(n => new SelectListItem
                {
                    Value = n.NurseId.ToString(),
                    Text = n.NurseName
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetNursesByDepartment(string department)
        {
            return _context.Nurses
                .AsNoTracking()
                .Where(n => n.NurseDepartment == department) // Filter by department
                .Select(n => new SelectListItem
                {
                    Value = n.NurseId.ToString(),
                    Text = n.NurseName
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetNursesForSelection() // Renamed for clarity
        {
            return _context.Nurses.AsNoTracking()
                .Select(n => new SelectListItem
                {
                    Value = n.NurseId.ToString(),
                    Text = n.NurseName
                }).ToList();
        }

        public Nurse GetOne(int id)
        {
            if (IsExist(id))
                return _context.Nurses.FirstOrDefault(n => n.NurseId == id);
            return null;
        }

        public bool Delete(int id)
        {
            if (!IsExist(id))
                return false;
            _context.Nurses.Remove(GetOne(id));
            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }

        public bool IsExist(int id)
        {
            if (_context.Nurses.Any(n => n.NurseId == id))
                return true;
            return false;
        }

        


    }
}