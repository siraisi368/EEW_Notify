using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using EEW_Notify.Jishin;
using EEW_Notify.Tsunami;

namespace EEW_Notify
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private async void API_AcTest_Tick(object sender, EventArgs e)
        {
            await Task.Delay(0);
            try
            {
                string uri = $"http://www.kmoni.bosai.go.jp";
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(uri);
                httpReq.AllowAutoRedirect = false;

                HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse();

                switch(httpRes.StatusCode)
                {
                    default:
                        label3.Text = "取得失敗";
                        break;

                    case HttpStatusCode.OK:
                        label3.Text = "強震モニタAPI:正常";
                        break;

                    case HttpStatusCode.Created:
                        label3.Text = "強震モニタAPI:正常";
                        break;

                    case HttpStatusCode.Accepted:
                        label3.Text = "強震モニタAPI:正常";
                        break;

                    case HttpStatusCode.NonAuthoritativeInformation:
                        label3.Text = "強震モニタAPI:正常";
                        break;

                    case HttpStatusCode.NoContent:
                        label3.Text = "強震モニタAPI:正常";
                        break;

                    case HttpStatusCode.ResetContent:
                        label3.Text = "強震モニタAPI:正常";
                        break;

                    case HttpStatusCode.PartialContent:
                        label3.Text = "強震モニタAPI:正常";
                        break;
                }
            }
            catch
            {
                label3.Text = "強震モニタAPI:取得失敗";
            }
        }

        private async void PAPI_AcTest_Tick(object sender, EventArgs e)
        {
            await Task.Delay(0);
            try
            {                        
                string uri = "https://www.p2pquake.net";
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(uri);
                httpReq.AllowAutoRedirect = false;

                HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse();

                switch (httpRes.StatusCode)
                {
                    default:
                        label4.Text = "取得失敗";
                        break;

                    case HttpStatusCode.OK:
                        label4.Text = "P2PQ API:正常";
                        break;

                    case HttpStatusCode.Created:
                        label4.Text = "P2PQ API:正常";
                        break;

                    case HttpStatusCode.Accepted:
                        label4.Text = "P2PQ API:正常";
                        break;

                    case HttpStatusCode.NonAuthoritativeInformation:
                        label4.Text = "P2PQ API:正常";
                        break;

                    case HttpStatusCode.NoContent:
                        label4.Text = "P2PQ API:正常";
                        break;

                    case HttpStatusCode.ResetContent:
                        label4.Text = "P2PQ API:正常";
                        break;

                    case HttpStatusCode.PartialContent:
                        label4.Text = "P2PQ API:正常";
                        break;
                }
            }
            catch
            {
                label4.Text = "P2PQ API:取得失敗";
            }
        }

        private async void Status_Tick(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            if(label3.Text == "強震モニタAPI:正常")
            {
                if(label4.Text == "P2PQ API:正常")
                {
                    label5.Text = "ALL GREEN";
                    KAPI_AcTest.Interval = 600000;
                    PAPI_AcTest.Interval = 600000;
                }
                else
                {
                    label5.Text = "STATE ERROR";
                }
            }
            else
            {
                label5.Text = "STATE ERROR";
            }
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            label2.Text = dt.ToString("現在時刻:yyyy/MM/dd HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "起動しました。";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EEW_Window f = new EEW_Window();
            f.Show();
        }

        private readonly HttpClient client = new HttpClient();

        private async void PAPI_EqAct_Tick(object sender, EventArgs e)
        {
            var url = "https://api.p2pquake.net/v2/history?codes=551&limit=1";
            //var url = "https://api.p2pquake.net/v2/jma/quake?limit=1&until_date=20190618";
            var json = await client.GetStringAsync(url);
            var p2p = JsonConvert.DeserializeObject<List<P2PEqAPI>>(json);

            var hypocenter = p2p[0].earthquake.hypocenter.name;
            int maxint = p2p[0].earthquake.maxScale;
            double magunitude = p2p[0].earthquake.hypocenter.magnitude;
            int depth_ = p2p[0].earthquake.hypocenter.depth;
            string depth = depth_.ToString();
            string mag = magunitude.ToString();

            label6.Text = "震源:"+hypocenter;

            if (mag == "-1")
            {
                label8.Text = "マグニチュード:不明";
            }
            else
            {
                label8.Text = "マグニチュード:M" + mag;
            }

            if (depth == "-1")
            {
                label9.Text = "深さ:不明";
            }
            else
            {   
                if(depth == "0")
                {
                    label9.Text = "深さ:ごく浅い";
                }
                else
                {
                    label9.Text = "深さ:" + depth + "km";
                }
            }

            switch (maxint)
            {
                case -1:
                    label7.Text = "最大震度:不明";
                    break;
                case 10:
                    label7.Text = "最大震度:１";
                    break;
                case 20:
                    label7.Text = "最大震度:２";
                    break;
                case 30:
                    label7.Text = "最大震度:３";
                    break;
                case 40:
                    label7.Text = "最大震度:４";
                    break;
                case 45:
                    label7.Text = "最大震度:５弱";
                    break;
                case 50:
                    label7.Text = "最大震度:５強";
                    break;
                case 55:
                    label7.Text = "最大震度:６弱";
                    break;
                case 60:
                    label7.Text = "最大震度:６強";
                    break;
                case 70:
                    label7.Text = "最大震度:７";
                    break;
            }
        }

        private async void P2PTsunami_Tick(object sender, EventArgs e)
        {
            P2PTsunami.Interval = 300000;
            var url = "https://api.p2pquake.net/v2/jma/tsunami";
            //var url = "https://api.p2pquake.net/v2/jma/tsunami?until_date=20220316&offset=3";
            //↑テスト用
            var json = await client.GetStringAsync(url);
            var p2pt = JsonConvert.DeserializeObject<List<P2PTsunami>>(json);

            bool cancel = p2pt[0].cancelled;
            if (cancel == true)
            {
                label10.Text = "津波情報なし";
                textBox1.Text = "発表なし";
                return;
            }

            string watch = null;
            string warning = null;
            string mwarning = null;

            string type = p2pt[0].areas[0].grade;
            string areaname = null;
            var dict = new Dictionary<string, string>();
            dict.Add("Watch", "津波注意報");
            dict.Add("Warning", "津波警報");
            dict.Add("MajorWarning", "大津波警報");

            foreach (var i in p2pt[0].areas)
            {
                    switch (i.grade)
                    {
                        case "Watch":
                            if(watch == null)
                                watch = $"[津波注意報]\r\n- {i.name}\r\n";
                            else
                                watch = watch + $"- {i.name}\r\n";
                            break;
                        case "Warning":
                            if (warning == null)
                                warning = $"[津波警報]\r\n- {i.name}\r\n";
                            else
                                warning = warning + $"- {i.name}\r\n";
                            break;
                        case "MajorWaning":
                            if (mwarning == null)
                                mwarning = $"[大津波警報]\r\n- {i.name}\r\n";
                            else
                                mwarning = mwarning + $"- {i.name}\r\n";
                            break;
                    }
            }
            areaname = $"発表元:{p2pt[0].issue.source}\r\n発表時刻:{p2pt[0].issue.time}\r\n\r\n" + mwarning + warning + watch;
            if (cancel != true)
            {
                label10.Text = $"！{dict[type]}発表中！";
                textBox1.Text = areaname;
                //textBox1.Text = "//--テスト--//\r\n"+areaname;
            }
            (watch,warning,mwarning) = (null,null,null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EqInfomation_W f = new EqInfomation_W();
            f.Show();
        }
    }
}
