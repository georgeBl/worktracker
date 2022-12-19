using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.JsonPatch;
using System.Diagnostics;
using WorckTracker.Services;
using WorkTracker.Models;

using MongoDB.Bson;

namespace WorkTracker.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private readonly TasksService _tasksService;

    public TaskItemsController (TasksService tasksService) =>
        _tasksService = tasksService;

    [HttpGet]
    public async Task<List<TaskItem>> Get() =>
        await _tasksService.GetAsync();
    
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TaskItem>> Get(string id)
    {
        var taskItem = await _tasksService.GetAsync(ObjectId.Parse(id));

        if(taskItem is null)
        {
            return NotFound();
        }
        return taskItem;

    }

    [HttpPost]
    public async Task<IActionResult> Post(TaskItem newtaskItem)
    {
        await _tasksService.CreateAsync(newtaskItem);

        return CreatedAtAction(nameof(Get), new { id =newtaskItem.Id}, newtaskItem);
    }

    [HttpPut("{id:length(24)}")]
     public async Task<IActionResult> Update(string id, TaskItem updatedTaskItem)
    {
        var taskItem = await _tasksService.GetAsync(ObjectId.Parse(id));
        
        if(taskItem is null)
        {
            return NotFound();
        }
        updatedTaskItem.Id = taskItem.Id;

        await _tasksService.UpdateAsync(ObjectId.Parse(id), updatedTaskItem);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var taskItem = await _tasksService.GetAsync(ObjectId.Parse(id));
        if (taskItem is null)
        {
            return NotFound();
        }

        await _tasksService.RemoveAsync(ObjectId.Parse(id));

        return NoContent();
        
    }

}
















    // [Route("api/TaskItems")]
    // [ApiController]
    // public class TaskItemsController : ControllerBase
    // {
    //     private readonly TaskContext _context;

    //     public TaskItemsController(TaskContext context)
    //     {
    //         _context = context;
    //     }

    //     // GET: api/TaskItems
    //     [HttpGet]
    //     public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
    //     {
    //       if (_context.TaskItems == null)
    //       {
    //           return NotFound();
    //       }
    //         return await _context.TaskItems.ToListAsync();
    //     }

    //     // GET: api/TaskItems/5
    //     [HttpGet("{id}")]
    //     public async Task<ActionResult<TaskItem>> GetTaskItem(long id)
    //     {
    //       if (_context.TaskItems == null)
    //       {
    //           return NotFound();
    //       }
    //         var taskItem = await _context.TaskItems.FindAsync(id);

    //         if (taskItem == null)
    //         {
    //             return NotFound();
    //         }

    //         return taskItem;
    //     }

    //     // PUT: api/TaskItems/5
    //     // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //     [HttpPut("{id}")]
    //     public async Task<IActionResult> PutTaskItem(string id, TaskItem taskItem)
    //     {
    //         if (id != taskItem.Id)
    //         {
    //             return BadRequest();
    //         }

    //         _context.Entry(taskItem).State = EntityState.Modified;

    //         try
    //         {
    //             await _context.SaveChangesAsync();
    //         }
    //         catch (DbUpdateConcurrencyException)
    //         {
    //             if (!TaskItemExists(id))
    //             {
    //                 return NotFound();
    //             }
    //             else
    //             {
    //                 throw;
    //             }
    //         }

    //         return NoContent();
    //     }
    //     //PATCH: api/TaskItems
    //     // how to use:
    //     //  [
    //     //     {
    //     //         "op":"replace",
    //     //         "path":"secret",
    //     //         "value":"new secret baby!!!!"
    //     //     }
    //     //  ]
    //     [HttpPatch("{id}")]
    //     public async Task<IActionResult> PatchTaskItem(string id, JsonPatchDocument taskItemJSON)
    //     {
    //         var task = await _context.TaskItems.FindAsync(id);
    //         if (task == null)
    //         {
    //             return BadRequest();
    //         }
    //         try{
    //             taskItemJSON.ApplyTo(task);
    //             }
    //         catch(Exception){
    //             return Content("Something went wrong");
    //         }
    //         await _context.SaveChangesAsync();
          
            
    //         return NoContent();
            
    //     }

    //     // POST: api/TaskItems
    //     // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //     [HttpPost]
    //     public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
    //     {
    //       if (_context.TaskItems == null)
    //       {
    //           return Problem("Entity set 'TaskContext.TaskItems'  is null.");
    //       }
    //         _context.TaskItems.Add(taskItem);
    //         await _context.SaveChangesAsync();

    //         // return CreatedAtAction("GetTaskItem", new { id = taskItem.Id }, taskItem);
    //         return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem);
    //     }

    //     // DELETE: api/TaskItems/5
    //     [HttpDelete("{id}")]
    //     public async Task<IActionResult> DeleteTaskItem(string id)
    //     {
    //         if (_context.TaskItems == null)
    //         {
    //             return NotFound();
    //         }
    //         var taskItem = await _context.TaskItems.FindAsync(id);
    //         if (taskItem == null)
    //         {
    //             return NotFound();
    //         }

    //         _context.TaskItems.Remove(taskItem);
    //         await _context.SaveChangesAsync();

    //         return NoContent();
    //     }

    //     private bool TaskItemExists(string id)
    //     {
    //         return (_context.TaskItems?.Any(e => e.Id == id)).GetValueOrDefault();
    //     }
    // }

