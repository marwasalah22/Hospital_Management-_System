namespace Hospital_Management__System.Servieces.Interfaces
{
    public interface INurseServices
    {
        IEnumerable<Nurse> GetAll();
        Nurse GetOne(int id);
        bool Create(Nurse nurse);
        bool Delete(int id);
        bool IsExist(int id);
        IEnumerable<SelectListItem> GetNursesByDepartment(string department);
        IEnumerable<SelectListItem> GetSelectableNurses();
    }
}
