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

        private int[] RandomizeIndexes()
        {

            Random rnd = new Random();
            bool duplicate = true;
            int[] indexes = new int[6];


            int test = db.Recipe.Count;

            for (int i = 0; i < 6; i++)
            {
                indexes[i] = rnd.Next(0, db.Recipe.Count);
            }

            while (duplicate == true)
            {
                int dupCounter = 0;
                for (int i = 0; i < 6; i++)
                {
                    for (int x = 0; x < 6; x++)
                    {
                        if (indexes[i] == indexes[x] && x != i)
                        {
                            dupCounter++;
                            indexes[x] = rnd.Next(0, db.Recipe.Count);
                        }
                    }
                }
                if (dupCounter == 0)
                {
                    duplicate = false;
                }
            }

            return indexes;
        }

        private void LoadRecommendedImages()
        {

            int[] picIndex = RandomizeIndexes();

            string path = "";
            string name = "";

            for (int i = 0; i < 6; i++)
            {
                //int index = rnd.Next(0, 2);

                if (db.Recipe[picIndex[i]] != null)
                {
                    path = PathBase + db.Recipe[picIndex[i]].recipe_id + postfix;
                    name = db.Recipe[picIndex[i]].recipe_name;
                }

                switch (i)
                {
                    case 0:
                        {
                            SetImage(RecommendedImg1, path);
                            RecommendedTxt1.Text = name;
                        } 
                        break;
                    case 1:
                        {
                            SetImage(RecommendedImg2, path);
                            RecommendedTxt2.Text = name;
                        }
                        break;
                    case 2:
                        {
                            SetImage(RecommendedImg3, path);
                            RecommendedTxt3.Text = name;
                        }
                        break;
                    case 3:
                        {
                            SetImage(RecommendedImg4, path);
                            RecommendedTxt4.Text = name;
                        }
                        break;

                    case 4:
                        {
                            SetImage(RecommendedImg5, path);
                            RecommendedTxt5.Text = name;
                        }
                        break;

                    case 5:
                        {
                            SetImage(RecommendedImg6, path);
                            RecommendedTxt6.Text = name;
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

        private void RecBtn1_Click(object sender, RoutedEventArgs e)
        {
            //Temporary
            Main.Content = new RecipePage();
            Recommended.Content = "ITs Working!!!";
        }
    }
}
