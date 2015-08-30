using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;

namespace ZzaDesktop.Customers
{
	class AddEditCustomerViewModel : BindableBase
	{
		private bool _EditMode;

		public bool EditMode
		{
			get { return _EditMode; }
			set { SetProperty(ref _EditMode, value); }
		}
		public AddEditCustomerViewModel()
		{
			CancelCommand = new RelayCommand(OnCancel);
			SaveCommand = new RelayCommand(OnSave, CanSave);
		}

		private SimpleEditableCustomer _Customer;

		public SimpleEditableCustomer Customer
		{
			get { return _Customer; }
			set { SetProperty(ref _Customer, value); }
		}

		public RelayCommand CancelCommand {get; private set;}
		public RelayCommand SaveCommand {get; private set;}
		public event Action Done = delegate { };
  
		private void OnCancel()
		{
			Done();
		}
		private void OnSave()
		{
			Done();
		}
		private bool CanSave()
		{
			return true;
		}
		private Customer _editingCustomer = null;

		public void SetCustomer(Customer cust)
		{
			_editingCustomer = cust;
			Customer = new SimpleEditableCustomer();
			CopyCustomer(cust, Customer);
		}

		private void CopyCustomer(Customer source, SimpleEditableCustomer target)
		{
			target.Id = source.Id;
			if (EditMode)
			{
				target.FirstName = source.FirstName;
				target.LastName = source.LastName;
				target.Phone = source.Phone;
				target.Email = source.Email;
			}
		}
	}
}
