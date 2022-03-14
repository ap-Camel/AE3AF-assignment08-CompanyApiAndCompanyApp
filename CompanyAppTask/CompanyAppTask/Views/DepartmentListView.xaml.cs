using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CompanyAppTask.ViewModels;

namespace CompanyAppTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DepartmentListView : ContentPage
    {
        DepartmentListViewModel vm;

        public DepartmentListView()
        {
            InitializeComponent();

            vm = new DepartmentListViewModel();
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Run(() => vm.LoadDataCommand.Execute(null));
            vm.SelectedDepartment = null;
        }
    }
}