// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdViewModelBase.cs" company="Identitas AG">
//   Copyright © Identitas AG, 2011 . All rights reserved.
// </copyright>
// <summary>
//   Identitas ViewModel Base class with IsBusy funtionality
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Sol2Reg.Supervisor.Utility
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using GalaSoft.MvvmLight;
	using GalaSoft.MvvmLight.Command;

	/// <summary>
	/// Identitas ViewModel Base class with IsBusy funtionality.
	/// </summary>
	/// <typeparam name="TViewModel">The type of the view model.</typeparam>
	public abstract partial class S2RViewModelBase<TViewModel> : ViewModelBase
	{
		private string m_PageId;
		private bool m_IsDirty;

		/// <summary>
		/// 	Occurs when IsDirty has changed on the model.
		/// </summary>
		public event EventHandler IsDirtyChanged;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is valid.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsValid { get; protected set; }

		/// <summary>
		/// Gets SaveChangesCommand.
		/// </summary>
		public RelayCommand SaveChangesCommand { get; private set; }

		/// <summary>
		/// 	Gets or sets a value indicating whether model is dirty i.e. it has changed.
		/// </summary>
		/// <value>
		/// <c>True</c> if the model is dirty; otherwise, <c>false</c>.
		/// </value>
		public bool IsDirty
		{
			get
			{
				return this.m_IsDirty;
			}

			set
			{
				this.m_IsDirty = value;

				this.OnIsDirty(this, new EventArgs());

				// Update bindings and inform listeners
				base.RaisePropertyChanged("IsDirty");
			}
		}

		/// <summary>
		/// Gets or sets the save action.
		/// </summary>
		protected Action SaveAction { get; set; }

		/// <summary>
		/// Gets or sets the page id.
		/// </summary>
		/// <value>
		/// The page id.
		/// </value>
		protected string PageId
		{
			get
			{
				if (this.m_PageId == null)
				{
					this.m_PageId = this.GetType().Name;

					if (this.m_PageId.EndsWith("ViewModel"))
					{
						this.m_PageId = this.m_PageId.Substring(0, this.m_PageId.LastIndexOf("ViewModel"));
					}
				}

				return this.m_PageId;
			}

			set
			{
				this.m_PageId = value;
			}
		}

		/// <summary>
		/// Gets the name of the model.
		/// </summary>
		/// <value>
		/// The name of the model.
		/// </value>
		protected virtual string ModelName
		{
			get
			{
				return this.PageId + "Model";
			}
		}

		/// <summary>
		/// Initializes the save changes.
		/// </summary>
		public void InitializeSaveChanges()
		{
			this.SaveChangesCommand = new RelayCommand(this.SaveChanges, this.CanSaveChanges);
		}

		/// <summary>
		/// The save changes.
		/// </summary>
		protected virtual void SaveChanges()
		{
			if (this.SaveAction != null)
			{
				this.AddIsSaving();
				this.SaveAction();
			}
		}

		/// <summary>
		/// Determines whether this instance [can save changes].
		/// </summary>
		/// <returns>
		///   <c>true</c> if this instance [can save changes]; otherwise, <c>false</c>.
		/// </returns>
		protected virtual bool CanSaveChanges()
		{
			return true;
		}

		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <typeparam name="TResult">
		/// The type of the result.
		/// </typeparam>
		/// <param name="p_Property">
		/// The property for which the event needs to be raised.
		/// </param>
		protected void RaisePropertyChanged<TResult>(Expression<Func<TViewModel, TResult>> p_Property)
		{
			var memberExpression = p_Property.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new InvalidOperationException("Could not cast to type MemberExpression.");
			}

			var propertyName = memberExpression.Member.Name;
			this.RaisePropertyChanged(propertyName);
		}

		/// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <typeparam name="TType">The type of the view model.</typeparam>
		/// <typeparam name="TResult">The return type of the property.</typeparam>
		/// <param name="p_Property">The property for which the event needs to be raised.</param>
		protected void RaisePropertyChanged<TType, TResult>(Expression<Func<TType, TResult>> p_Property)
		{
			var memberExpression = p_Property.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new InvalidOperationException("Could not cast to type MemberExpression.");
			}

			var propertyName = memberExpression.Member.Name;
			this.RaisePropertyChanged(propertyName);
		}

		/// <summary>
		/// 	Raises the property changed event for all the properties in the ViewModel.
		/// </summary>
		protected void RaiseAllPropertyChanged()
		{
			var allPropertiesNamesAsString = typeof(TViewModel).GetProperties().Select(p_Property => p_Property.Name);
			foreach (var propertyNameAsString in allPropertiesNamesAsString)
			{
				this.RaisePropertyChanged(propertyNameAsString);
			}
		}

		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <param name="p_Property">The property.</param>
		protected override void RaisePropertyChanged(string p_Property)
		{
			// Raise the property change on the mvvmlight toolkit. 
			base.RaisePropertyChanged(p_Property);
			this.RefreshPageStateAfterRaisePropertyChanged();
		}

		/// <summary>
		/// 	Raises the property changed.
		/// </summary>
		/// <typeparam name="TResult">
		/// The type of the result.
		/// </typeparam>
		/// <param name="p_Property">
		/// The property for which the event needs to be raised.
		/// </param>
		protected void RaisePropertyChanged<TResult>(params Expression<Func<TViewModel, TResult>>[] p_Property)
		{
			foreach (var func in p_Property)
			{
				this.RaisePropertyChanged<TResult>(func);
			}
		}

		private void RefreshPageStateAfterRaisePropertyChanged()
		{
			base.RaisePropertyChanged("IsValid");
			this.IsDirty = true;
			this.SaveChangesCommand.RaiseCanExecuteChanged();
		}

		private void OnIsDirty(object p_Sender, EventArgs p_Args)
		{
			var lo = this.IsDirtyChanged;
			if (lo != null)
			{
				lo(p_Sender, p_Args);
			}
		}
	}
}
