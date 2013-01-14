// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Supervisor\SystemConfiguration.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Supervisor\SystemConfiguration.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Supervisor\SystemConfiguration.cs
//     Created            : 07.01.2013 01:26
// </FileInfo>
//  ----------------------------------------------------------------------------------
/*
namespace TVKplus.Client.Infrastructure.DependencyInjection
{
	using System;
	using System.Linq;
	using Microsoft.Practices.Unity;
	using Sol2Reg.Supervisor;

	/// <summary>A static class representing the system configuration.</summary>
	public class SystemConfiguration
	{
		private static IUnityContainer s_UnityContainer;

		/// <summary>Gets the resolver.</summary>
		public static IResolver Resolver
		{
			get
			{
				// Fix for testing purposes
				if (s_UnityContainer == null)
				{
					s_UnityContainer = new UnityContainer();
					RegisterBindings();
				}

				return new ResolverProvider(s_UnityContainer);
			}
		}

		/// <summary>Registers the unity container.</summary>
		/// <param name="p_UnityContainer" >The p_ unity container.</param>
		public static void RegisterUnityContainer(IUnityContainer p_UnityContainer)
		{
			s_UnityContainer = p_UnityContainer;
			RegisterBindings();
		}

		/// <summary>Registers the types.</summary>
		private static void RegisterBindings()
		{
			s_UnityContainer.RegisterInstance<IResolver>(new ResolverProvider(s_UnityContainer));
			//s_UnityContainer.RegisterType<IMessagesToUser, MessagesToUser>(new ContainerControlledLifetimeManager());
			//s_UnityContainer.RegisterInstance<Func<ILookup<EnumAuthorisationGroup, RoleAuthGroupAuthLevel>>>(() => userContextProvider.Authorizations);
			//s_UnityContainer.RegisterInstance(userContextProvider);
		}

		#region Nested type: ResolverProvider
		private class ResolverProvider : IResolver
		{
			private readonly IUnityContainer m_Container;

			public ResolverProvider(IUnityContainer p_Container)
			{
				this.m_Container = p_Container;
			}

			#region IResolver Members
			/// <summary>Resolves the specified object with the given constructor parameters.</summary>
			/// <typeparam name="TObject" >The type of the object.</typeparam>
			/// <param name="p_ConstructorParameters" >The constructor parameters.</param>
			/// <returns>
			///     An instance of <typeparamref name="TObject" />.
			/// </returns>
			public TObject Resolve<TObject>(params object[] p_ConstructorParameters)
			{
				if (p_ConstructorParameters.Length > 0)
				{
					var temporaryContainer = this.m_Container.CreateChildContainer();
					foreach (var constructorParameter in p_ConstructorParameters)
					{
						temporaryContainer.RegisterInstance(constructorParameter.GetType(), constructorParameter);

						// Hack: Register Interfaces since the method above will always use the concrete type.
						var listOfInterfaces = constructorParameter.GetType().GetInterfaces();
						foreach (var interfaceType in listOfInterfaces)
						{
							temporaryContainer.RegisterInstance(interfaceType, constructorParameter);
						}
					}

					return temporaryContainer.Resolve<TObject>();
				}

				return this.m_Container.Resolve<TObject>();
			}
			#endregion
		}
		#endregion
	}
}
*/