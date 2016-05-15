using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CookingBook.Objects
{
    class MainWindowVeiwModel : INotifyPropertyChanged
    {

        CookingDataEntities cbdb = new CookingDataEntities();

        public void FillRecipe()
        {
            var q = (from recipe in cbdb.Recipes
                     orderby recipe.recipe_name
                     select recipe).ToList();

            this.Recipe = q;
        }

        public void FillCategories()
        {
            var q = (from category in cbdb.Categories
                     orderby category.category_id
                     select category).ToList();

            this.Category = q;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private List<Recipe> _recipe;
        public List<Recipe> Recipe
        {
            get
            {
                return _recipe;
            }
            set
            {
                _recipe = value;
                NotifyPropertyChanged();
            }
        }

        private List<Category> _category;
        public List<Category> Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                NotifyPropertyChanged();
            }
        }

        private List<Product> _products;
        public List<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                NotifyPropertyChanged();
            }
        }
    }
}
