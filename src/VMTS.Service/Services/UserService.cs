using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VMTS.Core.Entities.Identity;
using VMTS.Core.Interfaces.Services;

namespace VMTS.Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    // ✅ Constructor injection for _userManager and _mapper
    public UserService(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<bool> EditUserAsync(
        string userId,
        string? userName,
        string? phoneNumber,
        string? street,
        string? area,
        string? governorate,
        string? country,
        string? role)
    {
        var user = await _userManager.Users
            .Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return false;

        // Update basic fields
        if (!string.IsNullOrWhiteSpace(userName))
            user.UserName = userName;

        if (!string.IsNullOrWhiteSpace(phoneNumber))
            user.PhoneNumber = phoneNumber;

        // Update or create address if any address field is sent
        if (!string.IsNullOrWhiteSpace(street) ||
            !string.IsNullOrWhiteSpace(area) ||
            !string.IsNullOrWhiteSpace(governorate) ||
            !string.IsNullOrWhiteSpace(country))
        {
            if (user.Address == null)
            {
                user.Address = new Address
                {
                    Street = street,
                    Area = area,
                    Governorate = governorate,
                    Country = country,
                    AppUserId = user.Id
                };
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(street))
                    user.Address.Street = street;

                if (!string.IsNullOrWhiteSpace(area))
                    user.Address.Area = area;

                if (!string.IsNullOrWhiteSpace(governorate))
                    user.Address.Governorate = governorate;

                if (!string.IsNullOrWhiteSpace(country))
                    user.Address.Country = country;
            }
        }

        // Role update
        if (!string.IsNullOrWhiteSpace(role))
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
                return false;
        }

        var updateResult = await _userManager.UpdateAsync(user);
        return updateResult.Succeeded;
    }
}
