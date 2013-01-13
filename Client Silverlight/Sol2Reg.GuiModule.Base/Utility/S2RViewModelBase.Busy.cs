// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Supervisor\Class2.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Supervisor\Class2.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Supervisor\Class2.cs
//     Created            : 06.01.2013 23:16
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Supervisor.Utility
{
	using System.Collections.Generic;
	using GalaSoft.MvvmLight;

	/// <summary>Declare the IdViewModelBase type.</summary>
	/// <typeparam name="TViewModel" >The type of the view model.</typeparam>
	public abstract partial class S2RViewModelBase<TViewModel> : ViewModelBase
	{
		private const string IS_LOADING_REPORT = "IsLoadingReport";
		private const string IS_LOADING = "IsLoading";
		private const string IS_SAVING = "IsSaving";
		private readonly List<string> m_IsBusyStack = new List<string>();
		private readonly object m_Lock = new object();
		private bool m_IsBusy;

		/// <summary>Gets or sets a value indicating whether this instance is busy.</summary>
		/// <value>
		///     <c>true</c> if this instance is busy; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsBusy
		{
			get { return this.m_IsBusy; }

			set
			{
				this.m_IsBusy = value;
				this.RaisePropertyChanged("IsBusy");
			}
		}

		/// <summary>Show busy while the report is loading.</summary>
		public void AddIsLoadingReport()
		{
			this.AddIsBusy(IS_LOADING_REPORT);
		}

		/// <summary>Remove busy when the report is loaded.</summary>
		public void RemoveIsLoadingReport()
		{
			this.RemoveIsBusy(IS_LOADING_REPORT);
		}

		/// <summary>Adds the is loading.</summary>
		protected void AddIsLoading()
		{
			this.AddIsBusy(IS_LOADING);
		}

		/// <summary>Adds the is saving.</summary>
		protected void AddIsSaving()
		{
			this.AddIsBusy(IS_SAVING);
		}

		/// <summary>Determines whether [is busy active] [the specified p_ is busy name].</summary>
		/// <param name="p_IsBusyName" >Name of the is busy.</param>
		/// <returns>
		///     <c>true</c> if is busy active the specified is busy name; otherwise, <c>false</c>.
		/// </returns>
		protected bool IsBusyActive(string p_IsBusyName)
		{
			var ret = this.m_IsBusyStack.Contains(p_IsBusyName);
			return ret;
		}

		/// <summary>Adds a(nother) busy indicator.</summary>
		/// <param name="p_IsBusyName" >Name of the is busy.</param>
		protected void AddIsBusy(string p_IsBusyName)
		{
			lock (this.m_Lock)
			{
				this.m_IsBusyStack.Add(p_IsBusyName);
				this.IsBusy = true;
			}
		}

		/// <summary>Removes the is loading.</summary>
		protected void RemoveIsLoading()
		{
			this.RemoveIsBusy(IS_LOADING);
		}

		/// <summary>Removes the is saving.</summary>
		protected void RemoveIsSaving()
		{
			this.RemoveIsBusy(IS_SAVING);
		}

		/// <summary>Remove a(nother) busy indicator.</summary>
		/// <param name="p_IsBusyName" >Name of the is busy.</param>
		protected void RemoveIsBusy(string p_IsBusyName)
		{
			lock (this.m_Lock)
			{
				this.m_IsBusyStack.Remove(p_IsBusyName);
				this.IsBusy = this.m_IsBusyStack.Count > 0;
			}
		}

		/// <summary>Clears the is busy.</summary>
		protected void ClearIsBusy()
		{
			this.m_IsBusyStack.Clear();
			this.IsBusy = false;
		}
	}
}