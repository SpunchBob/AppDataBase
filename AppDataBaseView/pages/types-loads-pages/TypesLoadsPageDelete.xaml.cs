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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppDataBaseView.Models;

namespace AppDataBaseView.pages.types_loads_pages
{
    /// <summary>
    /// Логика взаимодействия для TypesLoadsPageDelete.xaml
    /// </summary>
    public partial class TypesLoadsPageDelete : Page
    {
        public static FormWindow formWindow { get; set; }
        public TypesLoadsPageDelete()
        {
            InitializeComponent();
            GetData();
        }
        public class ComboBoxItem_LoadType : ComboBoxItem 
        {
            public TypesLoad TypesLoadLink { get; set; }
        }
        public void GetData() 
        {
            DataBaseContext Context = new DataBaseContext();
            foreach (TypesLoad load in Context.TypesLoads.ToList<TypesLoad>()) 
            {
                loads_cb.Items.Add(
                    new ComboBoxItem_LoadType() 
                    { 
                        Content = $"{load.LoadTypeCode} - {load.Name}",
                        TypesLoadLink = load
                    }
                    );
            }
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить запись?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes) 
            {
                using (DataBaseContext Context = new DataBaseContext())
                {
                    ComboBoxItem_LoadType item = loads_cb.SelectedItem as ComboBoxItem_LoadType;

                    if (item?.TypesLoadLink != null)
                    {
                        // Прикрепляем сущность к контексту, если она была отсоединена
                        var typesLoadToDelete = Context.TypesLoads
                            .FirstOrDefault(tl => tl.LoadTypeCode == item.TypesLoadLink.LoadTypeCode);

                        if (typesLoadToDelete != null)
                        {
                            Context.TypesLoads.Remove(typesLoadToDelete);
                            Context.SaveChanges();
                            MessageBox.Show("Удаление прошло успешно");
                            formWindow.Close();
                            Scripts.EnableAllButtons();
                        }
                        else
                        {
                            MessageBox.Show("Запись не найдена в базе данных");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не выбрана запись для удаления");
                    }
                }
            }
        }
    }
}
