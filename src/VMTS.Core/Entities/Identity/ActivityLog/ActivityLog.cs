namespace VMTS.Core.Entities.Identity.ActivityLog;

public class ActivityLog : BaseEntity
{
        public string Description { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Email { get; set; }

        public string Role { get; set; }
        
        
}