namespace Hospital_Management__System.Services.Interfaces
{
    public interface IMedicalRecordService
    {
        IEnumerable<MedicalRecord> GetAll();

        IEnumerable<SelectListItem> SelectedItems();

        MedicalRecord GetOne(int id);

        bool Create(MedicalRecord medicalRecord);
        bool ACreate(MedicalRecord medicalRecord);
        bool DCreate(MedicalRecord medicalRecord);

        bool Update(int id, MedicalRecord medicalRecord);
        bool DUpdate(int id, MedicalRecord medicalRecord);

        bool Delete(int id);

        bool isExist(int id);
        //IEnumerable<MedicalRecord> Search(string name);

        // New method to get doctors by department
        IEnumerable<SelectListItem> GetDoctorsByDepartment(string department);

        bool CheckDoctorAppointmentConflict(int doctorId, DateTime appointmentDate);
    }
}

