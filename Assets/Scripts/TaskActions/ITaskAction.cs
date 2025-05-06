
namespace TaskAction
{
    /// <summary>
    /// This is an interface for the task actions strategy pattern
    /// </summary>
    public interface ITaskAction
    {
        public void ExecuteTask();
        public void StopTask();

    }
}

