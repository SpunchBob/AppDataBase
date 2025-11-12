using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AppDataBaseView.MainWindowHandlers;

namespace AppDataBaseView
{
    /// <summary>
    /// Логика взаимодействия для FormWindow.xaml
    /// </summary>

    public partial class FormWindow : Window
    {
        private static FormWindow instance;
        private FormWindow()
        {
            InitializeComponent();
            this.Closed += OnClose;
            this.Height = 600;
            this.Width = 600;
        }

        public static FormWindow getInstance()
        {
            if (instance == null)
            {
                instance = new FormWindow();
            }
            return instance;
        }

        private void OnClose(object sender, EventArgs e)
        {
            instance = null;
            Scripts.EnableAllButtons();
        }
    }
}
