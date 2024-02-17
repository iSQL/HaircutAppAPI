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
		public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
		{
			return Ok(await _context.GetAppointmentsAsync());
		}

		// POST: api/Appointments
		[HttpPost]
		public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
		{
			await _context.AddAppointmentAsync(appointment);
			return CreatedAtAction("GetAppointments", new { id = appointment.customerId }, appointment);
		}
	}
}
