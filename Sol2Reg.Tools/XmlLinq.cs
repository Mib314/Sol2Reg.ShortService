// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\XmlLinq.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\XmlLinq.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\XmlLinq.cs
//     Created            : 22.01.2013 01:29
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools
{
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Xml.Linq;

	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class XmlLinq
	{
		/// <summary>Reads the attribute.</summary>
		/// <param name="attributName" >Name of the attribut.</param>
		/// <param name="element" >The element.</param>
		/// <param name="defaultValue" >The default value.</param>
		/// <returns>String value.</returns>
		public string ReadAttribute(string attributName, XElement element, string defaultValue = "")
		{
			if (!element.HasAttributes)
			{
				return string.Empty;
			}

			var attribut = element.Attributes().FirstOrDefault(foo => foo.Name == attributName);
			if (attribut == null)
			{
				return defaultValue;
			}

			return attribut.Value;
		}

		/// <summary>Reads the attribute.</summary>
		/// <param name="attributName" >Name of the attribut.</param>
		/// <param name="element" >The element.</param>
		/// <param name="defaultValue" >The default value.</param>
		/// <returns>Integer value</returns>
		public int ReadAttribute(string attributName, XElement element, int defaultValue)
		{
			var i = 0;
			if (ConvertTo(this.ReadAttribute(attributName, element, string.Empty), ref i))
			{
				return i;
			}
			return defaultValue;
		}

		/// <summary>Reads the attribute.</summary>
		/// <param name="attributName" >Name of the attribut.</param>
		/// <param name="element" >The element.</param>
		/// <param name="defaultValue" >The default value.</param>
		/// <returns>Integer value</returns>
		public float ReadAttribute(string attributName, XElement element, float defaultValue)
		{
			var i = defaultValue;
			if (ConvertTo(this.ReadAttribute(attributName, element, string.Empty), ref i))
			{
				return i;
			}
			return defaultValue;
		}

		/// <summary>Converts to int.</summary>
		/// <param name="value" >The value.</param>
		/// <param name="valueToAssigne" >The value to assigne.</param>
		/// <returns>the integer value</returns>
		public bool ConvertTo(string value, ref int valueToAssigne)
		{
			int tempValue;
			if (int.TryParse(value, out tempValue))
			{
				valueToAssigne = tempValue;
				return true;
			}
			return false;
		}
		/// <summary>Converts to float.</summary>
		/// <param name="value" >The value.</param>
		/// <param name="valueToAssigne" >The value to assigne.</param>
		/// <returns>the integer value</returns>
		public bool ConvertTo(string value, ref float valueToAssigne)
		{
			float tempValue;
			if (float.TryParse(value, out tempValue))
			{
				valueToAssigne = tempValue;
				return true;
			}
			return false;
		}
	}
}