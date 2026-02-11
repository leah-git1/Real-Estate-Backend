using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record ProductDetailsDTO(
        int ProductId,
        string Title,
        string Description,
        decimal Price,
        string MainImageUrl,
        List<string> GalleryImages, 
        string City,
        int Beds,
        string CategoryName,
        string OwnerName,
        string OwnerPhone
    );

}
