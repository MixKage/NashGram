using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGramAPI.Model;

public class Person
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public long Country { get; set; }
    public long Age { get; set; }
    public string Number { get; set; }
}
