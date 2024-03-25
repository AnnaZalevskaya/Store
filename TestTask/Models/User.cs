using System.Text.Json.Serialization;
using TestTask.DTOs;
using TestTask.Enums;

namespace TestTask.Models
{
    public class User
    {
        public int Id { get; set; } 

        public string Email { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserStatus Status { get; set; }

        [JsonIgnore]
        public virtual List<Order> Orders { get; set; }

        public List<OrderDTO> OrderDTOs
        {
            get
            {
                if (Orders == null)
                    return new List<OrderDTO>(); 

                return Orders.Select(order => new OrderDTO
                {
                    Id = order.Id,
                    ProductName = order.ProductName,
                    Price = order.Price,
                    Quantity = order.Quantity
                }).ToList();
            }
        }
    }
}
