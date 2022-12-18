namespace WorkTracker.Models;

public class WorkTrackerDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string TaskItemsCollectionName { get; set; } = null!;
}