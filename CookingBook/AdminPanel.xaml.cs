using CookingBook.Objects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {


        private MainWindowVeiwModel db = new MainWindowVeiwModel();

        private static string passStore = "cook";
        private static int storeRecipeID;
        public static int temporaryVar;

        public static List<Product> prods = new List<Product>();

        string imgPath;

        public AdminPanel()
        {
            InitializeComponent();

            LoadCategory();
            LoadRecipes();
            ShowHidePanel();
            ShowHideLoginForm();
        }

        private bool checkPassword()
        {
            if (passStore == passwordBox.Password)
            {
                return true;
            }

            return false;
        }

        private void LoadCategory()
        {
            db.FillCategories();

            for (int i = 0; i < 5; i++)
            {
                catInputCombo.Items.Add(db.Category[i].category_name);
            }
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkPassword())
            {
                ShowHideLoginForm(false);
                ShowHidePanel(true);
                passwordBox.Clear();
            }
            else
            {
                MessageBox.Show("Грешна парола !!!");
            }
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPath = op.FileName;
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }

        }

        private void CopyFileIntoProject()
        {
            db.FillRecipe();
            int newIndex = db.Recipe.Count;
            string newPathToFile = @"C:\Users\karad\Documents\visual studio 2015\Projects\CookingBook\CookingBook\Images\Dishes\" + newIndex + ".jpg";
            
            File.Copy(imgPath, newPathToFile, true);

        }

        private void LoadRecipes()
        {
            db.FillRecipe();
            SearchList.ItemsSource = db.Recipe;
        }

        private void ListViewRecipe_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;

            SearchList.SelectedItem = item.DataContext;
            if (item != null)
            {
                Recipe recipe = (Recipe)SearchList.SelectedItem;
                storeRecipeID = recipe.recipe_id;
                recNameInput.Text = recipe.recipe_name;
                recDescrInput.Text = recipe.recipe_descr;

                catInputCombo.SelectedIndex = (int)recipe.category_id - 1;

                //temporary need to be fixed
                if (recipe.recipe_id == temporaryVar)
                {
                    productList.ClearValue(ListView.ItemsSourceProperty);
                    productList.Items.Clear();
                    productList.ItemsSource = prods;
                }
                else
                {
                    db.GetProducts(recipe.recipe_id);

                    productList.ClearValue(ListView.ItemsSourceProperty);
                    productList.Items.Clear();
                    productList.ItemsSource = db.Products;
                }


                imgPhoto.Source = new BitmapImage(new Uri(@"C:\Users\karad\Documents\visual studio 2015\Projects\CookingBook\CookingBook\Images\Dishes\" + recipe.recipe_id + ".jpg"));

                OpenBtn.Content = "Промени";
            }
        }

        private void ListViewProduct_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;

            SearchList.SelectedItem = item.DataContext;
            if (item != null)
            {

            }
        }

        private void ShowHidePanel(bool show = false)
        {

            if (!show)
            {
                recNameTxt.Visibility = Visibility.Collapsed;
                recNameInput.Visibility = Visibility.Collapsed;
                recDescrTxt.Visibility = Visibility.Collapsed;
                recDescrInput.Visibility = Visibility.Collapsed;
                recCatTxt.Visibility = Visibility.Collapsed;
                catInputCombo.Visibility = Visibility.Collapsed;
                productTxt.Visibility = Visibility.Collapsed;
                addProductTxt.Visibility = Visibility.Collapsed;
                addBtn.Visibility = Visibility.Collapsed;
                productList.Visibility = Visibility.Collapsed;
                loadImageTxt.Visibility = Visibility.Collapsed;
                OpenBtn.Visibility = Visibility.Collapsed;
                imgPhoto.Visibility = Visibility.Collapsed;
                SearchList.Visibility = Visibility.Collapsed;
                addRecipe.Visibility = Visibility.Collapsed;
                deleteRecipe.Visibility = Visibility.Collapsed;
                changeRecipe.Visibility = Visibility.Collapsed;
                clearForms.Visibility = Visibility.Collapsed;
            }
            else
            {
                recNameTxt.Visibility = Visibility.Visible;
                recNameInput.Visibility = Visibility.Visible;
                recDescrTxt.Visibility = Visibility.Visible;
                recDescrInput.Visibility = Visibility.Visible;
                recCatTxt.Visibility = Visibility.Visible;
                catInputCombo.Visibility = Visibility.Visible;
                productTxt.Visibility = Visibility.Visible;
                addProductTxt.Visibility = Visibility.Visible;
                addBtn.Visibility = Visibility.Visible;
                productList.Visibility = Visibility.Visible;
                loadImageTxt.Visibility = Visibility.Visible;
                OpenBtn.Visibility = Visibility.Visible;
                imgPhoto.Visibility = Visibility.Visible;
                SearchList.Visibility = Visibility.Visible;
                addRecipe.Visibility = Visibility.Visible;
                deleteRecipe.Visibility = Visibility.Visible;
                changeRecipe.Visibility = Visibility.Visible;
                clearForms.Visibility = Visibility.Visible;
            }


        }


        private void ShowHideLoginForm(bool show = true)
        {

            if (!show)
            {
                AdminTxtstatic.Visibility = Visibility.Collapsed;
                passTxtStatic.Visibility = Visibility.Collapsed;
                passwordBox.Visibility = Visibility.Collapsed;
                loginBtn.Visibility = Visibility.Collapsed;
                logOutBtn.Visibility = Visibility.Visible;
                backButton.Visibility = Visibility.Collapsed;
                backBtnImg.Visibility = Visibility.Collapsed;
            }
            else
            {
                AdminTxtstatic.Visibility = Visibility.Visible;
                passTxtStatic.Visibility = Visibility.Visible;
                passwordBox.Visibility = Visibility.Visible;
                loginBtn.Visibility = Visibility.Visible;
                logOutBtn.Visibility = Visibility.Collapsed;
                backButton.Visibility = Visibility.Visible;
                backBtnImg.Visibility = Visibility.Visible;
            }

        }

        private void logOutBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowHidePanel();
            ShowHideLoginForm();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.products_name = addProductTxt.Text;
            productList.ClearValue(ListView.ItemsSourceProperty);
            productList.Items.Add(product);
        }

        private void clearForms_Click(object sender, RoutedEventArgs e)
        {
            ClearForms();
        }

        private void ClearForms()
        {
            recNameInput.Text = "";
            recDescrInput.Text = "";

            catInputCombo.SelectedIndex = -1;

            productList.ClearValue(ListView.ItemsSourceProperty);

            imgPhoto.Source = null;

            OpenBtn.Content = "Отвори";
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void addRecipe_Click(object sender, RoutedEventArgs e)
        {

            if (validateAction())
            {
                db.FillRecipe();
                int newIndex = db.Recipe.Count + 1;
                db.InsertRecord(newIndex, recNameInput.Text, recDescrInput.Text, catInputCombo.SelectedIndex + 1);

                db.FillProduct();
                int newProdIndex = db.Products.Count + 1;

                temporaryVar = newIndex;


                for (int i = 0; i < productList.Items.Count; i++)
                {
                    Product prod = (Product)productList.Items[i];

                    prods.Add(prod);
                }


                db.InsertProductForRecipe(newIndex, prods, newProdIndex);



                //need to be fixed
                //db.LinkRecipeProduct(newProdIndex, prods.Count, newProdIndex);

                CopyFileIntoProject();
                LoadRecipes();
                ClearForms();
            }
            else
            {
                MessageBox.Show("Празни полета не са позволени !!!");
            }
            
        }

        private void changeRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (validateAction())
            {
                db.ModifyRecord(storeRecipeID, recNameInput.Text, recDescrInput.Text, catInputCombo.SelectedIndex + 1);
                LoadRecipes();
            }
            else
            {
                MessageBox.Show("Празни полета не са позволени !!!");
            }

        }

        private void deleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (validateAction())
            {
                db.DeleteRecord(storeRecipeID);
                LoadRecipes();
                ClearForms();
            }
            else
            {
                MessageBox.Show("Празни полета не са позволени !!!");
            }
        }

        private bool validateAction()
        {
            if (string.IsNullOrWhiteSpace(recNameInput.Text)
                || string.IsNullOrWhiteSpace(recDescrInput.Text)
                || catInputCombo.SelectedIndex == -1)
            {
                return false;
            }

            return true;
        }
    }
}
