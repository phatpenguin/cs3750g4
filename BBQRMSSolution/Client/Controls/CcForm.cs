using System;
using System.Windows.Forms;

namespace Controls
{
    public partial class CcForm : Form
    {
        public CcForm()
        {
            InitializeComponent();
        }

        private void axKbdWedge1_CardDataChanged(object sender, EventArgs e)
        {
            axKbdWedge1.PortOpen = false;
            txtFirst.Text = axKbdWedge1.GetFName();
            txtLast.Text = axKbdWedge1.GetLName();
            txtPAN.Text = axKbdWedge1.FindElement(2, ";", 0, "=");
            txtMonth.Text = axKbdWedge1.FindElement(2, "=", 2, "2");
            txtYear.Text = axKbdWedge1.FindElement(2, "=", 0, "2");
            axKbdWedge1.ClearBuffer();
            axKbdWedge1.PortOpen = true;

            Submit.DialogResult = DialogResult.OK;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axKbdWedge1.PortOpen = true;
            if(axKbdWedge1.PortOpen == false)
            {
                txtPAN.Text = "Unable to Open Keyboard Reader";
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (Submit.DialogResult == DialogResult.OK)
                this.Close();
            else
            {
                Submit.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            txtPAN.Text = "";
            txtFirst.Text = "";
            txtLast.Text = "";
            txtMonth.Text = "";
            txtYear.Text = "";
        }
    }
}