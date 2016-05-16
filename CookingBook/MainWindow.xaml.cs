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

        private static string postfix = ".jpg";
        private static string PathBase = "/CookingBook;component/Images/Dishes/";

        public MainWindow()
        {
            InitializeComponent();

            db.FillRecipe();

            LoadMenu();
            LoadRecommendedImages();


            //Test.Content = db.Recipe[0].recipe_name;


        }

        private void LoadMenu()
        {
            db.FillCategories();
            Menu.ItemsSource = db.Category;
        }

        private void LoadRecommendedImages()
        {
            Random rnd = new Random();
            string path = "";
            string name = "";

            for (int i = 0; i < 4; i++)
            {
                int index = rnd.Next(0, 2);

                if (db.Recipe[index] != null)
                {
                    path = PathBase + db.Recipe[index].recipe_id + postfix;
                    name = db.Recipe[index].recipe_name;
                }

                switch (i)
                {
                    case 1:
                        {
                            SetImage(RecommendedImg1, path);
                            RecommendedTxt1.Content = name;
                        } 
                        break;
                    case 2:
                        {
                            SetImage(RecommendedImg2, path);
                            RecommendedTxt2.Content = name;
                        }
                        break;
                    case 3:
                        {
                            SetImage(RecommendedImg3, path);
                            RecommendedTxt3.Content = name;
                        }
                        break;
                    default:
                        break;
                }       
            }
        }

        private void SetImage(Image img ,string path)
        {
            Uri uri = null;

            try
            {
                uri = new Uri("pack://application:,,," + path);
            }
            finally
            {
                img.Source = new BitmapImage(uri);
            }
           
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
