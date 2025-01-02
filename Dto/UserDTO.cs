using backend.Models;

namespace backend.Dto
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public List<TaskItemDTO>? Tasks { get; set; }
    }
}
