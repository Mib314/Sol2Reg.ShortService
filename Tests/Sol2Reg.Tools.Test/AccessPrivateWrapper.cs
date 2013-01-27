// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessPrivateWrapper.cs" company="Identitas AG">
//   Copyright © Identitas AG, 2011 . All rights reserved.
// </copyright>
// <summary>
//   AccessPrivateWrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sol2Reg.Tools.Test
{
	using System.Dynamic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// A 10 minute wrapper to access private members, havn't tested in detail.
	/// Use under your own risk - amazedsaint@gmail.com.
	/// </summary>
	public class AccessPrivateWrapper : DynamicObject
	{
		/// <summary>
		/// The object we are going to wrap
		/// </summary>
		private readonly object m_Wrapped;

		/// <summary>
		/// Specify the flags for accessing members
		/// </summary>
		private static BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance
			| BindingFlags.Static | BindingFlags.Public;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccessPrivateWrapper"/> class.
		/// </summary>
		/// <param name="p_Object">The object parameter.</param>
		public AccessPrivateWrapper(object p_Object)
		{
			this.m_Wrapped = p_Object;
		}

		/// <summary>
		/// Create an instance via the constructor matching the args.
		/// </summary>
		/// <param name="p_Asm">The asm param.</param>
		/// <param name="p_Type">Type of the param.</param>
		/// <param name="p_Args">The args collection.</param>
		/// <returns>Instance object.</returns>
		public static dynamic FromType(Assembly p_Asm, string p_Type, params object[] p_Args)
		{
			var allt = p_Asm.GetTypes();
			var t = allt.First(p_Item => p_Item.Name == p_Type);

			var types = from a in p_Args
						select a.GetType();

			// Gets the constructor matching the specified set of args
			var ctor = t.GetConstructor(flags, null, types.ToArray(), null);

			if (ctor != null)
			{
				var instance = ctor.Invoke(p_Args);
				return new AccessPrivateWrapper(instance);
			}

			return null;
		}

		/// <summary>
		/// Try invoking a method.
		/// </summary>
		/// <param name="p_Binder">The binder.</param>
		/// <param name="p_Args">The args param of the call.</param>
		/// <param name="p_Result">The result.</param>
		/// <returns>Call is ok.</returns>
		public override bool TryInvokeMember(InvokeMemberBinder p_Binder, object[] p_Args, out object p_Result)
		{
			var types = from a in p_Args
						select a.GetType();

			var method = this.m_Wrapped.GetType().GetMethod(p_Binder.Name, flags, null, types.ToArray(), null);

			if (method == null)
			{
				return base.TryInvokeMember(p_Binder, p_Args, out p_Result);
			}

			p_Result = method.Invoke(this.m_Wrapped, p_Args);
			return true;
		}

		/// <summary>
		/// Tries to get a property or field with the given name.
		/// </summary>
		/// <param name="p_Binder">The binder.</param>
		/// <param name="p_Result">The result.</param>
		/// <returns>True is ok.</returns>
		public override bool TryGetMember(GetMemberBinder p_Binder, out object p_Result)
		{
			// Try getting a property of that name
			var prop = this.m_Wrapped.GetType().GetProperty(p_Binder.Name, flags);

			if (prop == null)
			{
				// Try getting a field of that name
				var fld = this.m_Wrapped.GetType().GetField(p_Binder.Name, flags);
				if (fld != null)
				{
					p_Result = fld.GetValue(this.m_Wrapped);
					return true;
				}

				return base.TryGetMember(p_Binder, out p_Result);
			}

			p_Result = prop.GetValue(this.m_Wrapped, null);
			return true;
		}

		/// <summary>
		/// Tries to set a property or field with the given name.
		/// </summary>
		/// <param name="p_Binder">The binder.</param>
		/// <param name="p_Value">The value.</param>
		/// <returns>True is ok.</returns>
		public override bool TrySetMember(SetMemberBinder p_Binder, object p_Value)
		{
			var prop = this.m_Wrapped.GetType().GetProperty(p_Binder.Name, flags);
			if (prop == null)
			{
				var fld = this.m_Wrapped.GetType().GetField(p_Binder.Name, flags);
				if (fld != null)
				{
					fld.SetValue(this.m_Wrapped, p_Value);
					return true;
				}

				return base.TrySetMember(p_Binder, p_Value);
			}

			prop.SetValue(this.m_Wrapped, p_Value, null);
			return true;
		}
	}
}