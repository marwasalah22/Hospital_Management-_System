namespace Hospital_Management__System.Models
{
    public class Notification 
    {
        public int NotificationId { get; set; }
        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; } 

    }
    
    
}
