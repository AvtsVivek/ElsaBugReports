using Elsa.Services;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Elsa.Bug.MultiTenency
{
    public class CustomTenantAccessor : ITenantAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public CustomTenantAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public Task<string?> GetTenantIdAsync(CancellationToken cancellationToken = default)
        {
            //You can customize the data
            var httpContext = _accessor.HttpContext;

            var tenantId = httpContext.Request.Headers["x-tenant"].ToString();

            // If you insert null for tenant id, then things work fine.
            // But if you insert any other string, a simple empty string for that matter, 
            // we get 404 not found for some endpoints.

            // if (string.IsNullOrWhiteSpace(tenantId))
            //    return Task.FromResult<string?>(null);

            // Or you can get tenantid from claim
            //var tenantId = httpContext.User.FindFirstValue("x-tenant");

            return Task.FromResult(tenantId);
        }
    }
}
