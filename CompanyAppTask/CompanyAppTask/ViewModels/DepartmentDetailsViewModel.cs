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
    public class DepartmentDetailsViewModel : BaseViewModel
    {

        

        private Department selectedDepartment;

        public Department SelectedDepartment
        {
            get { return selectedDepartment; }
            set { SetProperty(ref selectedDepartment, value); }
        }

        private string controller = "api/Departments";

        public ICommand AddCommand { private set; get; }
        public ICommand UpdateCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }

        private async Task Add()
        {
            selectedDepartment.dpId = 0;
            bool result = await CompanyAppServices.AddItem<Department>(controller, selectedDepartment);

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
            bool result = await CompanyAppServices.UpdateItem<Department>(controller, SelectedDepartment, SelectedDepartment.dpId);

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
                bool result = await CompanyAppServices.DeleteItem(controller, selectedDepartment.dpId);

                if (result)
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "The item was deleted successfully!", "OK");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                    await App.Current.MainPage.DisplayAlert("Failure!", "The item was NOT deleted!", "OK");
            }
        }

        public DepartmentDetailsViewModel(Department model)
        {
            SelectedDepartment = model;

            AddCommand = new Command(async () => await Add());
            UpdateCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
        }
    }
}
