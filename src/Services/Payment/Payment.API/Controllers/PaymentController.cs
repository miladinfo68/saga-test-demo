using Microsoft.AspNetCore.Mvc;

namespace Payment.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{

    [HttpGet(Name = "Get")]
    public string Get() => "done";
}
