using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGramBack.Model
{
    public class Post
    {
        public long id { get; set; }
        public long author { get; set; }
        public string uri { get; set; }
        public string descryption { get; set; }
        public string tag { get; set; }
    }
}
