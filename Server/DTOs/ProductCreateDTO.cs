using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record ProductCreateDTO(
        string Title,
        string Description,
        decimal Price,
        string MainImageUrl,
        List<string> AdditionalImages, 
        int CategoryID,
        string City,
        int Beds
    );

}
