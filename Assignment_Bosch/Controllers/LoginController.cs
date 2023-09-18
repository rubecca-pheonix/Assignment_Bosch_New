using Assignment_Bosch.Models;
using Assignment_Bosch.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_Bosch.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IMessageProducer _messagePublisher;
    private readonly ValidateUserService _validateuserService;

    public LoginController(ILogger<LoginController> logger, IMessageProducer messageProducer,
        ValidateUserService validateUserService)
    {
        _logger = logger;
        _messagePublisher = messageProducer;
        _validateuserService = validateUserService;
    }

    [HttpGet(Name = "GetOtp")]
    public async Task<IActionResult> Get([FromQuery] string username, [FromQuery] string password)
    {
        try
        {
            // Validate the user credentials
           
            var user = await _validateuserService.GetUserData(username, password);

            // Check if user credentials exist in the database
            if (user is null)
            {
                return Unauthorized("Invalid user request!!!");
            }

            // Publish the username to the broker and send the OTP
            _messagePublisher.SendMessage(new LoginNotificationMessage { Username = username });

            return Ok("User Logged In");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "Internal seever error");
        }
    }
}
