using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopTanuki
{
    static class TanukiControl
    {
        /// <summary>
        /// たぬき召喚プログラム
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TanukiMainBody());
        }
    }
}
