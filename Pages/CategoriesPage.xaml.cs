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
using WPFModernVerticalMenu.Model;
using WPFModernVerticalMenu.Services;

namespace WPFModernVerticalMenu.Pages
{
    /// <summary>
    /// Logique d'interaction pour CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        private readonly ApiService apiService = new ApiService();
        private Category selectedCategory;
        public CategoriesPage()
        {
            InitializeComponent();
            LoadCategories();
        }
        private async Task LoadCategories()
        {
            var categories = await apiService.GetCategories();
            DataGridCategories.ItemsSource = categories;
        }

        private async void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = CategoryNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                var category = new Category { Name = categoryName };
                Console.WriteLine($"Adding category: {categoryName}"); // Log for debugging
                await apiService.AddCategory(category);
                await LoadCategories();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Le nom de la catégorie ne peut pas être vide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void EditCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCategory != null)
            {
                selectedCategory.Name = CategoryNameTextBox.Text;
                Console.WriteLine($"Updating category: {selectedCategory.Name}"); // Log for debugging
                await apiService.UpdateCategory(selectedCategory);
                await LoadCategories();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCategory != null)
            {
                await apiService.DeleteCategory(selectedCategory.Id);
                await LoadCategories();
                ClearFields();
            }
        }
        private void DataGridCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridCategories.SelectedItem is Category category)
            {
                selectedCategory = category;
                CategoryNameTextBox.Text = category.Name;
            }
        }
        private void ClearFields()
        {
            selectedCategory = null;
            CategoryNameTextBox.Text = string.Empty;
        }
    }
}
