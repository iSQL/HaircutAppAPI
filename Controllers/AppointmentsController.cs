using HaircutAppAPI.Models;
using HaircutAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HaircutAppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppointmentsController : ControllerBase
	{
		private readonly CosmosDbContext _context;

		public AppointmentsController(CosmosDbContext context)
		{
			_context = context;
		}

		// GET: api/Appointments
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Reservation>>> GetAppointments()
		{
			return Ok(await _context.GetAppointmentsAsync());
		}

		// POST: api/Appointments
		[HttpPost]
		public async Task<ActionResult<Reservation>> PostAppointment(Reservation appointment)
		{
			await _context.AddAppointmentAsync(appointment);
			return CreatedAtAction("GetAppointments", new { appointment.id }, appointment);
		}

		[HttpDelete("DeleteAll")]
		public async Task<IActionResult> DeleteAllAppointments()
		{
			await _context.DeleteAllAppointmentsAsync();
			return NoContent(); // 204 No Content is appropriate for a delete action
		}
	}
}
