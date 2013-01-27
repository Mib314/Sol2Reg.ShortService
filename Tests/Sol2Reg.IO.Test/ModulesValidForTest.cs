// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Test\ModulesValidForTest.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Test\ModulesValidForTest.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Test\ModulesValidForTest.cs
//     Created            : 22.01.2013 15:49
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Test
{
	using System.Collections.Generic;
	using Moq;
	using Sol2Reg.IO.Interface;
	using Sol2Reg.ServiceData.Enumerations;
	using Xunit;

	public class ModulesValidForTest
	{
		public const string Module_Name = "MesureTemp";
		public const string Module_Serie = "Adam6000";
		public const string Module_Type = "Adam6015";
		public const string Module_IP = "192.168.200.150";
		public const int Module_Port = 520;
		public const int Chanel_Id = 1;
		public const string Chanel_Key = "KEY";
		public const Direction Chanel_Direction = Direction.Input;
		public const TypeOfValue Chanel_Type_Of_Value = TypeOfValue.Analog;
		public const float Chanel_Gain = 1;
		public const float Chanel_Offset = 0;
		public const string Chanel_Description = "Chanel_Description";
		public const string Chanel_Comment = "Chanel_Comment";

		public static void GetModulesValid(IModuleBase moduleBase)
		{
			moduleBase.IpAddress = Module_IP;
			moduleBase.Name = Module_Name;
			moduleBase.Port = Module_Port;
			moduleBase.Chanels = GetChanelsValid();
		}

		public IModules GetModules()
		{
			var initializer = new Mock<InitializeModules>();
			initializer.Setup(foo => foo.AddModule(It.IsAny<string>(), It.IsAny<string>())).Returns(new ModuleTest());
			var modules = new Modules();
			modules.AddNewModule(Module_Name, Module_Type, Module_Serie, Module_IP, Module_Port, GetChanelsValid());
			return modules;
		}

		public static List<IChanel> GetChanelsValid()
		{
			return new List<IChanel>
								 {
									 new Chanel(Chanel_Id, Chanel_Key, Chanel_Direction, Chanel_Type_Of_Value) {Gain = Chanel_Gain, Offset = Chanel_Offset}
								 };
		}


		public static void CheckValidModule(IModules resultModules, int positionModule = 0)
		{
			var module = resultModules.ModuleList[positionModule];
			Assert.NotNull(module);
			Assert.Equal(Module_Name, module.Name);
			Assert.Equal(Module_Serie, module.ModuleSerie);
			Assert.Equal(Module_Type, module.ModuleModel);
			Assert.Equal(Module_IP, module.IpAddress);
			Assert.Equal(Module_Port, module.Port);
		}

		public static void CheckValidChanel(IModules resultModules, int positionModule = 0, int positionChanel = 0)
		{
			var module = resultModules.ModuleList[positionModule];
			Assert.NotNull(module);
			var chanel = module.Chanels[positionChanel];
			Assert.NotNull(chanel);
			Assert.Equal(Chanel_Id, chanel.Id);
			Assert.Equal(Chanel_Key, chanel.Key);
			Assert.Equal(Chanel_Direction, chanel.Direction);
			Assert.Equal(Chanel_Type_Of_Value, chanel.TypeOfValue);
			Assert.Equal(Chanel_Gain, chanel.Gain);
			Assert.Equal(Chanel_Offset, chanel.Offset);
			Assert.Equal(Chanel_Description, chanel.Description);
			Assert.Equal(Chanel_Comment, chanel.Comment);
		}

	}
}