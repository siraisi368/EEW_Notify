using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEW_Notify
{ 
        public class P2PEqAPI
        {
            public int code { get; set; }
            public Earthquake earthquake { get; set; }
            public string id { get; set; }
            public Issue issue { get; set; }
            public List<Point> points { get; set; }
            public string time { get; set; }

        }

        public class Earthquake
        {
            public string domesticTsunami { get; set; }
            public string foreignTsunami { get; set; }
            public Hypocenter hypocenter { get; set; }
            public int maxScale { get; set; }
            public string time { get; set; }
        }

        public class Hypocenter
        {
            public int depth { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public double magnitude { get; set; }
            public string name { get; set; }
        }

        public class Issue
        {
            public string correct { get; set; }
            public string source { get; set; }
            public string time { get; set; }
            public string type { get; set; }
        }

        public class Point
        {
            public string addr { get; set; }
            public bool isArea { get; set; }
            public string pref { get; set; }
            public int scale { get; set; }
        }

}