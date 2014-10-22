using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SoapConsumer
{
    public partial class SoapConsumerForm : Form
    {
        private List<varData> parameters;
        private Dictionary<string, TextBox> paramTextBoxes;
        private int color = 0;
        public SoapConsumerForm()
        {
            parameters = new List<varData>();
            paramTextBoxes = new Dictionary<string, TextBox>();

            InitializeComponent();
            InitializeWebServices();
            InitializeWebMethods();
            InitializeMethodParameters();

            
        }

        private void InitializeMethodParameters()
        {
            parameters = ConfigReader.getVariables(cmbBoxWebService.SelectedItem.ToString(), cmbBoxWebMethod.SelectedItem.ToString());

            tableLayoutPanel1.Controls.Clear();
            paramTextBoxes.Clear();

            if (parameters.Count > 0)
            {


                for (int i = 0; i < parameters.Count; i++)
                {
                    varData parameter = parameters[i];
                    Label paramLabel = new Label();
                    paramLabel.Text = parameter.varPName;
                    TextBox paramTextBox = new TextBox();
                    paramTextBox.LostFocus += paramTextBox_LostFocus;
                    

                    tableLayoutPanel1.Controls.Add(paramLabel, 0, i);
                    tableLayoutPanel1.Controls.Add(paramTextBox, 1, i);

                    paramTextBoxes.Add(parameter.varName, paramTextBox);
                }

                tableLayoutPanel1.Show();
            }
            else
            {
                tableLayoutPanel1.Hide();
            }
            
        }

        private void paramTextBox_LostFocus(object sender, EventArgs e)
        {
            TextBox senderTextBox = (TextBox)sender;

            string key = String.Empty;
            string type = String.Empty;

            foreach (KeyValuePair<string, TextBox> parameter in paramTextBoxes)
            {
                if (senderTextBox == parameter.Value) {
                    key = parameter.Key;
                    break;
                }
            }

            foreach (varData parameter in parameters)
            {
                if (parameter.varName.Equals(key))
                {
                    type = parameter.varType;
                    break;
                }
            }

            ValidateParameter(key, senderTextBox.Text, type);
        }


        private bool ValidateParameter(string pName, string pInput, string pType)
        {
            bool valid = true;

            switch (pType)
            {
                case "xs:unsignedLong":
                    try
                    {
                        Convert.ToUInt64(pInput);
                    }
                    catch(Exception e) {
                        valid = false;
                        MessageBox.Show("Invalid unsigned long");
                    }
                    break;
                case "xs:int":
                    try
                    {
                        Convert.ToInt32(pInput);
                    }
                    catch(Exception e) {
                        valid = false;
                        MessageBox.Show("Invalid integer");
                    }
                    break;
                case "xs:decimal":
                    try
                    {
                        Convert.ToDecimal(pInput);
                    }
                    catch(Exception e) {
                        valid = false;
                        MessageBox.Show("Invalid decimal");
                    }
                    break;
                case "xs:string":
                    if (String.IsNullOrWhiteSpace(pInput))
                    {
                        valid = false;
                        MessageBox.Show("Invalid input");
                    }
                    break;
                case "xs:boolean":
                    try {
                        Convert.ToBoolean(pInput);
                    }
                    catch (Exception e)
                    {
                        valid = false;
                        MessageBox.Show("Invalid boolean");
                    }
                    break;
            }

            return valid;
        }

        

        private void InitializeWebServices()
        {
            cmbBoxWebService.DataSource = ConfigReader.getServiceNames();
        }

        private void InitializeWebMethods()
        {
            cmbBoxWebMethod.DataSource = ConfigReader.getMethodNames(cmbBoxWebService.SelectedItem.ToString());
        }

        private void SendRequest()
        {

        }

        private void cmbBoxWebService_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeWebMethods();
        }


        private void cmbBoxWebMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeMethodParameters();
        }

        private void btnSendRequest_Click(object sender, EventArgs e)
        {
            SOAP soap = new SOAP();
            soap.populateServiceData(cmbBoxWebService.SelectedItem.ToString());
            soap.populateMethodData(cmbBoxWebMethod.SelectedItem.ToString());

            SortedList<string, string> paramsList = new SortedList<string,string>();
            foreach(KeyValuePair<string, TextBox> elm in paramTextBoxes) {
                paramsList.Add(elm.Key, elm.Value.Text);
            }

            textBox1.Text = soap.sendRequest(paramsList);
        }

        private void timerColorChanger_Tick(object sender, EventArgs e)
        {
            //if (color % 2 == 0)
            //{
            //    this.BackColor = Color.Orange;
            //}
            //else
            //{
            //    this.BackColor = Color.LightGreen;
            //}
            //color++;

        }

        

    }
}
