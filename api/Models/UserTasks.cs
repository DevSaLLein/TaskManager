using TaskManager.Model;

namespace TasManager.Models
{
    public class UserTasks
    {
        [JsonIgnore]
        public string UserId { get; set; }
        
        [JsonIgnore]
        public Guid TaskId { get; set; }

        public virtual UserIdentityApp User { get; set; }        
        public virtual TaskItem Task { get; set; }
    }
}