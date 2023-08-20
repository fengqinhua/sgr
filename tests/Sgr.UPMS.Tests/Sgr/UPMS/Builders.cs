using Sgr.UPMS.Domain.Roles;

namespace Sgr.UPMS
{
    internal class RoleBuilder
    {
        private readonly Role _role;

        public RoleBuilder(string code, string roleName, long orgId, int orderNumber = 0, string? remarks = null)
        {
            _role = new Role(code, roleName, orgId, orderNumber, remarks);
        }

        public RoleBuilder AddOneResource(ResourceType resourceType, string resourceCode)
        {
            _role.AddPermission(resourceType, resourceCode);
            return this;
        }

        public RoleBuilder ResetPermission(ResourceType resourceType)
        {
            _role.ResetPermission(resourceType);
            return this;
        }


        public Role Build()
        {
            return _role;
        }
    }
}
