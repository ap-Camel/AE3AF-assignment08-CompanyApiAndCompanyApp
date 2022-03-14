using CompanyAppTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CompanyAppTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeListView : ContentPage
    {
        EmployeeListViewModel vm;

        public EmployeeListView()
        {
  
            InitializeComponent();

            vm = new EmployeeListViewModel();
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Run(() => vm.LoadDataCommand.Execute(null));
            vm.SelectedEmployee = null;
        }
    }
}