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
            MessageBox.Show(String.Format(
                $"Alt + Arrow Up - Start Explorer \n" +
                $"Alt + Arrow Down - Close Explorer \n" +
                $"Alt + Arrow Escape - Kill this program from background \n"
                ),
                "Explorer Control FAQ");


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
                //hotkey for open Explorer
                if ((Keyboard.GetKeyStates(Key.LeftAlt) & KeyStates.Down) > 0
                    && (Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
                {
                    Process[] appInstances = Process.GetProcessesByName("Explorer");
                    if(appInstances.Length < 1)
                        System.Diagnostics.Process.Start(@"C:\Windows\explorer.exe");

                }

                //hotkey for close Explorer
                if ((Keyboard.GetKeyStates(Key.LeftAlt) & KeyStates.Down) > 0
                    && (Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
                {
                    for (int i = 0; i < 10; i++) //don't know, magic trick
                    {
                        Process[] appInstances = Process.GetProcessesByName("explorer");
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
                }
                
                //hotkey for close app
                if ((Keyboard.GetKeyStates(Key.LeftAlt) & KeyStates.Down) > 0
                        && (Keyboard.GetKeyStates(Key.Escape) & KeyStates.Down) > 0)
                {
                    isRunning = false;
                }
            }
        }
    }
}
