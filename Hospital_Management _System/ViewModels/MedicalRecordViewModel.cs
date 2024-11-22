namespace Hospital_Management__System.ViewModels
{
    public class MedicalRecordViewModel
    {
        [Required(ErrorMessage = "Patient Name is required")]
        [Display(Name = "Patient Name")]
        public string PatiantName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Patient Age is required")]
        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120")]
        [Display(Name = "Age")]
        public int PatiantAge { get; set; }

        [Display(Name = "Address")]
        public string PatiantAddress { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Phone")]

        public string PatiantPhone { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Appointment Date is required")]
       
        [Display(Name = "Appointment Date")]
        [FutureDate]
        public DateTime Appointment { get; set; }

        [Display(Name = "Appointment Finished")]
        public bool IsFinished { get; set; }

        [Required(ErrorMessage = "Doctor is required")]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public string Department { get; set; }
    }
}
