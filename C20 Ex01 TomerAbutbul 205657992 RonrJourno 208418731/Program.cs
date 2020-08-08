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
            LoginResult result = FacebookWrapper.FacebookService.Login(
                "654371695268712",
                "email",
                "user_friends",
                "user_age_range",
                "user_birthday",
                "user_gender",
                "user_hometown",
                "user_likes",
                "user_link",
                "user_photos",
                "user_posts",
                "user_videos");
            Console.WriteLine(result);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
