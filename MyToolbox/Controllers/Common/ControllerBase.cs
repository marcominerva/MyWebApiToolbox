using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace MyToolbox.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{

}
