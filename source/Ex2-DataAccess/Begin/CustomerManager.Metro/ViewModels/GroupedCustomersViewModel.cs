namespace CustomerManager.Metro.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using CustomerManager.Metro.Common;
    using CustomerManager.Metro.DataModel;
    using CustomerManager.Models;

    public class GroupedCustomersViewModel : BindableBase
    {
        public ObservableCollection<CustomerViewModel> CustomersList { get; set; }

        public GroupedCustomersViewModel()
        {
            this.CustomersList = new ObservableCollection<CustomerViewModel>();

            this.GetCustomers();
        }

        private async void GetCustomers()
        { 
            IEnumerable<Customer> customers = await CustomersWebApiClient.GetCustomers();

            foreach (var customer in customers)
            {
                this.CustomersList.Add(new CustomerViewModel(customer));                
            }        
        }
    }
}
