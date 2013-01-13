// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Service\ISol2RegService.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Service\ISol2RegService.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Service\ISol2RegService.cs
//     Created            : 31.12.2012 15:52
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Service
{
	using System.ServiceModel;
	using Sol2Reg.ServiceData;

	[ServiceContract]
	public interface ISol2RegService
	{
		/// <summary>Initilizes the connection.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		[OperationContract]
		void InitilizeConnection(string moduleName);

		/// <summary>Closes the connection.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		[OperationContract]
		void CloseConnection(string moduleName);

		/// <summary>Reads the data.</summary>
		/// <returns></returns>
		[OperationContract]
		ModulesData ReadData();

		/// <summary>Writes the data.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		/// <param name="chanelId" >The chanel id.</param>
		/// <param name="analogValue" >The analog value.</param>
		/// <param name="digitalValue" >The digital value.</param>
		[OperationContract]
		void WriteData(string moduleName, int chanelId, float? analogValue, bool? digitalValue);

		#region
		/// <summary>Reads the config in memory.</summary>
		/// <returns>The config of all modules.</returns>
		[OperationContract]
		ModulesData ReadConfig();

		/// <summary>Adds the new module.</summary>
		/// <param name="newModule" >The new module.</param>
		[OperationContract]
		void AddNewModule(ModuleData newModule);

		/// <summary>Deletes the module.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		[OperationContract]
		void DeleteModule(string moduleName);

		/// <summary>Changes the module info.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		/// <param name="moduleNewName" >New name of the module.</param>
		/// <param name="newIpAddress" >The new ip address.</param>
		/// <param name="newPort" >The new port.</param>
		[OperationContract]
		void ChangeModuleInfo(string moduleName, string moduleNewName, string newIpAddress, int? newPort);

		/// <summary>Changes the chanel value correction.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		/// <param name="chanelId" >The chanel id.</param>
		/// <param name="newGain" >The new gain.</param>
		/// <param name="newOffset" >The new offset.</param>
		[OperationContract]
		void ChangeChanelValueCorrection(string moduleName, int chanelId, float newGain, float newOffset);

		/// <summary>Changes the chanel info.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		/// <param name="chanelId" >The chanel id.</param>
		/// <param name="newName" >The new name.</param>
		/// <param name="newDesctiprion" >The new desctiprion.</param>
		/// <param name="newComment" >The new comment.</param>
		[OperationContract]
		void ChangeChanelInfo(string moduleName, int chanelId, string newName, string newDesctiprion, string newComment);

		/// <summary>Saves the config change.</summary>
		[OperationContract]
		void SaveConfigChange();
		#endregion
	}
}