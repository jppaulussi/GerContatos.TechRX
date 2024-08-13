using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.DDD;

public class GetDDDAPIExternalDto
{
    public string State { get; set; }
    public string[] Cities { get; set; }

}
