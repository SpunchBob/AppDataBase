using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppDataBaseView.MainWindowHandlers
{
    public class ButtonsHandlers
    {
        public static DbContext? Context { get; set; }
        public static Window? Window { get; set; }
        public static DataGrid? Data { get; set; }
        public static void WriteButton_OnClick(object sender, RoutedEventArgs eventArgs) 
        {
            
        }

        public static void ChangeButton_OnClick(object sender, RoutedEventArgs eventArgs) 
        {
        
        }

        public static void DeleteButton_OnClick(object sender, RoutedEventArgs eventArgs) 
        {
        
        }
    }
}
