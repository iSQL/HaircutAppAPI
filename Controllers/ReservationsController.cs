using HaircutAppAPI.Models;
using HaircutAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HaircutAppAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservationsController : ControllerBase
	{
		private readonly CosmosDbContext _context;

		public ReservationsController(CosmosDbContext context)
		{
			_context = context;
		}

		// GET: api/Reservations
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
		{
			return Ok(await _context.GetReservationAsync());
		}

		// POST: api/Reservations
		[HttpPost]
		public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
		{
			await _context.AddReservationAsync(reservation);
			return CreatedAtAction("GetReservations", new { reservation.id }, reservation);
		}

		[HttpDelete("DeleteAll")]
		public async Task<IActionResult> DeleteAllReservations()
		{
			await _context.DeleteAllReservationsAsync();
			return NoContent(); // 204 No Content is appropriate for a delete action
		}
	}
}
