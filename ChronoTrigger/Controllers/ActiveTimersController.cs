using ChronoTrigger.Data;
using ChronoTrigger.DTO;
using ChronoTrigger.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ChronoTrigger.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowAll")]
public class ActiveTimersController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActiveTimersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/activetimers -cronometros e atividades
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActiveTimerResponseDto>>> GetActiveTimers()
    {
        var timers = await _context.ActiveTimers
            .Include(t => t.Activity)
            .Select(t => new ActiveTimerResponseDto
            {
                ID = t.ID,
                ActivityID = t.ActivityID,
                ActivityName = t.Activity != null ? t.Activity.Name : "N/A",
                StartTime = t.StartTime,
                AccumulatedSeconds = t.AccumulatedSeconds,
                IsPaused = t.IsPaused,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync();

        return Ok(timers);
    }

    // POST: api/activetimers/start - 
    [HttpPost("start")]
    public async Task<ActionResult<ActiveTimerResponseDto>> StartTimer(
        [FromBody] StartTimerRequestDto request)
    {
        var activity = await _context.Activities
            .FindAsync(request.ActivityId);

        if (activity == null)
        {
            return NotFound($"Activity with ID {request.ActivityId} not found.");
        }

        var now = DateTime.UtcNow;

        // Verificar  se atividade tem um timer / pausar quando outro começar
        var existingTimer = await _context.ActiveTimers
            .FirstOrDefaultAsync(t => t.ActivityID == request.ActivityId);

        if (existingTimer != null)
        {
           
            if (existingTimer.IsPaused)
            {
                var runningTimers = await _context.ActiveTimers
                    .Where(t => !t.IsPaused && t.ID != existingTimer.ID)
                    .ToListAsync();

                foreach (var runningTimer in runningTimers)
                {
                    var elapsed =
                        (int)(now - runningTimer.UpdatedAt).TotalSeconds;

                    runningTimer.AccumulatedSeconds += Math.Max(0, elapsed);
                    runningTimer.IsPaused = true;
                    runningTimer.UpdatedAt = now;
                }

                existingTimer.IsPaused = false;
                existingTimer.UpdatedAt = now;

                await _context.SaveChangesAsync();

                return Ok(new ActiveTimerResponseDto
                {
                    ID = existingTimer.ID,
                    ActivityID = existingTimer.ActivityID,
                    ActivityName = activity.Name,
                    StartTime = existingTimer.StartTime,
                    AccumulatedSeconds = existingTimer.AccumulatedSeconds,
                    IsPaused = existingTimer.IsPaused,
                    UpdatedAt = existingTimer.UpdatedAt
                });
            }

            return BadRequest("Atividade já está em andamento.");
        }

        // pausar timers ativos
        var currentTimers = await _context.ActiveTimers
            .Where(t => !t.IsPaused)
            .ToListAsync();

        foreach (var timer in currentTimers)
        {
            var elapsed =
                (int)(now - timer.UpdatedAt).TotalSeconds;

            timer.AccumulatedSeconds += Math.Max(0, elapsed);
            timer.IsPaused = true;
            timer.UpdatedAt = now;
        }

        // criar novo cronômetro.
        var newTimer = new ActiveTimer
        {
            ActivityID = request.ActivityId,
            StartTime = now.TimeOfDay,
            AccumulatedSeconds = 0,
            IsPaused = false,
            UpdatedAt = now
        };

        _context.ActiveTimers.Add(newTimer);

        await _context.SaveChangesAsync();

        var response = new ActiveTimerResponseDto
        {
            ID = newTimer.ID,
            ActivityID = newTimer.ActivityID,
            ActivityName = activity.Name,
            StartTime = newTimer.StartTime,
            AccumulatedSeconds = newTimer.AccumulatedSeconds,
            IsPaused = newTimer.IsPaused,
            UpdatedAt = newTimer.UpdatedAt
        };

        return Ok(response);
    }

    // PUT: api/activetimers/{id}/toggle-pause - pausar
    [HttpPut("{id}/toggle-pause")]
    public async Task<IActionResult> TogglePause(int id)
    {
        var timer = await _context.ActiveTimers
            .Include(t => t.Activity)
            .FirstOrDefaultAsync(t => t.ID == id);

        if (timer == null)
        {
            return NotFound();
        }

        var now = DateTime.UtcNow;

        // PAUSE
        if (!timer.IsPaused)
        {
            var elapsed =
                (int)(now - timer.UpdatedAt).TotalSeconds;

            timer.AccumulatedSeconds += Math.Max(0, elapsed);
            timer.IsPaused = true;
            timer.UpdatedAt = now;
        }
        // retornar
        else
        {
            var runningTimers = await _context.ActiveTimers
                .Where(t => !t.IsPaused && t.ID != timer.ID)
                .ToListAsync();

            foreach (var runningTimer in runningTimers)
            {
                var elapsed =
                    (int)(now - runningTimer.UpdatedAt).TotalSeconds;

                runningTimer.AccumulatedSeconds += Math.Max(0, elapsed);
                runningTimer.IsPaused = true;
                runningTimer.UpdatedAt = now;
            }

            timer.IsPaused = false;
            timer.UpdatedAt = now;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/activetimers/{id}/stop
    [HttpDelete("{id}/stop")]
    public async Task<IActionResult> StopTimer(
    int id,
    [FromQuery] int hourlyRateId,
    [FromQuery] string? notes)
    {
        Console.WriteLine($"=== STOP 1 | ID: {id} | RATE: {hourlyRateId} ===");

        var timer = await _context.ActiveTimers
            .Include(t => t.Activity)
            .FirstOrDefaultAsync(t => t.ID == id);

        Console.WriteLine(
            $"=== STOP 2 | TIMER: {(timer == null ? "NULL" : "ENCONTRADO")} ===");

        if (timer == null)
        {
            return NotFound($"Active timer with ID {id} was not found.");
        }

        var now = DateTime.UtcNow;

        var totalSeconds = timer.AccumulatedSeconds;

        if (!timer.IsPaused)
        {
            var elapsed =
                (int)(now - timer.UpdatedAt).TotalSeconds;

            totalSeconds += Math.Max(0, elapsed);
        }

        var startTime = now.AddSeconds(-totalSeconds);

        var timeEntry = new TimeEntry
        {
            ActivityID = timer.ActivityID,
            HourlyRateID = hourlyRateId,
            StartTime = startTime,
            EndTime = now,
            CreatedAt = now,
            Notes = notes
        };

        _context.TimeEntries.Add(timeEntry);

        timer.Activity.IsActive = false;

        _context.TimeEntries.Add(timeEntry);
        _context.ActiveTimers.Remove(timer);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
