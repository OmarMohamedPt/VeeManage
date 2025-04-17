namespace VMTS.API.Dtos;

public class ChangePasswordDto
{
    public bool MustChangePassword { get; set; }

    public string Message { get; set; } = "reset password required";
}