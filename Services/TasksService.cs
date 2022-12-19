using WorkTracker.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace WorckTracker.Services;

public class TasksService
{
    private readonly IMongoCollection<TaskItem> _tasksCollection;

    public TasksService(
        IOptions<WorkTrackerDatabaseSettings> workTrackerDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            workTrackerDatabaseSettings.Value.ConnectionString);
        
        var mongoDatabase = mongoClient.GetDatabase(
            workTrackerDatabaseSettings.Value.DatabaseName);
        
        _tasksCollection = mongoDatabase.GetCollection<TaskItem>(
            workTrackerDatabaseSettings.Value.TaskItemsCollectionName);
            
    }

    public async Task<List<TaskItem>> GetAsync() =>
        await _tasksCollection.Find(_ => true).ToListAsync();
    
    public async Task<TaskItem?> GetAsync(ObjectId id) =>
        await _tasksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TaskItem newTaskItem) =>
        await _tasksCollection.InsertOneAsync(newTaskItem);

    public async Task UpdateAsync(ObjectId id, TaskItem updatedTaskItem) =>
        await _tasksCollection.ReplaceOneAsync(x => x.Id == id, updatedTaskItem);

    public async Task RemoveAsync(ObjectId id) =>
        await _tasksCollection.DeleteOneAsync (x => x.Id == id);
}