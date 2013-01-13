// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO.Interface\ISimulatorModule.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO.Interface\ISimulatorModule.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO.Interface\ISimulatorModule.cs
//     Created            : 28.12.2012 11:50
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ModuleIO.Interface.Simulator
{
	public interface ISimulatorModule
	{
		/// <summary>Injects the new analog input value.</summary>
		/// <param name="idChanel" >The id chanel.</param>
		/// <param name="value" >The value.</param>
		void InjectNewAnalogInputValue(int idChanel, float? value);

		/// <summary>Injects the new digital input value.</summary>
		/// <param name="idChanel" >The id chanel.</param>
		/// <param name="value" >
		///     if set to <c>true</c> [value].
		/// </param>
		void InjectNewDigitalInputValue(int idChanel, bool? value);
	}
}