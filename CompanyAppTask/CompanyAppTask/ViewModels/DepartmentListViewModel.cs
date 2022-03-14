using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Essentials;

using CompanyAppTask.Views;
using CompanyAppTask.Services;
using CompanyAppTask.Models;

namespace CompanyAppTask.ViewModels
{
    public class DepartmentListViewModel : BaseViewModel
    {

        public ObservableCollection<Department> departments { get; }

        private Department selectedDepartment;

        public Department SelectedDepartment
        {
            get { return selectedDepartment; }
            set { SetProperty(ref selectedDepartment, value); }
        }

        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }
        public ICommand GoToNewCommand { private set; get; }

        public async Task LoadData()
        {
            IsBusy = true;
            departments.Clear();

            List<Department> list = await CompanyAppServices.GetItems<Department>("api/Departments");

            foreach (Department item in list)
            {
                departments.Add(item);
            }

            IsBusy = false;
        }

        public async Task GoToDetails(Department dp)
        {
            if (SelectedDepartment != null)
            {

                DepartmentDetailsViewModel vm = new DepartmentDetailsViewModel(dp);
                DepartmentDetailsView page = new DepartmentDetailsView();

                vm.SelectedDepartment = SelectedDepartment;

                page.BindingContext = vm;

                await App.Current.MainPage.Navigation.PushAsync(page);
            }

        }

        public async Task GoToNew()
        {
            
                Department dp = new Department();

                DepartmentDetailsViewModel vm = new DepartmentDetailsViewModel(dp);
                DepartmentDetailsView page = new DepartmentDetailsView();


                page.BindingContext = vm;

                await App.Current.MainPage.Navigation.PushAsync(page);
            
        }


        public DepartmentListViewModel()
        {
            departments = new ObservableCollection<Department>();

            LoadDataCommand = new Command(async () => await LoadData());
            GoToDetailsCommand = new Command(async () => await GoToDetails(SelectedDepartment));
            GoToNewCommand = new Command(async () => await GoToNew());
        }
    }
}
