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
            db.FillRecipe();

            SetImage(recipe_index);

        }

        private void SetImage(int id)
        {
            string path = "";

            if (db.Recipe[id] != null)
            {
               path = PathBase + db.Recipe[id].recipe_id + postfix;
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
    }
}
