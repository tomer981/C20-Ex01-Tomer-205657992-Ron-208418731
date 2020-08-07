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
            //LoginResult result = FacebookWrapper.FacebookService.Connect("654371695268712");
            LoginResult result = FacebookWrapper.FacebookService.Login("654371695268712", "education_schools", "user_Educations");
            Console.WriteLine(result);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
