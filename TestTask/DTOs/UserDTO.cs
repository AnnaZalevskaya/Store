using System.Text.Json.Serialization;
using TestTask.Enums;

namespace TestTask.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserStatus Status { get; set; }
    }
}
