namespace Hospital_Management__System.Models
{
    public class Doctor 
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorEmail { get; set; }
        public string Department { get; set; }
        public string Schedule { get; set; }
        public int NurseId { get; set; }
        public Nurse Nurse { get; set; }

    }
}
