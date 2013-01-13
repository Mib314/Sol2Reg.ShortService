// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IResolver.cs" company="Identitas AG">
//   Copyright © Identitas AG, 2011 . All rights reserved.
// </copyright>
// <summary>
//   Defines the IResolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sol2Reg.Supervisor
{
	/// <summary>
	/// A wrapper around a unity container to allow only the resolving of objects.
	/// </summary>
	public interface IResolver
	{
		/// <summary>
		/// Resolves the specified object with the given constructor parameters.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="p_ConstructorParameters">The constructor parameters.</param>
		/// <returns>An instance of <typeparamref name="TObject"/>.</returns>
		TObject Resolve<TObject>(params object[] p_ConstructorParameters);
	}
}
