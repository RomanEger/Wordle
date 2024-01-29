using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Wordlee.Services
{
    class MyFrame
    {
        public static Frame frame;

        public static bool Navigate(Page newPage) => frame?.Navigate(newPage) ?? false;
        
        public static void ClearHistory()
        {
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
            frame.GoBack();
            ClearHistory();
        }
    }
}
