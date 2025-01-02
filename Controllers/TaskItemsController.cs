using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Dto;


namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            try
            {
                var taskItems = await _context.TaskItems.ToListAsync();
                return Ok(taskItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/TaskItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            try
            {
                var taskItem = await _context.TaskItems.FindAsync(id);

                if (taskItem == null)
                {
                    return NotFound();
                }

                return Ok(taskItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/TaskItems

        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem([FromBody] TaskItem taskItem)
        {
            try
            {
                // Check if the UserId exists in the User table
                var user = await _context.Users.FindAsync(taskItem.UserId);

                if (user == null)
                {
                    return BadRequest("User not found");
                }

                // Check if the title and description exist
                if (string.IsNullOrEmpty(taskItem.Title) || string.IsNullOrEmpty(taskItem.Description))
                {
                    return BadRequest("Title and description are required");
                }

                _context.TaskItems.Add(taskItem);
                await _context.SaveChangesAsync();

                return Ok("Task Created Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PATCH: api/TaskItems/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTaskItem(int id, [FromBody] TaskItemDTO taskItemDto)
        {
            try
            {
                var existingTaskItem = await _context.TaskItems.FindAsync(id);
                if (existingTaskItem == null)
                {
                    return NotFound("Task Not Found");
                }

                // Update only the fields that are provided in the request
                if (!string.IsNullOrEmpty(taskItemDto.Title))
                {
                    existingTaskItem.Title = taskItemDto.Title;
                }
                if (!string.IsNullOrEmpty(taskItemDto.Description))
                {
                    existingTaskItem.Description = taskItemDto.Description;
                }
                if (taskItemDto.IsComplete.HasValue)
                {
                    existingTaskItem.IsComplete = taskItemDto.IsComplete.Value;
                }
                if (taskItemDto.IsImportant.HasValue)
                {
                    existingTaskItem.IsImportant = taskItemDto.IsImportant.Value;
                }
                if (taskItemDto.DueDate.HasValue)
                {
                    existingTaskItem.DueDate = taskItemDto.DueDate.Value;
                }

                _context.Entry(existingTaskItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Task updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: api/TaskItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            try
            {
                var taskItem = await _context.TaskItems.FindAsync(id);
                if (taskItem == null)
                {
                    return NotFound();
                }

                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync();

                return Ok("Task deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool TaskItemExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
        
    }
}
