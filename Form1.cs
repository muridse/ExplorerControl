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

        protected override void OnLoad(System.EventArgs e)
        {

            Thread TH = new Thread(KeyboardCheck);
            TH.SetApartmentState(ApartmentState.STA);
            CheckForIllegalCrossThreadCalls = false;
            TH.Start();
            if (TH.IsAlive)
                this.BeginInvoke(new MethodInvoker(Close));
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
                    Process.Start("Notepad");
                }


                if ((Keyboard.GetKeyStates(Key.LeftAlt) & KeyStates.Down) > 0
                    && (Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
                {
                    Process[] appInstances = Process.GetProcessesByName("Notepad");
                    foreach (Process p in appInstances)
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                
                if ((Keyboard.GetKeyStates(Key.LeftAlt) & KeyStates.Down) > 0
                        && (Keyboard.GetKeyStates(Key.Escape) & KeyStates.Down) > 0)
                {
                    isRunning = false;
                }
            }
        }
    }
}
