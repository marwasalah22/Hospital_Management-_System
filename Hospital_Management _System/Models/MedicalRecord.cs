using Microsoft.VisualBasic;

namespace Hospital_Management__System.Models
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }
        public string? PatiantEmailnew { get; set; }

        [Display(Name = "Patiant Name")]
        public string PatiantName { get; set; }
        [Display(Name = "Patiant Age")]
        public int PatiantAge { get; set; }
        [Display(Name = "Patiant Address")]
        public string PatiantAddress { get; set; }
        [Display(Name = "Patiant Phone")]
        public string PatiantPhone { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public DateTime Appointment { get; set; }
        [Display(Name = "Appointment Finished")]
        public bool IsFinished { get; set; }
        public int DoctorId { get; set; }
        [Display(Name = "Doctor Name")]
        public Doctor Doctor { get; set; }
    }
}
