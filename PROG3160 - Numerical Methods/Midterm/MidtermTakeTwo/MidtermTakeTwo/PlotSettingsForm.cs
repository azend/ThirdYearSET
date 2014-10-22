using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidtermTakeTwo
{
    public partial class PlotSettingsForm : Form
    {

        public Form1 Parent { get; set; }
        public PlotSettings CurrentSettings { get; set; }

        public PlotSettingsForm()
        {
            InitializeComponent();
        }

        public void SetCurrentSettings(PlotSettings settings) {
            CurrentSettings = settings;

            tbMinX.Text = CurrentSettings.MinX.ToString();
            tbMaxX.Text = CurrentSettings.MaxX.ToString();
            tbMinY.Text = CurrentSettings.MinY.ToString();
            tbMaxY.Text = CurrentSettings.MaxY.ToString();
        }
        private void SaveSettings()
        {
            PlotSettings newSettings = new PlotSettings(GetDoubleFromTextBox(tbMinX), GetDoubleFromTextBox(tbMaxX),
                                                        GetDoubleFromTextBox(tbMinY), GetDoubleFromTextBox(tbMaxY));

            Parent.UpdateSettings(newSettings);
            
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

        private void ResetTextBox(TextBox tb)
        {
            tb.Text = "0";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

    }
}
