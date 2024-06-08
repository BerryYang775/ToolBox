using API.Model.Core;

namespace API.Model.DTO.Todo
{
    public record PostTodo
    {
        public string Caption { get; set; }
        public TodoCategory Category { get; set; }
    }
}
