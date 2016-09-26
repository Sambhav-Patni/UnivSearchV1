using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Gma.UserActivityMonitor;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Web;
using System.Resources;
using Microsoft.Win32;

namespace UnivSearchV1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        bool IsCtrlDown = false;
        static ContextMenuStrip contextMenuStrip;

        public App()
        {
            //InitializeComponent();     
            var AppIcon = UnivSearchV1.Properties.Resources.AppIcon.ToBitmap() as System.Drawing.Image;
            string browser = "iexplore";
            //contextMenuStrip.FindForm().Icon = UnivSearchV1.Properties.Resources.AppIcon;
            HookManager.KeyDown += HookManager_KeyDown;
            HookManager.KeyUp += HookManager_KeyUp;
            try
            {
                RegistryKey browserKeys;
                //on 64bit the browsers are in a different location
                browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
                if (browserKeys == null)
                    browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
                string[] browserNames = browserKeys.GetSubKeyNames();
                foreach (string browserName in browserNames)
                {
                    browser = "iexplore";
                    if (browserName.Contains("Chrome"))
                    {
                        browser = "chrome";
                        break;
                    }
                }
            }
            catch (Exception) { }
            ToolStripItem[] tsiEnvUSClaim = new ToolStripMenuItem[]{new ToolStripMenuItem("US Claim Dev", null, (sender,e) => openLink(sender,e,"cstt-dev",browser)),
                new ToolStripMenuItem("US Claim QA", null, (sender,e) => openLink(sender,e,"cstt-qa",browser)),
                new ToolStripMenuItem("US Claim UAT", null, (sender,e) => openLink(sender,e,"cstt-uat",browser)),
                new ToolStripMenuItem("US Claim Prod", null, (sender,e) => openLink(sender,e,"cstt-prod",browser))
            };

            ToolStripItem[] tsiEnvUSCorrelation = new ToolStripMenuItem[]{new ToolStripMenuItem("US Correlation Dev", null, (sender,e) => openLink(sender,e,"cstt-dev",browser)),
                new ToolStripMenuItem("US Correlation QA", null, (sender,e) => openLink(sender,e,"cstt-qa",browser)),
                new ToolStripMenuItem("US Correlation UAT", null, (sender,e) => openLink(sender,e,"cstt-uat",browser)),
                new ToolStripMenuItem("US Correlation Prod", null, (sender,e) => openLink(sender,e,"cstt-prod",browser))
            };

            ToolStripItem[] tsiEnvUSWorkitem = new ToolStripMenuItem[]{new ToolStripMenuItem("US Workitem Dev", null, (sender,e) => openLink(sender,e,"cstt-dev",browser)),
                new ToolStripMenuItem("US Workitem QA", null, (sender,e) => openLink(sender,e,"cstt-qa",browser)),
                new ToolStripMenuItem("US Workitem UAT", null, (sender,e) => openLink(sender,e,"cstt-uat",browser)),
                new ToolStripMenuItem("US Workitem Prod", null, (sender,e) => openLink(sender,e,"cstt-prod",browser))
            };

            ToolStripItem[] tsiEnvCAClaim = new ToolStripMenuItem[]{new ToolStripMenuItem("CA Claim UAT", null, (sender,e) => openLink(sender,e,"csttca-uat",browser)),
                new ToolStripMenuItem("CA Claim Prod", null, (sender,e) => openLink(sender,e,"csttca-prod",browser))
            };

            ToolStripItem[] tsiEnvCACorrelation = new ToolStripMenuItem[]{new ToolStripMenuItem("CA Correlation UAT", null, (sender,e) => openLink(sender,e,"csttca-uat",browser)),
                new ToolStripMenuItem("CA Correlation Prod", null, (sender,e) => openLink(sender,e,"csttca-prod",browser))
            };

            ToolStripItem[] tsiEnvCAWorkitem = new ToolStripMenuItem[]{new ToolStripMenuItem("CA Workitem UAT", null, (sender,e) => openLink(sender,e,"csttca-uat",browser)),
                new ToolStripMenuItem("CA Workitem Prod", null, (sender,e) => openLink(sender,e,"csttca-prod",browser))
            };

            ToolStripItem[] tsiUS = new ToolStripMenuItem[]{new ToolStripMenuItem("CSTT Claim", null, (sender,e) => openLink(sender,e,"cstt-prod",browser)),
                new ToolStripMenuItem("CSTT Correlation", null, (sender,e) => openLink(sender,e,"cstt-prod",browser)),
                new ToolStripMenuItem("CSTT Workitem", null, (sender,e) => openLink(sender,e,"cstt-prod",browser))
            };
            ToolStripItem[] tsiCA = new ToolStripMenuItem[]{new ToolStripMenuItem("CSTT Claim", null, (sender,e) => openLink(sender,e,"csttca-prod",browser)),
                new ToolStripMenuItem("CSTT Correlation", null, (sender,e) => openLink(sender,e,"csttca-prod",browser)),
                new ToolStripMenuItem("CSTT Workitem", null, (sender,e) => openLink(sender,e,"csttca-prod",browser))
            };
            ((ToolStripMenuItem)tsiUS[0]).DropDownItems.AddRange(tsiEnvUSClaim);
            ((ToolStripMenuItem)tsiUS[1]).DropDownItems.AddRange(tsiEnvUSCorrelation);
            ((ToolStripMenuItem)tsiUS[2]).DropDownItems.AddRange(tsiEnvUSWorkitem);

            ((ToolStripMenuItem)tsiCA[0]).DropDownItems.AddRange(tsiEnvCAClaim);
            ((ToolStripMenuItem)tsiCA[1]).DropDownItems.AddRange(tsiEnvCACorrelation);
            ((ToolStripMenuItem)tsiCA[2]).DropDownItems.AddRange(tsiEnvCAWorkitem);
            contextMenuStrip = new ContextMenuStrip();
            //var resourceManager = new ResourceManager(typeof(UnivSearchV1.Properties.Resources));            
           // System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream("UnivSearchV1.Resources.AppIcon.ico");            
            contextMenuStrip.Items.Add("UnivSearch By SAM", AppIcon);//@"c:\users\sambhav.patni\documents\visual studio 2012\Projects\UnivSearchV1\UnivSearchV1\application_windows_shrink.ico"));            
            //contextMenuStrip.Items.Add("CSTT US", null,(sender,e) => openLink(sender,e,"",browser));
            ToolStripMenuItem CsttUS = new ToolStripMenuItem("CSTT US");
            contextMenuStrip.Items.Add(CsttUS);
            CsttUS.DropDownItems.AddRange(tsiUS);
            ToolStripMenuItem CsttCA = new ToolStripMenuItem("CSTT Canada");
            contextMenuStrip.Items.Add(CsttCA);
            CsttCA.DropDownItems.AddRange(tsiCA);
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add("SalesForce", null, (sender, e) => openLink(sender, e, "SF", browser));
            contextMenuStrip.Items.Add("SharePoint", null, (sender, e) => openLink(sender, e, "SP", browser));
            contextMenuStrip.Items.Add("TFS ID", null, (sender, e) => openLink(sender, e, "TFSID", browser));
            contextMenuStrip.Items.Add("TFS TEXT", null, (sender, e) => openLink(sender, e, "TFS", browser));
            contextMenuStrip.Items.Add("Google", null, (sender, e) => openLink(sender, e, "GS", browser));
            //contextMenuStrip.LostFocus += contextMenuStrip_LostFocus;
            //contextMenuStrip.Items.AddRange(tsi);
            //contextMenuStrip.Items.Add(            
        }

        //void contextMenuStrip_LostFocus(object sender, EventArgs e)
        //{
        //    contextMenuStrip.Hide();
        //    //throw new NotImplementedException();
        //}

        private void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)//System.Windows.Forms.
        {
            if (e.KeyValue == 162)
            {
                IsCtrlDown = false;
            }
            if (e.KeyValue == 27)
            {
                contextMenuStrip.Hide();
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
                    //if (System.Windows.Clipboard.ContainsText())
                    //{
                        //System.Windows.MessageBox.Show(System.Windows.Clipboard.GetText());

                        //Process.Start("chrome.exe", "http://cstt-prod/Home/Results?claimNumber=" + System.Windows.Clipboard.GetText().Trim() + "%");
                                            
                        contextMenuStrip.Show((int)GetCursorPosition().X, (int)GetCursorPosition().Y);//e.Location));
                        contextMenuStrip.Refresh();
                        e.Handled = true;                        
                    //}                    
                }
            }
            //textBoxLog.AppendText(string.Format("KeyDown - {0}\n", e.KeyCode));
            //textBoxLog.ScrollToCaret();
        }
        private void openLink(object sender, EventArgs e, string type, string browser)
        {
            contextMenuStrip.Hide();
            if (System.Windows.Clipboard.ContainsText())
            {
                if (type.Equals("TFS"))
                {
                    Process.Start(browser + ".exe", "http://tfs:8080/tfs/MitchellProjects/APD/_workItems#searchText=" + HttpUtility.UrlEncode(System.Windows.Clipboard.GetText().Trim()) + "&_a=search");
                }
                else if (type.Equals("TFSID"))
                {
                    Process.Start(browser + ".exe", "http://tfs:8080/tfs/MitchellProjects/APD/_workitems#_a=edit&id=" + HttpUtility.UrlEncode(System.Windows.Clipboard.GetText().Trim()));
                }
                else if (type.Equals("SF"))
                {
                    Process.Start(browser + ".exe", "https://na26.salesforce.com/_ui/search/ui/UnifiedSearchResults?str=" + HttpUtility.UrlEncode(System.Windows.Clipboard.GetText().Trim()));
                }
                else if (type.Equals("SP"))
                {
                    Process.Start(browser + ".exe", "http://intranet/Search/Results.aspx?k=" + HttpUtility.UrlEncode(System.Windows.Clipboard.GetText().Trim()));
                }
                else if (type.Equals("GS"))
                {
                    Process.Start(browser + ".exe", "http://www.google.co.in/search?q=" + HttpUtility.UrlEncode(System.Windows.Clipboard.GetText().Trim()));
                }
                else if(type.Contains("cstt")){

                    string url = "http://" + type + "/Home/<>" + HttpUtility.UrlEncode(System.Windows.Clipboard.GetText().Trim()); //Results?claimNumber=
                    if (sender.ToString().Contains("Correlation"))
                    {
                        url = url.Replace("<>", "ErrorLogResult?correlation_id=");
                    }
                    else if (sender.ToString().Contains("Workitem"))
                    {
                        url = url.Replace("<>", "ErrorLogResult?work_item_id=");
                    }
                    else
                    {
                        url = url.Replace("<>", "Results?claimNumber=") + "%";
                    }
                    Process.Start(browser + ".exe", url);
                }
                else

                    Process.Start(browser + ".exe", "http://www.google.co.in/search?q=" + HttpUtility.UrlEncode(System.Windows.Clipboard.GetText().Trim()));
                    //Process.Start(browser + ".exe", "http://cstt-prod/Home/Results?claimNumber=" + HttpUtility.UrlEncode(System.Windows.Clipboard.GetText().Trim() + "%"));
            }
            else
            {
                System.Windows.MessageBox.Show("Please Copy some Text First!!!","Clipboard Error");
            }
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
    }
}
