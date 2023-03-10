using Core.Configurations;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder app)
            => app.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
