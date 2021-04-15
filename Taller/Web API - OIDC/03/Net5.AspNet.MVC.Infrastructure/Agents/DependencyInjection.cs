using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Net5.AspNet.MVC.Infrastructure.Agents.Comments;
using Net5.AspNet.MVC.Infrastructure.Agents.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Agents
{
    public static class DependencyInjection
    {
        public class BlogRepositoriesOptions
        {
            public string UrlService { get; set; }
        }
        public static IServiceCollection AddAgents(this IServiceCollection services, Action<BlogRepositoriesOptions> configureOptions)
        {
            var options = new BlogRepositoriesOptions();
            configureOptions(options);

            services.AddHttpClient<IPostAgent, PostsAgent>(
                async (services, client) =>
                {
                    var accessor = services.GetRequiredService<IHttpContextAccessor>();
                    var accessToken = await accessor.HttpContext.GetTokenAsync("access_token");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                    client.BaseAddress = new Uri(options.UrlService);
                });

            services.AddHttpClient<ICommentsAgent, CommentsAgent>(
                async (services, client) =>
                {
                    var accessor = services.GetRequiredService<IHttpContextAccessor>();
                    var accessToken = await accessor.HttpContext.GetTokenAsync("access_token");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                    client.BaseAddress = new Uri(options.UrlService);
                });

            return services;
        }
    }
}
