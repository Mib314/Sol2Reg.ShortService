// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO\ModuleDataValidator.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO\ModuleDataValidator.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO\ModuleDataValidator.cs
//     Created            : 22.01.2013 02:39
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using Sol2Reg.IO.Interface;
	using Sol2Reg.ServiceData;
	using Sol2Reg.ServiceData.Enumerations;

	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class ModuleDataValidator
	{
		/// <summary>Validats the data.</summary>
		/// <returns>True if ok.</returns>
		public virtual bool ValidatData(IModuleBase module)
		{
			return (string.IsNullOrWhiteSpace(module.IpAddress) || string.IsNullOrWhiteSpace(module.Name) || module.Port > 81 || !module.Chanels.Any() || this.ValidChanel(module.Chanels));
		}

		/// <summary>Valids the chanel.</summary>
		/// <param name="chanels" >The chanels.</param>
		/// <returns>True if valid.</returns>
		private bool ValidChanel(IList<IChanel> chanels)
		{
			foreach (var chanel in chanels)
			{
				if (chanel.Id < 0 ||
					string.IsNullOrWhiteSpace(chanel.Key))
				{
					return false;
				}
				if (chanel.TypeOfValue == TypeOfValue.Analog)
				{
					if (Math.Abs(chanel.Gain - 0) < GlobalVariables.FloatPrecision ||
						chanel.ValueAnalogBrut == null)
					{
						return false;
					}
				}
				else
				{
					if (chanel.ValueDigital == null)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}