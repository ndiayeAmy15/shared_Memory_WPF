using System.Collections.Generic;
using System.Windows.Controls;
using WPFModernVerticalMenu.Model;
using WPFModernVerticalMenu.Services;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Net.Http;

namespace WPFModernVerticalMenu.Pages
{
    public partial class ProductsPage : Page
    {
        private readonly ApiService apiService = new ApiService();
        private Product selectedProduct;
        private List<Category> categories;

        public ProductsPage()
        {
            InitializeComponent();
            LoadCategoriesAndProducts();
        }

        private async void LoadCategoriesAndProducts()
        {
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            //categories = apiService.GetCategories();
            if (ProductCategoryComboBox != null)
            {
                ProductCategoryComboBox.ItemsSource = categories;
            }
        }

        private List<Product> LoadProducts()
        {
            HttpClient client = new HttpClient();
            var services = new List<Product>();
            string url = "http://localhost/backendAppExamen/list_products.php";
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                services = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(responseData);

            }
            return services;
        }

        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = ProductNameTextBox.Text;
            string productPriceText = ProductPriceTextBox.Text;
            Console.WriteLine($"Adding product: Name={productName}, Price={productPriceText}, CategoryId={ProductCategoryComboBox.SelectedValue}");

            if (ProductCategoryComboBox.SelectedValue is int categoryId && !string.IsNullOrWhiteSpace(productName) && decimal.TryParse(productPriceText, out decimal productPrice))
            {
                var product = new Product { Name = productName, Price = productPrice, CategoryId = categoryId };
                await apiService.AddProduct(product);
                LoadProducts();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Veuillez remplir correctement tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }






        private async void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct != null)
            {
                string productName = ProductNameTextBox.Text;
                string productPriceText = ProductPriceTextBox.Text;
                Console.WriteLine($"Updating product: Id={selectedProduct.Id}, Name={productName}, Price={productPriceText}, CategoryId={ProductCategoryComboBox.SelectedValue}");

                if (ProductCategoryComboBox.SelectedValue is int categoryId && !string.IsNullOrWhiteSpace(productName) && decimal.TryParse(productPriceText, out decimal productPrice))
                {
                    selectedProduct.Name = productName;
                    selectedProduct.Price = productPrice;
                    selectedProduct.CategoryId = categoryId;
                    await apiService.UpdateProduct(selectedProduct);
                    LoadProducts();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Veuillez remplir correctement tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct != null)
            {
                await apiService.DeleteProduct(selectedProduct.Id);
                LoadProducts();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridProduits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridProduits.SelectedItem is Product product)
            {
                selectedProduct = product;
                ProductNameTextBox.Text = product.Name;
                ProductPriceTextBox.Text = product.Price.ToString();
                ProductCategoryComboBox.SelectedValue = product.CategoryId;
            }
        }

        private void ClearFields()
        {
            selectedProduct = null;
            ProductNameTextBox.Text = string.Empty;
            ProductPriceTextBox.Text = string.Empty;
            ProductCategoryComboBox.SelectedIndex = -1;
        }
    }
}
