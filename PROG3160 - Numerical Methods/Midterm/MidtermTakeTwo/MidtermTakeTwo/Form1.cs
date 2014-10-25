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
        private List<double> xCoefficients = new List<double>();
        private List<TextBox> xCoeffecientTextBoxes = new List<TextBox>();
        private List<double> yCoefficients = new List<double>();
        private List<TextBox> yCoeffecientTextBoxes = new List<TextBox>();

        private double pointsTime = 0;

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
            xCoeffecientTextBoxes.Add(tbXSquared);
            xCoeffecientTextBoxes.Add(tbX);
            xCoeffecientTextBoxes.Add(tbXConstant);

            yCoeffecientTextBoxes.Add(tbYSquared);
            yCoeffecientTextBoxes.Add(tbY);
            yCoeffecientTextBoxes.Add(tbYConstant);
        }

        private void CalculateFunction()
        {
            ResetCoefficients();

            GetPolynomialCoefficients();


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

            pointsTime = 0;

            timerAnimation.Enabled = true;
        }

        private void StopGraphing()
        {
            timerAnimation.Enabled = false;


        }

        private void GraphPoint()
        {

            double x = PlotPoint(xCoefficients, pointsTime);
            double y = PlotPoint(yCoefficients, pointsTime);

            DataPoint dataPoint = new DataPoint(x, y);
            dataPoint.MarkerImage = "ball.png";

            if (chart1.Series.First().Points.Count > 0)
            {
                chart1.Series.First().Points.Last().MarkerImage = String.Empty;
            }

            chart1.Series.First().Points.Add(dataPoint);

            


            if ( pointsTime >= plotSettings.MaxX)
            {
                StopGraphing();
            }
            else
            {
                pointsTime += 0.1;
            }
        }

        private void GetPolynomialCoefficients()
        {
            foreach (TextBox tb in xCoeffecientTextBoxes)
            {
                double value = GetDoubleFromTextBox(tb);

                if (value == 0 && xCoefficients.Count == 0)
                {
                    continue;
                }

                xCoefficients.Add(value);
            }

            foreach (TextBox tb in yCoeffecientTextBoxes)
            {
                double value = GetDoubleFromTextBox(tb);

                if (value == 0 && yCoefficients.Count == 0)
                {
                    continue;
                }

                yCoefficients.Add(value);
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

        private double PlotPoint(List<double> coefficients, double x)
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
            xCoefficients.Clear();
            yCoefficients.Clear();
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
            GraphPoint();
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

    }
}
