using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record UserUpdateDTO
     (
        string? FullName,
        string? Email,
        string? Password,
        string? Phone,
        string? Address
    );

}
