using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record OrderCreateDTO(
    int UserId,
    List<OrderItemDTO> OrderItems,
    decimal TotalAmount
    );

}
