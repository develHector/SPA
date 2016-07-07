using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalRService
{
    public partial class RunForm : Form
    {
        private SingalRService ServicesToRun = new SingalRService();

        public RunForm()
        {
            this.InitializeComponent();
        }

        private void RunForm_Load(object sender, EventArgs e)
        {
            if ((Properties.Settings.Default.Location.X + Properties.Settings.Default.Location.Y) != 0 && 
                Screen.AllScreens.Any(i => i.Bounds.Contains(Properties.Settings.Default.Location)))
                this.Location = Properties.Settings.Default.Location;            
        }

        private void RunForm_FormClosing(object sender, FormClosingEventArgs e)
        {               
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Save();     
        }
               

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.ServicesToRun.StartServices();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.ServicesToRun.StopServices();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.buttonStop_Click(sender, e);
            this.Close();
        }
    }
}
