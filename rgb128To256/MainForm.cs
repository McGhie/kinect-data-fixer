using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinnectDataToHexAndXYZ
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {

        string FilePath = "Data.txt";
        int count = 0;
        public MainForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        delegate void AppendOutputT(string text);
        delegate void AppendOutputI(int text);

        public void AppendOutput(string data)
        {
            data += '\n';
            if (this.OutPutBox.InvokeRequired)
            {
                AppendOutputT d = new AppendOutputT(AppendOutput);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                OutPutBox.AppendText(data);
                
            }



        }

        public void AppendOutput(int data)
        {
            data += '\n';
            if (this.OutPutBox.InvokeRequired)
            {
                AppendOutputT d = new AppendOutputT(AppendOutput);
                this.Invoke(d, new object[] { data.ToString() });
            }
            else
            {
                OutPutBox.AppendText(data.ToString());
                
            }
        }

        private void RunSystem_Click(object sender, EventArgs e)
        {
            Data D = new Data(count,PathBox.Text);
            count++;
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OutPutBox.SelectionStart = OutPutBox.Text.Length;
            OutPutBox.ScrollToCaret();

        }
    }
}
