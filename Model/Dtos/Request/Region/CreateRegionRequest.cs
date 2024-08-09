using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request.Region;

public class CreateRegionRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int DDD { get; set; }
}
