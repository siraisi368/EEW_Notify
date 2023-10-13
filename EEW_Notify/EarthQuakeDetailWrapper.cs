using EEW_Notify.Jishin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Resources;

namespace EEW_Notify
{
    internal class EarthQuakeDetailWrapper
    {
        /// <summary>
        /// 市町村別データ
        /// </summary>
        public class KansokuPoints_listdata 
        {
            public int Scale { get; set; }
            public string Pref { get; set; }
            public string CityName { get; set; }
        }

        public string Translate_ScaleToShind(int scale)
        {
            switch (scale)
            {
                case 10:
                    return "1";
                case 20:
                    return "2";
                case 30:
                    return "3";
                case 40:
                    return "4";
                case 45:
                    return "5弱";
                case 50:
                    return "5強";
                case 55:
                    return "6弱";
                case 60:
                    return "6強";
                case 65:
                    return "7";
                default:
                    return "不明";
            }
        }

        public string Eqdata_Wrapper(List<P2PEqAPI> data)
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<KansokuPoints_listdata> respDataT = new List<KansokuPoints_listdata>();
            string respData = null;
            List<Point> points = data[0].points;
            List<string> Prefs = new List<string>();
            foreach (Point value in points) //存在する都道府県を取り出す
            {
                if (!Prefs.Contains(value.pref))
                {
                    Prefs.Add(value.pref);
                }
                else
                {
                    continue;
                }
            }

            Dictionary<string,List<KansokuPoints_listdata>> Pref_Points = new Dictionary<string, List<KansokuPoints_listdata>>();
            
            foreach(string value in Prefs)
            {
                Pref_Points.Add(value,new List<KansokuPoints_listdata>());
            }

            foreach (Point value in points) //都道府県ごとに存在する震度を取り出す(準備)
            {
                Pref_Points[value.pref].Add(new KansokuPoints_listdata() { 
                    CityName = value.addr,
                    Pref = value.pref,
                    Scale= value.scale
                });
            }
            
            Dictionary<string, List<Dictionary<string, List<string>>>> shindodatas = new Dictionary<string, List<Dictionary<string, List<string>>>>();  //Pref, Dict<震度,その地名>
            
            foreach (KeyValuePair<string, List<KansokuPoints_listdata>> value in Pref_Points) //都道府県ごとに存在する震度を抽出
            {
                shindodatas.Add(value.Key,new List<Dictionary<string, List<string>>>());
                Dictionary<string,List<string>> shindo = new Dictionary<string, List<string>>();
                foreach (KansokuPoints_listdata value2 in value.Value)
                {
                    if (!shindo.ContainsKey(Translate_ScaleToShind(value2.Scale)))
                    {
                        shindo.Add(Translate_ScaleToShind(value2.Scale), new List<string>());
                    }
                    else
                    {
                        continue;
                    }
                }
                shindodatas[value.Key].Add(shindo);
            }

            foreach(Point value in points)
            {
                shindodatas[value.pref][0][Translate_ScaleToShind(value.scale)].Add(value.addr);
            }

            foreach(KeyValuePair<string, List<Dictionary<string, List<string>>>> value in shindodatas)
            {
                respData += $"----------------{value.Key}----------------\r\n";
                foreach(Dictionary<string, List<string>> value2 in value.Value)
                {
                    foreach(KeyValuePair<string,List<string>> value3 in value2)
                    {
                        respData += $"[震度{value3.Key}]\r\n";
                        foreach(string value4 in value3.Value)
                        {
                            respData += $"{value4}　";
                        }
                        respData += "\r\n";
                    }
                }
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            return respData;
        }
    }
}
