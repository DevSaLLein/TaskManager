using TaskManager.Enum;
using TaskManager.Model;
using TasManager.Models;

namespace TasManager.DTO.Response.User
{
    public class GetAllUsersWithYoursTasksDto
    (
        UserInformationsToTasksDto user,
        List<TaskItem> tasks
    )
    {
        public UserInformationsToTasksDto User  {get; set;} = user;
        public List<TaskItem> taskItems {get; set;} = tasks;   
    }
}