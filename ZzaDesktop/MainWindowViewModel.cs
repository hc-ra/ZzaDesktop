using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaDesktop.Customers;
using ZzaDesktop.OrderPrep;
using ZzaDesktop.Orders;
using Zza.Data;
namespace ZzaDesktop
{
	class MainWindowViewModel : BindableBase
	{
		private CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
		private OrderViewModel _orderViewModel = new OrderViewModel();
		private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
		private AddEditCustomerViewModel _addEditViewModel = new AddEditCustomerViewModel();

		private BindableBase _currentViewModel;

		public MainWindowViewModel()
		{
			NavCommand = new RelayCommand<string>(OnNav);
			_customerListViewModel.PlaceOrderRequested += NavToOrder;
			_customerListViewModel.AddCustomerRequested += NavToAddCustomer;
			_customerListViewModel.EditCustomerRequested += NavToEditCustomer;
			_addEditViewModel.Done +=
				() => CurrentViewModel = _customerListViewModel;
		}

		private void NavToAddCustomer(Customer customer)
		{
			_addEditViewModel.EditMode = false;
			_addEditViewModel.SetCustomer(customer);
			CurrentViewModel = _addEditViewModel;
		}

		private void NavToEditCustomer(Customer customer)
		{
			_addEditViewModel.EditMode = true;
			_addEditViewModel.SetCustomer(customer);
			CurrentViewModel = _addEditViewModel;
		}


		private void NavToOrder(Guid customerId)
		{
			_orderViewModel.CustomerId = customerId;
			CurrentViewModel = _orderViewModel;
		}
		public BindableBase CurrentViewModel
		{
			get { return _currentViewModel; }
			set { SetProperty(ref _currentViewModel, value); }
		}

		public RelayCommand<string> NavCommand { get; private set; }
		private void OnNav(string destination)
		{
			switch (destination)
			{
				case "orderPrep":
					CurrentViewModel = _orderPrepViewModel;
					break;
				case "customers":
				default:
					CurrentViewModel = _customerListViewModel;
					break;
			}
		}

	}
}
