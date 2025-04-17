using VMTS.Core.Entities.Identity.ActivityLog;
using VMTS.Core.Interfaces.Repositories;
using VMTS.Core.Interfaces.Services;
using VMTS.Core.Interfaces.UnitOfWork;

namespace VMTS.Service.Services;

public class RecentActivityService : IRecentActivityService
{
    private readonly IIdentityGenericRepository<ActivityLog> _activityRepo;

    public RecentActivityService(IIdentityGenericRepository<ActivityLog> activityRepo)
    {
        _activityRepo = activityRepo;
    }


    public async Task LogActivity(string description, string email, string userRole)
    {
        var activity = new ActivityLog
        {
            Email = email,
            Role = userRole,
            Description = description,
            Date = DateTime.Now
        };

        await _activityRepo.CreateAsync(activity);
        await _activityRepo.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<ActivityLog>> GetLogActivities()
    {
        return await _activityRepo.GetAllAsync();
    }
}
