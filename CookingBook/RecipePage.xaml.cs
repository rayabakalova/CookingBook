using CookingBook.Objects;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Printing;
using System.IO;

namespace CookingBook
{
    /// <summary>
    /// Interaction logic for RecipePage.xaml
    /// </summary>
    public partial class RecipePage : Page
    {

        private static string postfix = ".jpg";
        private static string PathBase = "/CookingBook;component/Images/Dishes/";

        private MainWindowVeiwModel db = new MainWindowVeiwModel();

        public RecipePage(int recipe_index)
        {
            InitializeComponent();


            db.FindById(recipe_index);
            SetImage();
            WriteProdcuts(recipe_index);

        }

        private void SetImage()
        {
            string path = "";

            if (db.Recipe[0] != null)
            { 
                path = PathBase + db.Recipe[0].recipe_id + postfix;
                recipeDescr.Text = db.Recipe[0].recipe_descr;
                recipeTitle.Text = db.Recipe[0].recipe_name;
            }

            Uri uri = null;

            try
            {
                uri = new Uri("pack://application:,,," + path);
            }
            finally
            {
                RecipeImage.Source = new BitmapImage(uri);
            }
        }

        private void WriteProdcuts(int recipe_index)
        {
            db.GetProducts(recipe_index);

            ProductList.ItemsSource = db.Products;
  
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }


        private void print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new System.Windows.Controls.PrintDialog();

            if (printDlg.ShowDialog() == true)
            {

                printDlg.PrintVisual(this, "WPF Print");
            }
        }
    }
}
