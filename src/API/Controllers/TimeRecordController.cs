using Application.Interfaces;
using Domain.Requests;
using Infra.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TimeRecordController : ControllerBase
    {

        [HttpPost("RecordTime")]
        public async Task<IActionResult> RecordTime([FromServices] ITimeRecordUseCase useCase)
        {
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await useCase.RecordTime(employeeId);

            return Ok(result);
        }

        [HttpGet("RecordTime")]
        public async Task<IActionResult> GetRecordTime([FromServices] ITimeRecordUseCase useCase, [FromQuery] Filter filter)
        {
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await useCase.GetRecordTime(employeeId, filter);

            return Ok(result);
        }

        [HttpGet("GenerateEmail")]
        public async Task<IActionResult> GenerateEmail([FromServices] ITimeRecordUseCase useCase, [FromQuery] Filter filter)
        {
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await useCase.GenerateRecordTimeEmail(employeeId, filter);

            EmailService.SendSimpleMessage($"Relatório {filter.Month}/{filter.Year} - Funcionário: {employeeId}", result);

            return Ok(result);
        }
    }
}
