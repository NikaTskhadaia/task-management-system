using Microsoft.AspNetCore.Builder;

namespace TMS.Application.Exceptions
{
    public static class Extension
    {
        public static void AddCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
