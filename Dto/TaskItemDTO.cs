namespace backend.Dto
{
    public class TaskItemDTO
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsComplete { get; set; }
        public bool? IsImportant { get; set; }
        public DateOnly? DueDate { get; set; }
        public int UserId { get; set; }
    }
}
