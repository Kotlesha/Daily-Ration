using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daily_Food_Ration.Service_Layer.ServicesClasses;
using Daily_Food_Ration.Business_Layer.Models;
using Daily_Food_Ration.Service_Layer;
using System.Windows.Forms;
using Maroquio;

namespace Daily_Food_Ration
{
    public partial class DailyFoodRation : Form
    {
        private static BaseService service;
        private static User user;

        public DailyFoodRation(string connectionString)
        {
            service = new Service(connectionString);
            InitializeComponent();


            this.CategoriesNames.BindingContext = new();
            this.CategoriesNames.DataSource = service.CategoriesService.GetAllCategories();
            this.productCategoryName.BindingContext = new();
            this.productCategoryName.DataSource = service.CategoriesService.GetAllCategories();
            this.CategoriesTable.BindingContext = new();
            this.CategoriesTable.DataSource = service.CategoriesService.GetAllCategories();
            this.CategoryOfProduct.BindingContext = new();
            this.CategoryOfProduct.DataSource = service.CategoriesService.GetAllCategories();
            this.categoriesProductsList.BindingContext = new();
            this.categoriesProductsList.DataSource = service.CategoriesService.GetAllCategories();
            user = service.DailyRationService.CreateUser(this.Weight.Value, this.Height.Value, (int)this.Age.Value, DailyActivity.Low);
            this.dailyActivity.DataSource = Enum.GetValues(typeof(DailyActivity));
            this.mealtime.DataSource = Enum.GetValues(typeof(MealTime));
            this.mealList.DataSource = service.DailyRationService.GetAllProductsByMealType(user, (MealTime)mealtime.SelectedIndex);
        }

        private void CategoriesNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CategoriesNames.SelectedIndex == -1)
            {
                return;
            }
            
