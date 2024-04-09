using Microsoft.AspNetCore.Identity;

namespace SolarPowerAPI.Mock
{
    public class RolesMock
    {
        string readerRoleId = "7fcabc58-b587-45f2-a220-60144140e5fe";
        string writerRoleId = "6527c83e-b7d4-4e80-83a0-02a22bc95443";

        public List<IdentityRole> CreateRolesMock()
        {
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            return roles;
        }
    }
}
