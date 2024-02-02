using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wordlee.Views.Pages;

namespace Wordlee.Services
{
    class MyFrame
    {
        public static Frame frame;

        public static bool Navigate(Page newPage) => frame?.Navigate(newPage) ?? false;
        
        public static void ClearHistory()
        {
            CurrentUser.UserId = null;
            while (frame.CanGoBack && frame.CanGoForward)
            {
                try
                {
                    frame.RemoveBackEntry();
                }
                catch
                {
                    break;
                }
            }
        }

        public static void Exit()
        {
            Navigate(new AuthorizationPage());
            ClearHistory();
        }
    }
}
