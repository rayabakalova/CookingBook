﻿using CookingBook.Objects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        private static bool isLogged = false;

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
                isLogged = true;
                msgTxt.Content = "";
                ShowHideLoginForm(false);
                ShowHidePanel(true);
                passwordBox.Clear();
            }
            else
            {
                msgTxt.Content = "Грешна парола !!!";
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

            CopyFileIntoProject();
        }

        private void CopyFileIntoProject()
        {
            string newPathToFile = @"C:\Users\karad\Documents\visual studio 2015\Projects\CookingBook\CookingBook\Images\Dishes\Test.jpg";
            File.Copy(imgPath, newPathToFile);
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
                recNameInput.Text = recipe.recipe_name;
                recDescrInput.Text = recipe.recipe_descr;

                catInputCombo.SelectedIndex = (int)recipe.category_id - 1;

                db.GetProducts(recipe.recipe_id);

                productList.ClearValue(ListView.ItemsSourceProperty);
                productList.Items.Clear();
                productList.ItemsSource = db.Products;


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
                msgTxt.Visibility = Visibility.Collapsed;
                logOutBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AdminTxtstatic.Visibility = Visibility.Visible;
                passTxtStatic.Visibility = Visibility.Visible;
                passwordBox.Visibility = Visibility.Visible;
                loginBtn.Visibility = Visibility.Visible;
                msgTxt.Visibility = Visibility.Visible;
                logOutBtn.Visibility = Visibility.Collapsed;
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
            recNameInput.Text = "";
            recDescrInput.Text = "";

            catInputCombo.SelectedIndex = - 1;

            productList.ClearValue(ListView.ItemsSourceProperty);

            imgPhoto.Source = null;

            OpenBtn.Content = "Отвори";
        }
    }
}
