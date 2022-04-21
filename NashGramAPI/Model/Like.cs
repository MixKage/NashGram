using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGramAPI.Model;

public class Like
{
    public long Id { get; set; }
    public long IdPost { get; set; }
    public long IdAccount { get; set; }
}
