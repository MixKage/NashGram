using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGramAPI.Model;

public class Post
{
    public long Id { get; set; }
    public long Author { get; set; }
    public string Uri { get; set; }
    public string Descryption { get; set; }
    public string Tag { get; set; }
}
