using Microsoft.AspNetCore.Routing;

namespace Engine.Web;
public interface IMinimalEndpoint
{
    IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder);
}
