
namespace Hospital_Management__System.Servieces.Interfaces
{
    public interface IDoctorServices
    {
        IEnumerable<Doctor> GetAll();
        Doctor GetOne(int id);
        bool Create(Doctor doctor);
        bool Update(int id, Doctor doctor);
        bool Delete(int id);
        bool IsExist(int id);
        IEnumerable<SelectListItem> GetDoctorsByDepartment(string department);
        IEnumerable<SelectListItem> GetSelectableDoctors();
    }
}
