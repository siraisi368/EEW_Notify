using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace EEW_Notify
{
    public partial class EqInfomation_W : Form
    {
        public EqInfomation_W()
        {
            InitializeComponent();
        }

        private readonly HttpClient client = new HttpClient();

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 10000;
            var url = "https://api.p2pquake.net/v2/history?codes=551&limit=1";
            var json = await client.GetStringAsync(url);
            var p2p = JsonConvert.DeserializeObject<List<P2PEqAPI>>(json);

            var hypocenter = p2p[0].earthquake.hypocenter.name;
            int maxint = p2p[0].earthquake.maxScale;
            double magunitude = p2p[0].earthquake.hypocenter.magnitude;
            int depth_ = p2p[0].earthquake.hypocenter.depth;
            string date = p2p[0].earthquake.time;
            string type = p2p[0].issue.type;
            string issue_time_r = p2p[0].issue.time;

            int time_leng = issue_time_r.Length;
            string issue_time = issue_time_r.Substring(0, time_leng);

            string depth = depth_.ToString();
            string mag = magunitude.ToString();

            label2.Text = date + " 発生";
            label3.Text = "震源:" + hypocenter;

            if(mag == "-1")
            {
                label5.Text = "マグニチュード:不明";
            }
            else
            {
                label5.Text = "マグニチュード:M" + mag;
            }

            if(depth == "-1")
            {
                label6.Text = "深さ:不明";
            }
            else
            {
                label6.Text = "深さ:" + depth + "km";
            }

            label8.Text = issue_time+" 発表";
            switch(type)
            {
                case "ScalePrompt":
                    label7.Text = "情報種別:震度速報";
                    break;
                case "Destination":
                    label7.Text = "情報種別:震源に関する情報";
                    break;
                case "ScaleAndDestination":
                    label7.Text = "情報種別:震源・震度に関する情報";
                    break;
                case "DetailScale":
                    label7.Text = "情報種別:各地の震度に関する情報";
                    break;
                case "Foreign":
                    label7.Text = "情報種別:遠地地震に関する情報";
                    break;
                default:
                    label7.Text = "情報種別:その他";
                    break;
            }

            switch (maxint)
            {
                case -1:
                    label4.Text = "最大震度:不明";
                    break;
                case 10:
                    label4.Text = "最大震度:１";
                    break;
                case 20:
                    label4.Text = "最大震度:２";
                    break;
                case 30:
                    label4.Text = "最大震度:３";
                    break;
                case 40:
                    label4.Text = "最大震度:４";
                    break;
                case 45:
                    label4.Text = "最大震度:５弱";
                    break;
                case 50:
                    label4.Text = "最大震度:５強";
                    break;
                case 55:
                    label4.Text = "最大震度:６弱";
                    break;
                case 60:
                    label4.Text = "最大震度:６強";
                    break;
                case 70:
                    label4.Text = "最大震度:７";
                    break;
            }
        }

        private async void label8_TextChanged(object sender, EventArgs e)
        {
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = @"p2p_q.exe";
            proc.Start();
            textBox1.Text = "取得中...";
            await Task.Delay(3000);
            StreamReader sr = new StreamReader(@"file\eqdata.txt", Encoding.GetEncoding("Shift_JIS"));

            string text = sr.ReadToEnd();

            sr.Close();
            textBox1.Text = text;
        }
    }
}
