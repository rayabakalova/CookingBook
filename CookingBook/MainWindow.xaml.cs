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
using CookingBook.Objects;


namespace CookingBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindowVeiwModel db = new MainWindowVeiwModel();

        public MainWindow()
        {
            InitializeComponent();

            //db.FillRecipe();

            LoadMenu();


            //Test.Content = db.Recipe[0].recipe_name;


        }

        private void LoadMenu()
        {
            db.FillCategories();
            Menu.ItemsSource = db.Category;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null)
            {
                Test.Content = "Raboti";
            }
        }
    }
}
