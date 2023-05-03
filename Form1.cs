using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ExplorerControll
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        //{
        //    MessageBox.Show("Test");
        //    e.Cancel = true;
        //}
        protected override void OnLoad(System.EventArgs e)
        {
                
            Thread TH = new Thread(KeyboardCheck);
            TH.SetApartmentState(ApartmentState.STA);
            CheckForIllegalCrossThreadCalls = false;
            TH.Start();
            //if(TH.IsAlive)
            //    this.BeginInvoke(new MethodInvoker(Close));
        }
        bool isRunning = true;
        private void KeyboardCheck()
        {
            while (isRunning)
            {
                Thread.Sleep(40);
                if ((Keyboard.GetKeyStates(Key.LeftAlt) & KeyStates.Down) > 0
                    && (Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
                {
                    Process.Start("Notepad.exe");
                    label1.Text = "Pressed";
                }
                else
                {
                    label1.Text = "Unpressed";
                }

                if ((Keyboard.GetKeyStates(Key.LeftAlt) & KeyStates.Down) > 0
                    && (Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
                {
                    Process[] appInstances = Process.GetProcessesByName("Notepad");
                    if (appInstances.Length > 0) 
                    {
                        foreach (Process p in appInstances)
                            p.Kill();
                    }
                   

                }
                else
                {
                    label1.Text = "Unpressed";
                }
                
            }
        }
    }
}
