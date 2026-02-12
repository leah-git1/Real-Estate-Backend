using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record ProductDTO(
        int ProductId,
        string Title,
        string Description,
        decimal Price,
        string MainImageUrl,
        int CategoryID,
        string City,
        int Beds
    );

}