            this.ProductsTable.DataSource = service.ProductsService.GetAllProducts(this.CategoriesNames.SelectedIndex);
            ProductsTable_RowEnter(sender, e as DataGridViewCellEventArgs);
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.ProductsTable.SelectedRows)
            {
                service.ProductsService.RemoveProduct(this.CategoriesNames.SelectedIndex, row.Index);
            }

            ProductsTable_RowEnter(sender, e as DataGridViewCellEventArgs);
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.productName.Text))
            {
                MessageBox.Show("Пустая строка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.productName.Clear();
                return;
            }

            Product product = service.ProductsService.CreateProduct(this.productName.Text.Trim(), (int)this.productGramms.Value, this.productProteins.Value, this.productFats.Value, this.productCarbs.Value, this.productCalories.Value);
            service.ProductsService.UpdateProduct(this.CategoriesNames.SelectedIndex, this.productCategoryName.SelectedIndex, this.ProductsTable.SelectedRows[0].Index, product);

            ProductsTable_RowEnter(sender, e as DataGridViewCellEventArgs);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.productName.Text))
            {
                MessageBox.Show("Пустая строка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.productName.Clear();
                return;
            }

            Product product = service.ProductsService.CreateProduct(this.productName.Text, (int)this.productGramms.Value, this.productProteins.Value, this.productFats.Value, this.productCarbs.Value, this.productCalories.Value);
            bool success = service.ProductsService.AddProduct(this.productCategoryName.SelectedIndex, product);

            if (!success)
            {
                MessageBox.Show("Продукт с таким именем уже существует в базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveInformation_Click(object sender, EventArgs e) => SetProductInfo(string.Empty, this.CategoriesNames.SelectedIndex, this.productGramms.Minimum, this.productProteins.Minimum, this.productFats.Minimum, this.productCarbs.Minimum, this.productCalories.Minimum);

        private void SetProductInfo(string productName, int productCategoryNameIndex, decimal productGramms, 
            decimal productProteins, decimal productFats, decimal productCarbs, decimal productCalories)
        {
            this.productName.Text = productName;
            this.productCategoryName.SelectedIndex = productCategoryNameIndex;
            this.productGramms.Value = productGramms;
            this.productProteins.Value = productProteins;
            this.productFats.Value = productFats;
            this.productCarbs.Value = productCarbs;
            this.productCalories.Value = productCalories;
        }

        private void RemoveCategory_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.CategoriesTable.SelectedRows)
            {
                service.CategoriesService.RemoveCategory(row.Index);
            }

            CategoriesNames_SelectedIndexChanged(sender, e);
            this.productsList.BindingContext = new();
            this.productsList.DataSource = service.ProductsService.GetAllProducts(this.categoriesProductsList.SelectedIndex);
        }

        private void AddCategory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.categoryName.Text))
            {
                MessageBox.Show("Пустая строка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.categoryName.Clear();
                return;
            }

            Category category = service.CategoriesService.CreateCategory(this.categoryName.Text, this.categoryDescription.Text, new SortableBindingList<Product>());
            bool success = service.CategoriesService.AddCategory(category);

            if (!success)
            {
                MessageBox.Show("Категория с таким именем уже существует в базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductsTable_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.ProductsTable.SelectedRows.Count > 0)
            {
                int index = this.ProductsTable.SelectedRows[0].Index;

                if (index >= service.ProductsService.GetAllProducts(this.CategoriesNames.SelectedIndex).Count())
                {
                    index--;
                }
                
                Product product = service.ProductsService.GetProductById(this.CategoriesNames.SelectedIndex, index);
                SetProductInfo(product.Name, this.CategoriesNames.SelectedIndex, product.Gramms,
                    product.Protein, product.Fats, product.Carbs, product.Calories);
                return;
            }

            if (this.ProductsTable.Rows.Count == 0)
            {
                RemoveInformation_Click(sender, e);
                return;
            }
        }

        private void DailyFoodRation_Load(object sender, EventArgs e)
        {
            if (ProductsTable.Rows.Count > 0)
            {
                ProductsTable.Rows[0].Selected = true;
            }

            if (CategoriesTable.Rows.Count > 0)
            {
                CategoriesTable.Rows[0].Selected = true;
            }
        }

        private void UpdateCategory_Click(object sender, EventArgs e)
        {
            if (CategoriesTable.SelectedRows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(this.categoryName.Text))
                {
                    MessageBox.Show("Пустая строка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.categoryName.Clear();
                    return;
                }

                Category category = service.CategoriesService.CreateCategory(this.categoryName.Text, this.categoryDescription.Text.Trim(), new SortableBindingList<Product>());
                service.CategoriesService.UpdateCategory(CategoriesTable.SelectedRows[0].Index, category);
            }
        }

        private void CategoriesTable_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (CategoriesTable.SelectedRows.Count > 0 && this.CategoriesTable.SelectedRows[0].Index < service.CategoriesService.GetAllCategories().Count())
            {
                Category category = service.CategoriesService.GetCategoryById(this.CategoriesTable.SelectedRows[0].Index);
                SetCategoryInfo(category.Name, category.Description);
            }
        }

        private void SetCategoryInfo(string name, string description)
        {
            this.categoryName.Text = name;
            this.categoryDescription.Text = description;
        }

        private void ProductsTable_Sorted(object sender, EventArgs e) => ProductsTable_RowEnter(sender, e as DataGridViewCellEventArgs);

        private void categoriesProductsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoriesTable.Rows.Count == 0)
            {
                return;
            }

            this.productsList.BindingContext = new();
            this.productsList.DataSource = service.ProductsService.GetAllProducts(this.categoriesProductsList.SelectedIndex);
        }

        private void productsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product product = this.productsList.SelectedItem as Product;

            if (product is not null)
            {
                SetProductInfoDiary(product.Name, this.categoriesProductsList.SelectedIndex, product.Gramms, product.Protein, product.Fats, product.Carbs, product.Calories);
            }
        }

        private void SetProductInfoDiary(string productName, int productCategoryNameIndex, decimal productGramms,
            decimal productProteins, decimal productFats, decimal productCarbs, decimal productCalories)
        {
            this.NameOfProduct.Text = productName;
            this.CategoryOfProduct.SelectedIndex = productCategoryNameIndex;
            this.GrammsCount.Value = productGramms;
            this.ProteinsCount.Value = productProteins;
            this.FatsCount.Value = productFats;
            this.CarbsCount.Value = productCarbs;
            this.CaloriesCount.Value = productCalories;
        }

        private void ClearCategory_Click(object sender, EventArgs e) => SetCategoryInfo(string.Empty, string.Empty);

        private void productsList_MouseDown(object sender, MouseEventArgs e)
        {
            if (productsList.SelectedItem is not null)
            {
                productsList.DoDragDrop(productsList.SelectedItem, DragDropEffects.Move);
            }

            productsList_SelectedIndexChanged(sender, e);
        }

        private void mealList_DragEnter(object sender, DragEventArgs e) 
        { 
            if (service.DailyRationService.GetAllCalories(user) > user.DailyCaloriesRate)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move; 
        }

        private void mealList_DragDrop(object sender, DragEventArgs e)
        {
            Product product = (Product)e.Data.GetData(typeof(Product));
            service.DailyRationService.AddProductToMealTime(user, product, (MealTime)mealtime.SelectedItem, (int)this.GrammsCount.Value);
            this.totalCalories.Value = service.DailyRationService.GetAllCalories(user);
        }

        private void mealList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.mealList.SelectedIndex == -1)
            {
                return;
            }

            Product product = service.DailyRationService.GetProductInMealTimeById(user, (MealTime)this.mealtime.SelectedItem, this.mealList.SelectedIndex);
            SetProductInfoDiary(product.Name, this.categoriesProductsList.SelectedIndex, product.Gramms, product.Protein, product.Fats, product.Carbs, product.Calories);
        }

        private void Weight_ValueChanged(object sender, EventArgs e) => DailyCaloriesRate();

        private void Height_ValueChanged(object sender, EventArgs e) => DailyCaloriesRate();

        private void Age_ValueChanged(object sender, EventArgs e) => DailyCaloriesRate();

        private void dailyActivity_SelectedIndexChanged(object sender, EventArgs e) => DailyCaloriesRate();

        private void DailyCaloriesRate()
        {
            user.Update(this.Weight.Value, this.Height.Value, (int)this.Age.Value, (DailyActivity)dailyActivity.SelectedIndex);
            this.caloriesRate.Value = user.DailyCaloriesRate;
        }

        private void mealtime_SelectedIndexChanged(object sender, EventArgs e) =>
            this.mealList.DataSource = service.DailyRationService.GetAllProductsByMealType(user, (MealTime)mealtime.SelectedItem);

        private void ClearOneMealTime_Click(object sender, EventArgs e)
        {
            service.DailyRationService.ClearRationMealTime(user, (MealTime)this.mealtime.SelectedItem);
            this.totalCalories.Value = service.DailyRationService.GetAllCalories(user);
        }

        private void GrammsCount_ValueChanged(object sender, EventArgs e)
        {
            Product product = service.ProductsService.CreateCopyProduct(this.categoriesProductsList.SelectedIndex, this.productsList.SelectedIndex, (int)this.GrammsCount.Value);
            SetProductInfoDiary(product.Name, this.categoriesProductsList.SelectedIndex, product.Gramms, product.Protein, product.Fats, product.Carbs, product.Calories);
        }

        private void ClearFullRation_Click(object sender, EventArgs e)
        {
            service.DailyRationService.ClearRation(user);
            this.totalCalories.Value = service.DailyRationService.GetAllCalories(user);
        }

        private void mealList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.mealList.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches)
            {
                service.DailyRationService.RemoveProductFromMealTime(user, index, (MealTime)this.mealtime.SelectedIndex);
            }

            this.totalCalories.Value = service.DailyRationService.GetAllCalories(user);
        }

        private void SearchProduct_TextChanged(object sender, EventArgs e)
        {
            this.productsList.BindingContext = new();

            if (this.SearchProduct.Text.Length == 0)
            {
                this.productsList.DataSource = service.ProductsService.GetAllProducts(this.categoriesProductsList.SelectedIndex);
                return;
            }

            this.productsList.DataSource = service.ProductsService.GetAllProductsByName(this.categoriesProductsList.SelectedIndex, this.SearchProduct.Text).ToList();
        }

        private void ChangeProduct_Click(object sender, EventArgs e)
        {
            service.DailyRationService.UpdateProductInMealTime(user, this.mealList.SelectedIndex, (MealTime)this.mealtime.SelectedIndex, (int)this.GrammsCount.Value);
            this.totalCalories.Value = service.DailyRationService.GetAllCalories(user);
        }

        private void totalCalories_ValueChanged(object sender, EventArgs e) => this.label27.Visible = service.DailyRationService.GetAllCalories(user) > user.DailyCaloriesRate;

        private void DailyFoodRation_FormClosed(object sender, FormClosedEventArgs e) => service.Serialize();
    }
}
