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

namespace MidtermTakeTwo
{
    public partial class Form1 : Form
    {
        private List<double> coefficients = new List<double>();
        private List<TextBox> coeffecientTextBoxes = new List<TextBox>();
        private Dictionary<double, double> points = new Dictionary<double, double>();
        private Dictionary<double, double>.Enumerator pointsAnimationEnumerator;

        private PlotSettings plotSettings = new PlotSettings();
        private PlotSettingsForm plotSettingsForm = null;
        
        public Form1()
        {
            InitializeComponent();

            SetupCoefficientTextBoxList();

            SetupChart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CalculateFunction();
        }

        private void SetupCoefficientTextBoxList() {
            coeffecientTextBoxes.Add(tbXSquared);
            coeffecientTextBoxes.Add(tbX);
            coeffecientTextBoxes.Add(tbConstant);
        }

        private void CalculateFunction()
        {
            ResetCoefficients();

            GetPolynomialCoefficients();

            points = PlotGraph(plotSettings.MinX, plotSettings.MaxX);


            StartGraphing();
        }

        private void SetupChart()
        {
            ChartArea chartArea = chart1.ChartAreas.First();

            Axis xAxis = chartArea.AxisX;
            Axis yAxis = chartArea.AxisY;

            xAxis.Minimum = plotSettings.MinX;
            xAxis.Maximum = plotSettings.MaxX;

            yAxis.Minimum = plotSettings.MinY;
            yAxis.Maximum = plotSettings.MaxY;

            xAxis.IsStartedFromZero = false;
            yAxis.IsStartedFromZero = false;
        }

        private void StartGraphing() {
            chart1.Series.First().Points.Clear();

            pointsAnimationEnumerator = points.GetEnumerator();
            pointsAnimationEnumerator.MoveNext();

            timerAnimation.Enabled = true;
        }

        private void StopGraphing()
        {
            timerAnimation.Enabled = false;


        }

        private void GraphEnumerationPoint()
        {
            KeyValuePair<double, double> current = pointsAnimationEnumerator.Current;

            double x = current.Key;
            double y = current.Value;

            chart1.Series.First().Points.AddXY(x, y);


            if (!pointsAnimationEnumerator.MoveNext())
            {
                StopGraphing();
            }
        }

        private void GetPolynomialCoefficients()
        {
            foreach (TextBox tb in coeffecientTextBoxes)
            {
                double value = GetDoubleFromTextBox(tb);

                if (value == 0 && coefficients.Count == 0)
                {
                    continue;
                }

                coefficients.Add(value);
            }
        }

        private double GetDoubleFromTextBox(TextBox tb)
        {
            double value = 0;

            try
            {
                value = Convert.ToDouble(tb.Text);
            }
            catch (FormatException e)
            {
                ResetTextBox(tb);
            }
            catch (OverflowException e)
            {
                ResetTextBox(tb);
            }

            return value;
        }
        private Dictionary<double, double> PlotGraph(double minX, double maxX, double precision = 0.1)
        {
            Dictionary<double, double> points = new Dictionary<double, double>();

            for (double x = minX; x <= maxX; x += precision)
            {
                points.Add(x, PlotFAtX(x));
            }

            return points;
        }

        private double PlotFAtX(double x)
        {
            double y = 0;

            for (int i = 0; i < coefficients.Count; i++)
            {
                y += coefficients[i] * Math.Pow(x, coefficients.Count - (i + 1));
            }

            return y;
        }

        private void ResetTextBox(TextBox tb)
        {
            tb.Text = "0";
        }

        private void ResetCoefficients()
        {
            coefficients.Clear();
        }

        private void ResetPoints()
        {
            points.Clear();
        }

        private void OpenSettings()
        {
            if (plotSettingsForm != null)
            {
                plotSettingsForm = null;
            }

            plotSettingsForm = new PlotSettingsForm();
            plotSettingsForm.Parent = this;
            plotSettingsForm.SetCurrentSettings(plotSettings);
            plotSettingsForm.Show();
        }

        public void UpdateSettings(PlotSettings settings)
        {
            plotSettings = settings;

            SetupChart();

            if (plotSettingsForm != null)
            {
                plotSettingsForm = null;
            }
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            GraphEnumerationPoint();
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

    }
}
