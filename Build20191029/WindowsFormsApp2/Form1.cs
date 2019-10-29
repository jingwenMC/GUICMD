using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Process p;
        //string outStr;
        public Form1()
        {
            InitializeComponent();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            p = new Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            p.Start();
            p.BeginOutputReadLine();
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            p.StandardInput.WriteLine("exit");
            p.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }
        delegate void updateDelegate(string msg);
        void update(string msg)
        {
            if (this.InvokeRequired)
                Invoke(new updateDelegate(update), new object[] { msg });
            else
            {
                try
                {
                    richTextBox1.Text += msg;
                    richTextBox1.Focus();
                    richTextBox1.Select(richTextBox1.TextLength, 0);
                    richTextBox1.ScrollToCaret();
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.ToString(), "GUICMD Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            update(e.Data + Environment.NewLine);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength != 0)
            {
                try
                {
                    if(textBox1.Text!="exit"&& textBox1.Text != "cmd"&& textBox1.Text != "Exit"&& textBox1.Text != "Cmd" && textBox1.Text != "EXIT" && textBox1.Text != "CMD")
                    { 
                        p.StandardInput.WriteLine(textBox1.Text);
                        //outStr = p.StandardOutput.ToString();
                        //richTextBox1.Text = outStr;
                    }
                    else
                    {
                        MessageBox.Show("Can't run command:exit or cmd!", "GUICMD Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch(Exception err)
                {
                    MessageBox.Show(err.ToString(), "GUICMD Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No Inputs!", "GUICMD Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //outStr = p.StandardOutput.ReadToEnd();
            //richTextBox1.Text = outStr;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
///https://bbs.csdn.net/topics/390115693