using InventorSoftTestApp.Domain.Jobs.Abstractions;
using InventorSoftTestApp.Domain.Models.DbEntities;
using InventorSoftTestApp.Domain.Models.Enums;
using InventorSoftTestApp.Domain.Repositories.Abstractions;

namespace InventorSoftTestApp.Domain.Jobs;

public class TransferTaskJob(
    ITaskRepository taskRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ITransferTaskJob
{
    public async Task Run()
    {
        try
        {
            await ExecuteJob();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Execution failed \n {e} \n{e.StackTrace} \n {e.InnerException}");
        }
    }

    private async Task ExecuteJob()
    {
        List<TaskModel> uncompletedTasksOlderThanTwoMinutes = await taskRepository.GetUncompletedTasksOlderThanTwoMinutes();
        if (uncompletedTasksOlderThanTwoMinutes.Count > 0)
        {
            List<User> users = await userRepository.FindAll();

            foreach (var task in uncompletedTasksOlderThanTwoMinutes)
            {
                if (task.TransferCounter == 3)
                {
                    task.State = TaskState.Completed;
                    task.UserId = null;

                    continue;
                }

                List<User> usersDoNotConcernToCurrentTask = users.Where(user => user.Id != task.UserId).ToList();

                if (usersDoNotConcernToCurrentTask.Count > 0)
                {
                    var random = new Random();
                    int index = random.Next(usersDoNotConcernToCurrentTask.Count);
                    task.UserId = usersDoNotConcernToCurrentTask[index].Id;
                    task.State = TaskState.InProgress;
                    task.TransferCounter++;
                }
                else
                {
                    task.State = TaskState.Waiting;
                    task.UserId = null;
                }
            }

            taskRepository.UpdateRange(uncompletedTasksOlderThanTwoMinutes);
            await unitOfWork.Commit();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tasks were transferred.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There are no uncompleted tasks older than two minutes.");
        }
    }
}