using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VMTS.Core.Entities.Identity.ActivityLog;
using VMTS.Core.Interfaces.Services;

namespace VMTS.API.Controllers;

public class RecentActivityController : BaseApiController
{
    private readonly IRecentActivityService _activityService;

    public RecentActivityController(IRecentActivityService activityService)
    {
        _activityService = activityService;
    }
    
        
    #region get all activity logs
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost]
    public async Task<IReadOnlyList<ActivityLog>> GetActivityLog()
    {
        return await _activityService.GetLogActivities();
    }
    

    #endregion
    
}