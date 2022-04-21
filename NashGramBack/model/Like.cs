using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGramBack.Model
{
    public class Like
    {
        public long id { get; set; }
        public long idPost { get; set; }
        public long idAccount { get; set; }
    }
}
