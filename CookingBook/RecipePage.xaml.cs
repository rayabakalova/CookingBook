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


        private string _previewWindowXaml =
                @"<Window
                    xmlns                 ='http://schemas.microsoft.com/netfx/2007/xaml/presentation'
                    xmlns:x               ='http://schemas.microsoft.com/winfx/2006/xaml'
                    Title                 ='Print Preview - @@TITLE'
                    Height                ='200'
                    Width                 ='300'
                    WindowStartupLocation ='CenterOwner'>
                    <DocumentViewer Name='dv1'/>
                    </Window>";


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

        //internal void DoPreview(string title)
        //{
        //    string fileName = System.IO.Path.GetRandomFileName();
        //    FlowDocumentScrollViewer visual = (FlowDocumentScrollViewer)(_parent.FindName("fdsv1"));
        //    try
        //    {
        //        // write the XPS document
        //        using (XpsDocument doc = new XpsDocument(fileName, FileAccess.ReadWrite))
        //        {
        //            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
        //            writer.Write(visual);
        //        }

        //        // Read the XPS document into a dynamically generated
        //        // preview Window 
        //        using (XpsDocument doc = new XpsDocument(fileName, FileAccess.Read))
        //        {
        //            FixedDocumentSequence fds = doc.GetFixedDocumentSequence();

        //            string s = _previewWindowXaml;
        //            s = s.Replace("@@TITLE", title.Replace("'", "&apos;"));

        //            using (var reader = new System.Xml.XmlTextReader(new StringReader(s)))
        //            {
        //                Window preview = System.Windows.Markup.XamlReader.Load(reader) as Window;

        //                DocumentViewer dv1 = LogicalTreeHelper.FindLogicalNode(preview, "dv1") as DocumentViewer;
        //                dv1.Document = fds as IDocumentPaginatorSource;


        //                preview.ShowDialog();
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        if (File.Exists(fileName))
        //        {
        //            try
        //            {
        //                File.Delete(fileName);
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //}

        private void test_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
