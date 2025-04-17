using System.ComponentModel.DataAnnotations;

namespace VMTS.API.Dtos;

public class AddressDto
{
    public string? Street { get; set; }
    
    public string? Area { get; set; }
    
    public string? Governorate { get; set; }
    
    public string? Country { get; set; }
    
}

