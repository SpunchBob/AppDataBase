using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppDataBaseView.MainWindowHandlers
{
    public static class CheckBoxHandlers
    {
        public static MainWindow? Window { get; set; }
        public static void CheckBox_Checked(object sender, RoutedEventArgs eventArgs) 
        {
            CheckBox box = (CheckBox)sender;
            if (Window != null)
            {
                Scripts.SetCurrent(Window.tablesListBox, Window, box);
                Scripts.DisableAnother(Window.tablesListBox, box);
            }
        }

        public static void CheckBox_Unchecked(object sender, RoutedEventArgs eventArgs) 
        {
            if (Window != null)
            {
                Scripts.EnableAll(Window.tablesListBox);
                Window.Current = "";
            }
        }
    }
}
