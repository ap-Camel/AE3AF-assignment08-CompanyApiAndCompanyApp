using CompanyAppTask.Models;
using CompanyAppTask.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompanyAppTask.ViewModels
{
    public class EmployeeDetailsViewModel : BaseViewModel
    {

        private Employee selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set { SetProperty(ref selectedEmployee, value); }
        }


        private string controller = "api/Employees";

        public ICommand AddCommand { private set; get; }
        public ICommand UpdateCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }

        private async Task Add()
        {
            selectedEmployee.EmId = 0;
            bool result = await CompanyAppServices.AddItem<Employee>(controller, selectedEmployee);

            if (result)
            {
                await App.Current.MainPage.DisplayAlert("Success!", "The item was added successfully!", "OK");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
                await App.Current.MainPage.DisplayAlert("Failure!", "The item was NOT added!", "OK");
        }

        private async Task Update()
        {
            bool result = await CompanyAppServices.UpdateItem<Employee>(controller, SelectedEmployee, SelectedEmployee.EmId);

            if (result)
            {
                await App.Current.MainPage.DisplayAlert("Success!", "The item was updated successfully!", "OK");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
                await App.Current.MainPage.DisplayAlert("Failure!", "The item was NOT updated!", "OK");
        }

        private async Task Delete()
        {
            bool confirm = await App.Current.MainPage.DisplayAlert("Please confirm", "Do you really want to delete this item?", "Yes", "No");

            if (confirm)
            {
                bool result = await CompanyAppServices.DeleteItem(controller, selectedEmployee.EmId);

                if (result)
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "The item was deleted successfully!", "OK");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                    await App.Current.MainPage.DisplayAlert("Failure!", "The item was NOT deleted!", "OK");
            }
        }


        public EmployeeDetailsViewModel(Employee model)
        {
            SelectedEmployee = model;

            AddCommand = new Command(async () => await Add());
            UpdateCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
        }
    }
}
