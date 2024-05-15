using TaskManager.Enum;
using TaskManager.Model;
using TasManager.Models;

namespace TasManager.DTO.Response.User
{
    public class GetAllUsersWithYoursTasksDto
    (
        string idUser,
        UserInformationsToTasksDto user,
        List<TaskItem> tasks

    )
    {
        public string IdUser {get; set;} = idUser;
        public UserInformationsToTasksDto User  {get; set;} = user;
        public List<TaskItem> taskItems {get; set;} = tasks;   
    }
}