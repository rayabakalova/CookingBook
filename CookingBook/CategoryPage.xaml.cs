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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookingBook
{
    /// <summary>
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {

        private MainWindowVeiwModel db = new MainWindowVeiwModel();

        private static string postfix = ".jpg";
        private static string PathBase = "/CookingBook;component/Images/Dishes/";

        private static int[] ButtonLinker = new int[6];

        private static int storeCatId; 

        public CategoryPage(string title, int categoryId)
        {
            InitializeComponent();

            categoryTitle.Content = title;
            storeCatId = categoryId;

            LoadMenu();
            LoadRecipes();
            LoadCategoryImages();
        }

        private void LoadMenu()
        {
            db.FillCategories();
            Menu.ItemsSource = db.Category;
        }

        public int[] getButtonLinkerId()
        {
            return ButtonLinker;
        }

        private int[] RandomizeIndexes()
        {

            Random rnd = new Random();
            bool duplicate = true;
            int[] indexes = new int[6];

            for (int i = 0; i < 6; i++)
            {
                indexes[i] = rnd.Next(0, db.RecipeByCategory.Count);

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
                            indexes[x] = rnd.Next(0, db.RecipeByCategory.Count);
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

        private void LoadCategoryImages()
        {

            int[] picIndex = RandomizeIndexes();

            string path = "";
            string name = "";

            for (int i = 0; i < 6; i++)
            {

                if (db.RecipeByCategory[picIndex[i]] != null)
                {
                    path = PathBase + db.RecipeByCategory[picIndex[i]].recipe_id + postfix;
                    ButtonLinker[i] = db.RecipeByCategory[picIndex[i]].recipe_id;
                    name = db.RecipeByCategory[picIndex[i]].recipe_name;
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

        private void SetImage(Image img, string path)
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

        private void LoadRecipes()
        {
            db.RecipesByCategory(storeCatId);
            //SearchList.ItemsSource = db.Recipe;
        }

        private void RecBtn1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RecipePage(ButtonLinker[0]));
        }

        private void RecBtn2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RecipePage(ButtonLinker[1]));
        }

        private void RecBtn3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RecipePage(ButtonLinker[2]));
        }

        private void RecBtn4_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RecipePage(ButtonLinker[3]));
        }

        private void RecBtn5_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RecipePage(ButtonLinker[4]));
        }

        private void RecBtn6_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RecipePage(ButtonLinker[5]));
        }


        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            Menu.SelectedItem = item.DataContext;
            if (item != null)
            {
                Category cat = (Category)Menu.SelectedItem;
                this.NavigationService.Navigate(new CategoryPage(cat.category_name, cat.category_id));
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
