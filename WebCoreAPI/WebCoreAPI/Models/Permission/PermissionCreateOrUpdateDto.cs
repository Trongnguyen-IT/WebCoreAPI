namespace WebCoreAPI.Models.Permission
{
    public record PermissionCreateOrUpdateDto(int RoleId, List<string> Permissions);
}
