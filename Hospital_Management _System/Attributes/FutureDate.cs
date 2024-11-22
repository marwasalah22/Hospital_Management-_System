
using Hospital_Management__System.Services.Interfaces;


namespace Hospital_Management__System.ValidationAttributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime appointmentDate)
            {
                // 1. Check if the appointment date is in the future
                if (appointmentDate <= DateTime.Now)
                {
                    return new ValidationResult("Appointment date must be in the future.");
                }

                // 2. Get the required services (e.g., medical record service or database context)
                var _medicalRecordService = validationContext.GetService<IMedicalRecordService>();

                // Get the DoctorId and the patient name (assuming they're part of the ViewModel)
                var medicalRecordViewModel = (MedicalRecordViewModel)validationContext.ObjectInstance;
                var doctorId = medicalRecordViewModel.DoctorId;

                // 3. Check if the doctor has an appointment at this time
                var isConflict = _medicalRecordService.CheckDoctorAppointmentConflict(doctorId, appointmentDate);
                if (isConflict)
                {
                    return new ValidationResult("The selected appointment date and time is already booked for this doctor.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
