using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGramAPI.Model;

public class Account
{
    public long Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}
