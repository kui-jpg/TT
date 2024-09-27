using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SMS_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //初始化数据源
            Random x = new Random();
            Random y = new Random();
            DateTime dttime = Convert.ToDateTime("2024-9-24 00:00");
            List<DateTime> timelist = new List<DateTime>();

            List<int> listGRT_Phase_Y = new List<int>();
            List<int> listBD_Phase_Y = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                listGRT_Phase_Y.Add(y.Next(60, 74));
                listBD_Phase_Y.Add(y.Next(30, 72));
                timelist.Add(dttime.AddMinutes(x.Next(0, 4320)));
            }



            //设置图标XY轴网格线条样式
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 120;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -120;
            chart1.ChartAreas["ChartArea1"].Position.Auto = false;
            chart1.ChartAreas["ChartArea1"].Position.X = 1;
            chart1.ChartAreas["ChartArea1"].Position.Y = 80;
            chart1.ChartAreas["ChartArea1"].Position.Width = 100;
            chart1.ChartAreas["ChartArea1"].Position.Height = 90;

            chart1.Legends[0].DockedToChartArea = "ChartArea1";
            chart1.Legends[0].Docking = Docking.Bottom;


            //添加Series
            chart1.Series.Add(new Series("BD_Phase"));
            chart1.Series["BD_Phase"].ToolTip = "#VALX,#VALY";
            chart1.Series.Add(new Series("GRT_Phase"));
            chart1.Series["GRT_Phase"].ToolTip = "#VALX,#VALY";


            //绑定数据
            for (int i = 0; i < timelist.Count - 1; i++)
            {
                chart1.Series["BD_Phase"].Points.AddXY(timelist[i].ToString("yy-MM-dd HH:mm"), listBD_Phase_Y[i].ToString());
                chart1.Series["GRT_Phase"].Points.AddXY(timelist[i].ToString("yy-MM-dd HH:mm"), listGRT_Phase_Y[i].ToString());
            }
            //设置Series 样式
            chart1.Series["BD_Phase"].ChartType = SeriesChartType.Line;
            chart1.Series["BD_Phase"].Color = Color.DarkRed;
            chart1.Series["GRT_Phase"].ChartType = SeriesChartType.FastLine;
            chart1.Series["GRT_Phase"].YAxisType = AxisType.Secondary;
            chart1.Series["GRT_Phase"].Color = Color.Aquamarine;
            chart1.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            //chart1.ChartAreas["ChartArea1"].AxisX2.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chart1.ChartAreas["ChartArea1"].AxisY2.Maximum = 84;
            chart1.ChartAreas["ChartArea1"].AxisY2.Minimum = -84;


            //添加Titles
            Title title = new Title();
            title.ForeColor = Color.CadetBlue;
            title.Name = "chart1T";
            title.Text = "GRT_Phase&BD_Phase";
            chart1.Titles.Add(title);
            chart1.Titles["chart1T"].Alignment = System.Drawing.ContentAlignment.TopCenter;
            chart1.Titles["chart1T"].Position.Auto = false;
            chart1.Titles["chart1T"].Position.X = 50;
            chart1.Titles["chart1T"].Position.Y = 4;

            //设置X轴Title属性
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "[" + dttime.ToString("yyyy-MM-dd HH:mm") + "~" + dttime.AddMinutes(4320).ToString("yyyy-MM-dd HH:mm") + "]";
            chart1.ChartAreas["ChartArea1"].AxisX.TitleAlignment = StringAlignment.Near;
            chart1.ChartAreas["ChartArea1"].AxisX.TitleForeColor = Color.Chocolate;

            Title tt = new Title();
            tt.Position.X = 20;

        }


        Point point;
        bool isMoving = false;




        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            point = e.Location;
            isMoving = true;
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMoving)
            {
                Point pNew = new Point(e.Location.X - point.X, e.Location.Y - point.Y);
                Location += new Size(pNew);
            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }
    }
}
