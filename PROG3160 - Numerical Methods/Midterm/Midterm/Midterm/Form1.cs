using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Midterm
{
    public partial class Form1 : Form
    {
        private Timer animationTimer;
        public Form1()
        {
            InitializeComponent();
            animationTimer = new Timer(this.Container);



            Dictionary<double, double> pointsX = new Dictionary<double, double>();
            Dictionary<double, double> pointsY = new Dictionary<double, double>();

            for (double i = 0; i < 10; i += 0.5)
            {
                pointsX.Add(i, CalcXValue(i));
                pointsY.Add(i, CalcYValue(i));
            }

            chart.Series[0].Points.DataBind(pointsX, "Key", "Value", "");
            chart.Series[1].Points.DataBind(pointsY, "Key", "Value", "");
        }

        private double CalcXValue(double t)
        {
            return 32 * t;
        }
        private double CalcYValue(double t)
        {
            return (42f * t) - (4.9f * Math.Pow(t, 2));
        }
    }

}
