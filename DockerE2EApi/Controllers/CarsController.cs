using DockerE2EApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerE2EApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarDataContext _context;

        public CarsController(CarDataContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.Include(c => c.Bookings).ToListAsync();
        }


        // GET: api/Cars/Day/25.09.2000
        [HttpGet("Day/{day}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCarsForDay(string day)
        {
            DateTime date = DateTime.Parse(day);
            return (await _context.Cars.Include(c => c.Bookings).ToListAsync()).Where(c => !c.Bookings.Any(b => b.BookingDate == date)).ToList();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("Book/{id}")]
        public async Task<IActionResult> BookCar(int id, [FromBody] Date date)
        {
            DateTime day = date.DateTime;
            if (!CarExists(id))
            {
                return NotFound();
            }
            Car c = await _context.Cars.Include(c => c.Bookings).FirstAsync(c => c.CarId == id);
            if (c.Bookings.Any(b => b.BookingDate.Date == day))
            {
                return BadRequest("Car already booked at this day");
            }

            c.Bookings.Add(new Booking { BookingDate = day });
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.CarId }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}
