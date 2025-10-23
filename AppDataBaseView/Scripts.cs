using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDataBaseView
{
    public static class Scripts
    {
        public static void AddListBoxHandlers(ListBox? list, List<MouseButtonEventHandler>? pointers) 
        {
            if (list != null && pointers != null)
            {
                for (int index = 0; index < list.Items.Count; index++)
                {
                    ListBoxItem item = (ListBoxItem) list.Items[index];
                    item.MouseDoubleClick += pointers[index];
                }
            }
        }
    }
}
