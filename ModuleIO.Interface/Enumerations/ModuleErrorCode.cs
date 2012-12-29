// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO.Interface\ModuleErrorList.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO.Interface\ModuleErrorList.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO.Interface\ModuleErrorList.cs
//     Created            : 28.12.2012 21:17
// </FileInfo>
//  ----------------------------------------------------------------------------------
namespace ModuleIO.Interface.Enumerations
{

	/// <summary>
	/// Enum of Error code for Module
	/// </summary>
	/// <remarks>This list is based of Advantech ADAM Error code.</remarks>
	public enum ModuleErrorCode
	{
		No_Error = 0,

		#region Sol2Reg Error
		#region Module
		Module_NotFind = 100,
		Module_NotInitialized = 101,
		Module_NotConnected = 102,
		Module_StartChanel_Error = 150,
		#endregion

		#region Chanel
		Invalid_Chanel_Id = 200,
		Invalid_Chanel_TypeOfValue = 201,
		Invalie_Chanel_Direction = 202,
		#endregion
		#endregion

		#region Advantech ADAM Error code
		ComPort_Error = 1073741825,
		ComPort_Open_Fail = 1073741826,
		ComPort_Send_Fail = 1073741827,
		ComPort_Recv_Fail = 1073741828,
		Socket_Null = 1073807361,
		Socket_Connect_Fail = 1073807362,
		Socket_Invalid_IP = 1073807363,
		Socket_Send_Fail = 1073807364,
		Socket_Recv_Fail = 1073807365,
		Socket_Unknown = 1073872895,
		Adam_Invalid_Head = 1074266113,
		Adam_Invalid_End = 1074266114,
		Adam_Invalid_Length = 1074266115,
		Adam_Invalid_Data = 1074266116,
		Adam_Invalid_Checksum = 1074266117,
		Adam_Invalid_Param = 1074266118,
		Adam_Invalid_Password = 1074266119,
		Modbus_Invalid_CRC = 1074331649,
		Modbus_Invalid_Length = 1074331650,
		Modbus_Invalid_Serial = 1074331651,
		Modbus_Exception = 1074335744,
		Modbus_Exception_IllegalFunction = 1074335745,
		Modbus_Exception_IllegalDataAddress = 1074335746,
		Modbus_Exception_IllegalDataValue = 1074335747,
		Modbus_Exception_SlaveDeviceFailure = 1074335748,
		Modbus_Exception_Acknowledge = 1074335749,
		Modbus_Exception_SlaveDeviceBusy = 1074335750,
		Modbus_Exception_NativeAcknowledge = 1074335751,
		Modbus_Exception_MemoryParityError = 1074335752,
		Adam_Null_Error = 1074397185,
		Command_Nack = 1074462721,
		Command_Unknown = 1074462722,
		API_Parameter_Error = 1074462723,
		#endregion
			}
}