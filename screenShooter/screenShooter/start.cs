using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace screenShooter
{
    class start
    {
        [STAThread]
        static void Main(string[] args)
        {
            new ScreenShootr();
        }
    }
}
