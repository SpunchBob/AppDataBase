using Microsoft.EntityFrameworkCore;
using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDataBaseView
{
    public static class Scripts
    {
        public static void AddListBoxItemsHandlers(ListBox? list, List<MouseButtonEventHandler>? pointers, RoutedEventHandler handler) 
        {
            if (list != null && pointers != null)
            { 
                for (int index = 0; index < list.Items.Count; index++)
                {
                    ListBoxItem item = (ListBoxItem) list.Items[index];
                    item.MouseDoubleClick += pointers[index];
                    item.Selected += handler;
                }
            }
        }

        public static void GetCurrent(ListBox list, MainWindow window) 
        {
            if (list != null)
            {
                foreach (ListBoxItem item in list.Items) 
                {
                    
                }
            }
        }
    }
}
