using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.EF.Datatable
{
    public class Datatable
    {
        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public List<Dictionary<string, string>> order { get; set; }
        public DTColumn[] columns { get; set; }
    }

    public class DTColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool search { get; set; }
        public bool order { get; set; }
    }
}
