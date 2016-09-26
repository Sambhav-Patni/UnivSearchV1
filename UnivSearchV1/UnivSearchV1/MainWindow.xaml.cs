using System;
//using System.Collections.Generic;
using System.Diagnostics;
//using System.Linq;
using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
using Gma.UserActivityMonitor;
//using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Forms;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace UnivSearchV1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///     
    public partial class MainWindow : Window
    {
        bool IsCtrlDown = false;
        ContextMenuStrip contextMenuStrip;
        //Point CurrentMousePos = new Point();
        public MainWindow()
        {
            InitializeComponent();
            HookManager.KeyDown += HookManager_KeyDown;
            HookManager.KeyUp += HookManager_KeyUp;

            ToolStripItem[] tsi = new ToolStripMenuItem[]{new ToolStripMenuItem("CSTT-sub", null, openLink),
                new ToolStripMenuItem("CSTT-sub-PROD", null, openLink)                
            };
            ToolStripMenuItem Cstt = new ToolStripMenuItem("CSTT-NEW");

            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("foo");
            contextMenuStrip.Items.Add("bar");
            contextMenuStrip.Items.Add("CSTT", null, openLink);
            contextMenuStrip.Items.Add(Cstt);
            Cstt.DropDownItems.AddRange(tsi);
            //contextMenuStrip.Items.AddRange(tsi);
            //contextMenuStrip.Items.Add(
        }
        private void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)//System.Windows.Forms.
        {
            if (e.KeyValue == 162)
            {
                IsCtrlDown = false;
            }
            //textBoxLog.AppendText(string.Format("KeyDown - {0}\n", e.KeyCode));
            //textBoxLog.ScrollToCaret();
        }
        private void HookManager_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)//System.Windows.Forms.
        {
            //System.Security.SecureString ssPwd = new System.Security.SecureString();
            //string password = "Mitchell21";
            //for (int x = 0; x < password.Length; x++)
            //{
            //    ssPwd.AppendChar(password[x]);
            //}            
            if (e.KeyValue == 162)
            {
                IsCtrlDown = true;
            }
            if (e.KeyValue == 160)
            {
                if (IsCtrlDown)
                {
                    if (System.Windows.Clipboard.ContainsText())
                    {
                        //System.Windows.MessageBox.Show(System.Windows.Clipboard.GetText());

                        //Process.Start("chrome.exe", "http://cstt-prod.mitchell.com/Home/Results?claimNumber=" + System.Windows.Clipboard.GetText().Trim() + "%");

                        contextMenuStrip.Show((int)GetCursorPosition().X, (int)GetCursorPosition().Y);//e.Location));
                        e.Handled = true;
                    }
                }
            }
            //textBoxLog.AppendText(string.Format("KeyDown - {0}\n", e.KeyCode));
            //textBoxLog.ScrollToCaret();
        }
        private void openLink(object sender, EventArgs e)
        {
            Process.Start("chrome.exe", "http://cstt-prod.mitchell.com/Home/Results?claimNumber=" + System.Windows.Clipboard.GetText().Trim() + "%");
        }


        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            //bool success = User32.GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }

        //private void Window_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    CurrentMousePos = e.GetPosition(this);
        //}
    }
}
