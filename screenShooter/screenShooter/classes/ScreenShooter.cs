using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screenShooter.classes
{
    class ScreenShooter
    {
        public ScreenShooter()
        {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         NotifyIcon notifyIcon1 = new NotifyIcon();
         ContextMenu contextMenu1 = new ContextMenu();
         MenuItem menuItem1 = new MenuItem();
         contextMenu1.MenuItems.AddRange(new MenuItem[] { menuItem1 });
         menuItem1.Index = 0;
         menuItem1.Text = "E&xit";
         menuItem1.Click += new EventHandler(menuItem1_Click);
         notifyIcon1.Icon = new Icon("Resources/Yellowicon-Flat-Image-capture.ico");
         notifyIcon1.Text = "Form1 (NotifyIcon example)";
         notifyIcon1.ContextMenu = contextMenu1;
         notifyIcon1.Visible = true;
         Application.Run();
         notifyIcon1.Visible = false;
         }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
      //  private static void menuItem1_Click(object Sender, EventArgs e)
      //{
      //   Application.Exit();
      //}
    }
}
