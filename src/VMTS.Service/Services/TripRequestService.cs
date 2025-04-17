using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using VMTS.Core.Entities.Identity;
using VMTS.Core.Entities.Maintenace;
using VMTS.Core.Entities.Report;
using VMTS.Core.Entities.Trip;
using VMTS.Core.Entities.Vehicle_Aggregate;
using VMTS.Core.Interfaces.UnitOfWork;
using VMTS.Core.ServicesContract;
using VMTS.Core.Specifications.VehicleSpecification;

namespace VMTS.Service.Services;

public class TripRequestService : ITripRequestService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public TripRequestService(UserManager<AppUser> userManager,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<TripRequest> CreateTripRequestAsync(string managerId,
                                                          string driverId,
                                                          string vehicleId,
                                                          TripType tripType,
                                                          string details,
                                                          string destination
                                                          )
    {
        

        
        // Get vehicle
        var vehicleSpec = new VehicleIncludesSpecification(vehicleId);
        var vehicle = await _unitOfWork.GetRepo<Vehicle>().GetByIdWithSpecification(vehicleSpec);
        if (vehicle is null) throw new InvalidOperationException("Vehicle not found");

        // Create TripRequest
        var tripRequest = new TripRequest
        {
            Id = Guid.NewGuid().ToString(),
            Type = tripType,
            Destination = destination,
            Details = details,
            Date = DateTime.UtcNow,
            Status = TripStatus.Pending,
            DriverId = driverId,
            ManagerId = managerId,
            VehicleId = vehicleId
        };
           

        // Save to database
        await _unitOfWork.GetRepo<TripRequest>().CreateAsync(tripRequest);
        var result = await _unitOfWork.CompleteAsync();

        return result > 0 ? tripRequest : null;
    }

}