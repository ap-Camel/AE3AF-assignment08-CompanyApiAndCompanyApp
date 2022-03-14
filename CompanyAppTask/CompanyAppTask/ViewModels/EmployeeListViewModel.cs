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
    public class EmployeeListViewModel : BaseViewModel
    {

        public ObservableCollection<Employee> employees { get; }

        private Employee selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set { SetProperty(ref selectedEmployee, value); }
        }

        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }
        public ICommand GoToNewCommand { private set; get; }

        public async Task LoadData()
        {
            IsBusy = true;
            employees.Clear();

            List<Employee> list = await CompanyAppServices.GetItems<Employee>("api/Employees");

            foreach (Employee item in list)
            {
                employees.Add(item);
            }

            IsBusy = false;
        }

        public async Task GoToDetails(Employee ep)
        {
            if (SelectedEmployee != null)
            {

                EmployeeDetailsViewModel vm = new EmployeeDetailsViewModel(ep);
                EmployeeDetailsView page = new EmployeeDetailsView();

                vm.SelectedEmployee = SelectedEmployee;

                page.BindingContext = vm;

                await App.Current.MainPage.Navigation.PushAsync(page);
            }

        }

        public async Task GoToNew()
        {

            Employee dp = new Employee();

            EmployeeDetailsViewModel vm = new EmployeeDetailsViewModel(dp);
            EmployeeDetailsView page = new EmployeeDetailsView();


            page.BindingContext = vm;

            await App.Current.MainPage.Navigation.PushAsync(page);

        }


        public EmployeeListViewModel()
        {
            employees = new ObservableCollection<Employee>();

            LoadDataCommand = new Command(async () => await LoadData());
            GoToDetailsCommand = new Command(async () => await GoToDetails(SelectedEmployee));
            GoToNewCommand = new Command(async () => await GoToNew());
        }

    }
}
