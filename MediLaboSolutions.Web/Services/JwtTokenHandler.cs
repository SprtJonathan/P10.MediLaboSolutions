using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MediLaboSolutions.Web.Services;

public class JwtTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTokenHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _httpContextAccessor.HttpContext.Request.Cookies["jwtToken"];
        if (!string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Token ajouté à la requête : " + token);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            Console.WriteLine("Aucun token trouvé dans les cookies");
        }
        return await base.SendAsync(request, cancellationToken);
    }
}