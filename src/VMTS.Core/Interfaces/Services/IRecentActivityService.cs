using VMTS.Core.Entities.Identity.ActivityLog;

namespace VMTS.Core.Interfaces.Services;

public interface IRecentActivityService
{
    Task LogActivity(string description, string userName, string userRole);

    Task<IReadOnlyList<ActivityLog>> GetLogActivities();
}