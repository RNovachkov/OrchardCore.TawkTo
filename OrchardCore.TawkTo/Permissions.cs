using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCore.TawkTo
{
    public class Permissions
    {
        public static readonly Permission ManageTawkTo
            = new Permission(nameof(ManageTawkTo), "Manage TawkTo");

        public class TawkTo : IPermissionProvider
        {
            public Task<IEnumerable<Permission>> GetPermissionsAsync()
            {
                return Task.FromResult(new[]
                {
                    ManageTawkTo
                }
                .AsEnumerable());
            }

            public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
            {
                yield return new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[]
                    {
                        ManageTawkTo
                    }
                };
            }
        }
    }
}
