using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FacebookWrapper;
using Facebook;

namespace C20_Ex01_TomerAbutbul_205657992_RonJourno_208418731
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginResult result = FacebookWrapper.FacebookService.Connect("654371695268712");
            Application.Run(new Form1());
        }
    }
}
