using Microsoft.AspNetCore.Mvc;
using OtoKiralama.Application.Dtos.Reservation;
using OtoKiralama.Application.Interfaces;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllReservations(int pageNumber = 1, int pageSize = 10)
        {
            var reservations = await _reservationService.GetAllReservationsAsync(pageNumber, pageSize);
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            return Ok(await _reservationService.GetReservationByIdAsync(id));
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateReservation(ReservationCreateDto reservationCreateDto)
        {
            await _reservationService.CreateReservationAsync(reservationCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationById(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPatch("cancel/{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            await _reservationService.CancelReservation(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPatch("complete/{id}")]
        public async Task<IActionResult> CompleteReservation(int id)
        {
            await _reservationService.CompleteReservation(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet("FindReservation")]
        public async Task<IActionResult> GetReservationByReservationNumberAndEmail(string reservationNumber, string email)
        {
            var reservation = await _reservationService.GetReservationByReservationNumberAndEmail(reservationNumber, email);
            return Ok(reservation);
        }
    }
}
