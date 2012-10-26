using System;
using System.Collections.Generic;
using System.Windows.Forms;
using uptimeApiChecker;
using Newtonsoft.Json.Linq;
using uptime;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Text = "Connecting to up.time API...";
            btnConnect.Enabled = false;
            txtOutput.Text = "";

            //uptimeApi api = new uptimeApi("admin", "admin", "win-dleith.rd.local", 9997, "v1", true);
            if (txtHostname.Text.Length > 0 && txtUsername.Text.Length > 0 && txtPassword.Text.Length > 0)
            {
                uptimeApi api = new uptimeApi(txtUsername.Text, txtPassword.Text, txtHostname.Text, Int32.Parse(txtPort.Text), txtVersion.Text, chkSSL.Checked);

                string error = "";

                JObject apiInfo = api.getApiInfo(ref error);
                txtOutput.Text += "API Info:" + Environment.NewLine + apiInfo + Environment.NewLine;
                if (error.Length > 0) { txtOutput.Text = error + Environment.NewLine; }

                JArray allElements = api.getElements(ref error, "");
                txtOutput.Text += "Elements      : ";
                if (error.Length > 0) { txtOutput.Text = error + Environment.NewLine; }
                else { txtOutput.Text += "Successfully retrieved info on " + allElements.Count + " Elements." + Environment.NewLine; }

                JArray allMonitors = api.getMonitors(ref error, "");
                txtOutput.Text += "Monitors      : ";
                if (error.Length > 0) { txtOutput.Text = error + Environment.NewLine; }
                else { txtOutput.Text += "Successfully retrieved info on " + allMonitors.Count + " Monitors." + Environment.NewLine; }

                JArray allGroups = api.getGroups(ref error, "");
                txtOutput.Text += "Element Groups: ";
                if (error.Length > 0) { txtOutput.Text = error + Environment.NewLine; }
                else { txtOutput.Text += "Successfully retrieved info on " + allGroups.Count + " Element Groups." + Environment.NewLine; }

            }
            else
            {
                txtOutput.Text = "Please enter the hostname, user name and password to connect to the up.time API.";
            }
            btnConnect.Enabled = true;
            btnConnect.Text = "Connect";
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
