using System;
using System.Windows.Input;

namespace Spillway.Utilities
{
	public class RelayCommand : ICommand
	{
		#region Members

		private readonly Action<object> _action;
		private readonly Predicate<object> _actionCanExecute;

		#endregion Members

		#region Constructors

		public RelayCommand(Action<object> action)
			: this(action, null)
		{
		}

		public RelayCommand(Action<object> action, Predicate<object> actionCanExecute)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			_action = action;
			_actionCanExecute = actionCanExecute;
		}

		#endregion Constructors

		#region ICommand  Members

		public bool CanExecute(object parameter = null)
		{
			return _actionCanExecute == null ? true : _actionCanExecute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter = null)
		{
			_action(parameter);
		}

		#endregion ICommand  Members
	}
}