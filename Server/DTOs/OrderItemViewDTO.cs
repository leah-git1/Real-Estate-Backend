using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record OrderItemViewDTO(
        int ProductID,
        string Title,
        decimal PriceAtPurchase, 
        string MainImageUrl
    );
}
