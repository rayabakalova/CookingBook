using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data.Entity;
using System.Data.SqlClient;

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

        public void FillProduct()
        {
            var q = (from product in cbdb.Products
                     orderby product.products_id
                     select product).ToList();

            this.Products = q;
        }

        public void FindById(int currentId)
        {
            var q = (from recipe in cbdb.Recipes
                     where recipe.recipe_id == currentId
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


        public void FindByName(string searchParameter)
        {
            var words = searchParameter.Split(' ');

            var q = (from recipe in cbdb.Recipes
                     from word in words
                     where recipe.recipe_name.Contains(word) || recipe.recipe_descr.Contains(word)
                     select recipe).ToList();

            this.FilteredRecipe = q;
        }


        public void RecipesByCategory(int categoryId)
        {

            var q = (from recipe in cbdb.Recipes
                     where recipe.category_id == categoryId
                     select recipe).ToList();

            this.RecipeByCategory = q;
        }

        public void GetProducts(int id)
        {
            //LAMBDA Expression
            var q = cbdb.Products.Where(r => r.Recipes.Any(t => t.recipe_id == id)).ToList();

            this.Products = q;
        }

        public void InsertRecord(int rec_id, string rec_name, string rec_descr, int cat_id)
        {
            Recipe rec = new Recipe()
            {
                recipe_id = rec_id,
                recipe_name = rec_name,
                recipe_descr = rec_descr,
                category_id = cat_id,
            };

            cbdb.Recipes.Add(rec);
            cbdb.SaveChanges();
        }

        public void InsertProductForRecipe(int rec_id, List<Product> products, int newId)
        {
            Product prod = new Product();

            for (int i = 0; i < products.Count; i++)
            {

                string query = "INSERT INTO [dbo].Products(products_id, products_name) VALUES( {0}, {1})";
                cbdb.Database.ExecuteSqlCommand(query, newId+i, products[i].products_name);

            }

            cbdb.SaveChanges();

        }

        public void LinkRecipeProduct(int rec_id, int count, int prod_id)
        {
            for (int i = 0; i < count; i++)
            {
                string query = "INSERT INTO [dbo].RecipeProduct(recipe_id, products_id) VALUES( {0}, {1})";
                cbdb.Database.ExecuteSqlCommand(query, rec_id, prod_id);
            }

            cbdb.SaveChanges();
        }

    public void ModifyRecord(int rec_id, string recipe_name, string rec_descr, int cat_id )
        {
            var q = (from recipe in cbdb.Recipes
                     where recipe.recipe_id == rec_id
                     select recipe).First();

            q.recipe_name = recipe_name;
            q.recipe_descr = rec_descr;
            q.category_id = cat_id;

           int succsess = cbdb.SaveChanges();

        }

        public void DeleteRecord(int rec_id)
        {
            string linkQuery = "DELETE FROM dbo.RecipeProduct WHERE recipe_id = {0}";

            cbdb.Database.ExecuteSqlCommand(linkQuery, rec_id);


            string query = "DELETE FROM Recipe WHERE recipe_id = {0}";

            cbdb.Database.ExecuteSqlCommand(query, rec_id);

            cbdb.SaveChanges();

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


        private List<Recipe> _filteredRecipe;
        public List<Recipe> FilteredRecipe
        {
            get
            {
                return _filteredRecipe;
            }
            set
            {
                _filteredRecipe = value;
                NotifyPropertyChanged();
            }
        }


        private List<Recipe> _recipeByCategory;
        public List<Recipe> RecipeByCategory
        {
            get
            {
                return _recipeByCategory;
            }
            set
            {
                _recipeByCategory = value;
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
