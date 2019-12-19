using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace YTS.Data.Models.WebApi
{
    [ApiController]
    [Route(ApiConfig.APIRoute)]
    [EnableCors("AllowSameDomain")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}