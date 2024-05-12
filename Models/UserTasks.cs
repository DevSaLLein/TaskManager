using TaskManager.Model;

namespace TasManager.Models
{
    public class UserTasks
    {
        public string UserId { get; set; }
        public Guid TaskId { get; set; }

        public virtual UserIdentityApp User { get; set; }        
        public virtual TaskItem Task { get; set; }
    }
}