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

//first commit

namespace CookingBook
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        private MainWindowVeiwModel db = new MainWindowVeiwModel();

        private static string postfix = ".jpg";
        private static string PathBase = "/CookingBook;component/Images/Dishes/";

        private static int[] ButtonLinker = new int[6];

        private bool placeHolder = false;

        public MainPage()
        {
            InitializeComponent();

            db.FillRecipe();


            LoadRecipes();
            LoadMenu();
            LoadRecommendedImages();

        }

        public int[] getButtonLinkerId()
        {
            return ButtonLinker;
        }

        private void LoadMenu()
        {
            db.FillCategories();
            Menu.ItemsSource = db.Category;
        }


        private void LoadRecipes()
        {   
            db.FillRecipe();
            SearchList.ItemsSource = db.Recipe;
        }

        private int[] RandomizeIndexes()
        {

            Random rnd = new Random();
            bool duplicate = true;
            int[] indexes = new int[6];

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

                if (db.Recipe[picIndex[i]] != null)
                {
                    path = PathBase + db.Recipe[picIndex[i]].recipe_id + postfix;
                    ButtonLinker[i] = db.Recipe[picIndex[i]].recipe_id;
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

        private void ListViewRecipe_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;

            SearchList.SelectedItem = item.DataContext;
            if (item != null)
            {
                Recipe recipe = (Recipe)SearchList.SelectedItem;
                this.NavigationService.Navigate(new RecipePage(recipe.recipe_id));
            }
        }

        //Placeholder
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = string.Empty;
            searchBox.Foreground = Brushes.Black;
            searchBox.GotFocus -= TextBox_GotFocus;
            placeHolder = true;
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

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (placeHolder)
            {
                db.FillRecipe();
                List<Recipe> recipes = db.Recipe;
                recipes.RemoveAll(x => !x.recipe_name.ToLower().Contains(searchBox.Text.ToLower()) && !x.recipe_descr.ToLower().Contains(searchBox.Text.ToLower()));

                if (recipes != null)
                {
                    SearchList.ItemsSource = recipes;
                }
            }

        }

        private void adminBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminPanel());
        }
    }
}
