using System.Security.Claims;
using VMTS.Core.Entities.Maintenace;
using VMTS.Core.Entities.Report;
using VMTS.Core.Specifications.FaultReportSepcification;

namespace VMTS.Core.ServicesContract;

public interface IReportService
{
    Task<FaultReport> CreateFaultReportAsync(
        string userEmail,
        string details,
        MaintenanceType faultType,
        string address);

    Task<IReadOnlyList<FaultReport>> GetAllFaultReportsAsync(FaultReportSpecParams specParams);

    Task<IReadOnlyList<FaultReport>> GetFaultReportsForUserAsync(FaultReportSpecParams specParams , string driverId);

    Task<FaultReport> GetByIdAsync(string id);

}