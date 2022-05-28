using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEW_Notify
{
    public class Area
    {
        public string grade { get; set; }
        public bool immediate { get; set; }
        public string name { get; set; }
    }

    public class P2PTsunami
    {
        public List<Area> areas { get; set; }
        public bool cancelled { get; set; }
        public int code { get; set; }
        public string id { get; set; }
        public string time { get; set; }

    }

}
