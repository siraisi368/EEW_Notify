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
using EEW_Notify.Jishin;
using NAudio.Wave;

namespace EEW_Notify
{
    public partial class EqInfomation_W : Form
    {
        public EqInfomation_W()
        {
            InitializeComponent();
        }
        
        private readonly HttpClient client = new HttpClient();

        private async Task P2PQact_p(int a, dynamic p2p)
        {
            var hypocenter = p2p[a].earthquake.hypocenter.name;
            int maxint = p2p[a].earthquake.maxScale;
            float magunitude = (float)p2p[a].earthquake.hypocenter.magnitude;
            int depth_ = p2p[a].earthquake.hypocenter.depth;
            string date = p2p[a].earthquake.time;
            string type = p2p[a].issue.type;

            string depth = depth_.ToString();
            string mag = magunitude.ToString("F1");
            string shindo = null;
            if(a == 0)
            {
                label13.Text = date + " 発生";
                label15.Text = "震源:" + hypocenter;
            }
            if (a == 1)
            {
                label18.Text = date + " 発生";
                label20.Text = "震源:" + hypocenter;
            }
            if (a == 2)
            {
                label23.Text = date + " 発生";
                label25.Text = "震源:" + hypocenter;
            }
            string depth_d = null;
            string mag_d = null;
            

            if (mag == "-1")
            {
                mag_d = "マグニチュード:不明";
            }
            else
            {
                mag_d = "マグニチュード:M" + mag;
            }

            if (depth == "-1")
            {
                depth_d = "深さ:不明";
            }
            else
            {
                if (depth == "0")
                {
                    depth_d = "深さ:ごく浅い";
                }
                else
                {
                    depth_d = "深さ:" + depth + "km";
                }
            }

            switch (maxint)
            {
                case -1:
                    shindo = "最大震度:不明";
                    break;
                case 10:
                    shindo = "最大震度:１";
                    break;
                case 20:
                    shindo = "最大震度:２";
                    break;
                case 30:
                    shindo = "最大震度:３";
                    break;
                case 40:
                    shindo = "最大震度:４";
                    break;
                case 45:
                    shindo = "最大震度:５弱";
                    break;
                case 50:
                    shindo = "最大震度:５強";
                    break;
                case 55:
                    shindo = "最大震度:６弱";
                    break;
                case 60:
                    shindo = "最大震度:６強";
                    break;
                case 70:
                    shindo = "最大震度:７";
                    break;
                default:
                    shindo = "最大震度:不明";
                    break;
            }

            switch (type)
            {
                case "DetailScale":
                    if(a == 0)
                    {
                        label15.Text = "震源:" + hypocenter;
                        label12.Text = mag_d;
                        label11.Text = depth_d;
                        label14.Text = shindo;
                    }
                    if (a == 1)
                    {
                        label20.Text = "震源:" + hypocenter;
                        label17.Text = mag_d;
                        label16.Text = depth_d;
                        label19.Text = shindo;
                    }
                    if (a == 2)
                    {
                        label25.Text = "震源:" + hypocenter;
                        label22.Text = mag_d;
                        label21.Text = depth_d;
                        label24.Text = shindo;
                    }
                    break;
            }
        }
            private async Task P2PQact_now(int a, dynamic p2p)
            {
                var hypocenter = p2p[a].earthquake.hypocenter.name;
                int maxint = p2p[a].earthquake.maxScale;
                float magunitude = (float)p2p[a].earthquake.hypocenter.magnitude;
                int depth_ = p2p[a].earthquake.hypocenter.depth;
                string date = p2p[a].earthquake.time;
                string type = p2p[a].issue.type;
                string issue_time_r = p2p[a].issue.time;

                int time_leng = issue_time_r.Length;
                string issue_time = issue_time_r.Substring(0, time_leng);

                string depth = depth_.ToString();
                string mag = magunitude.ToString("F1");

                string depth_d = null;
                string mag_d = null;

                label2.Text = date + " 発生";
                label3.Text = "震源:" + hypocenter;

                if (mag == "-1.0")
                {
                    mag_d = "マグニチュード:不明";
                }
                else
                {
                    mag_d = "マグニチュード:M" + mag;
                }

            if (depth == "-1")
            {
                depth_d = "深さ:不明";
            }
            else
            {
                if (depth == "0")
                {
                    depth_d = "深さ:ごく浅い";
                }
                else
                {
                    depth_d = "深さ:" + depth + "km";
                }
            }

            label8.Text = issue_time + " 発表";

                switch (type)
                {
                    case "ScalePrompt":
                        label7.Text = "情報種別:震度速報";
                        label3.Text = "震源:調査中";
                        label6.Text = "深さ:調査中";
                        label5.Text = "マグニチュード:調査中";
                        break;
                    case "Destination":
                        label7.Text = "情報種別:震源に関する情報";
                        label3.Text = "震源:" + hypocenter;
                        label6.Text = depth_d;
                        label5.Text = mag_d;
                        break;
                    case "ScaleAndDestination":
                        label7.Text = "情報種別:震源・震度に関する情報";
                        label3.Text = "震源:" + hypocenter;
                        label5.Text = mag_d;
                        label6.Text = depth_d;
                        break;
                    case "DetailScale":
                        label7.Text = "情報種別:各地の震度に関する情報";
                        label3.Text = "震源:" + hypocenter;
                        label5.Text = mag_d;
                        label6.Text = depth_d;
                        break;
                    case "Foreign":
                        label7.Text = "情報種別:遠地地震に関する情報";
                        label3.Text = "震源:" + hypocenter;
                        label5.Text = mag_d;
                        label6.Text = "深さ:不明";
                        break;
                    default:
                        label7.Text = "情報種別:その他";
                        label3.Text = "震源:不明";
                        label5.Text = "マグニチュード:不明";
                        label6.Text = "深さ:不明";
                        break;
                }

                if (type == "ScalePrompt" || type == "DetailScale" || type == "Foreign")
                {
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
            }

        private readonly EarthQuakeDetailWrapper eqwrap = new EarthQuakeDetailWrapper();

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 10000;
            //var url = "https://api.p2pquake.net/v2/history?codes=551&limit=1";
            var url = "https://api.p2pquake.net/v2/jma/quake?limit=1&min_scale=45";
            var json = await client.GetStringAsync(url);
            var p2p = JsonConvert.DeserializeObject<List<P2PEqAPI>>(json);
            await P2PQact_now(0,p2p);
            textBox1.Text = eqwrap.Eqdata_Wrapper(p2p);

            var url2 = "https://api.p2pquake.net/v2/jma/quake?limit=3&offset=1&quake_type=DetailScale";
            var json2 = await client.GetStringAsync(url2);
            var p2p2 = JsonConvert.DeserializeObject<List<P2PEqAPI>>(json2);
            await P2PQact_p(0, p2p2);
            await P2PQact_p(1, p2p2);
            await P2PQact_p(2, p2p2);
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            WaveOut waveOut = new WaveOut();
            AudioFileReader reader = new AudioFileReader(@"file/info.wav");
            waveOut.Init(reader);
            waveOut.Play();
        }

        private void EqInfomation_W_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(linkLabel1.Text == "地震情報履歴を表示する＞")
            {
                linkLabel1.Text = "地震情報履歴を閉じる＜";
                this.Width = 923;
            }
            else if (linkLabel1.Text == "地震情報履歴を閉じる＜")
            {
                linkLabel1.Text = "地震情報履歴を表示する＞";
                this.Width = 470;
            }
        }
    }
}