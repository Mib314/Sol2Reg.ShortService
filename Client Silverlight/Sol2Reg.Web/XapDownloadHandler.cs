// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Web\XapDownloadHandler.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Web\XapDownloadHandler.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Web\XapDownloadHandler.cs
//     Created            : 13.01.2013 22:11
// </FileInfo>
// <Remarque>
//		===================================================================================
//		 Microsoft patterns & practices
//		 Composite Application Guidance for Windows Presentation Foundation and Silverlight
//		===================================================================================
//		 Copyright (c) Microsoft Corporation.  All rights reserved.
//		 THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//		 OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//		 LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//		 FITNESS FOR A PARTICULAR PURPOSE.
//		===================================================================================
//		 The example companies, organizations, products, domain names,
//		 e-mail addresses, logos, people, places, and events depicted
//		 herein are fictitious.  No association with any real company,
//		 organization, product, domain name, email address, logo, person,
//		 places, or events is intended or should be inferred.
//		===================================================================================
// </Remarque>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Web
{
	using System;
	using System.Globalization;
	using System.IO;
	using System.Threading;
	using System.Web;

	/// <summary>Slows the incremental download of XAP files for Modularity Quickstart purposes.</summary>
	public class XapDownloadHandler : IHttpHandler
	{
		// The default percentage of the file size per chunk transmitted is 10%
		private const double DEFAULT_TRANSMIT_CHUNK_PERCENT = 0.1;

		/// <summary>
		///     Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
		/// </summary>
		/// <value></value>
		/// <returns>
		///     true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
		/// </returns>
		public bool IsReusable
		{
			get { return true; }
		}

		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
		/// </summary>
		/// <param name="p_Context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		public void ProcessRequest(HttpContext p_Context)
		{
			var physicalPath = p_Context.Server.MapPath(p_Context.Request.Path);
			SlowlyTransmitFile(p_Context, physicalPath, 300);
		}

		// This method provides the actual slow-down and incremental transmittion.
		private static void SlowlyTransmitFile(HttpContext p_Context, string p_Path, int p_MilliSecondsDelayPerChunk)
		{
			// The output must be unbufferred to allow for the client to receive chunks separately.
			p_Context.Response.BufferOutput = false;

			// So that the client can display progress as chunks are downloaded, we output the Content-length header.
			var fileSize = -1L;
			if (File.Exists(p_Path))
			{
				var fileInfo = new FileInfo(p_Path);
				fileSize = fileInfo.Length;
			}

			p_Context.Response.AppendHeader("Content-Length", fileSize.ToString(CultureInfo.InvariantCulture));

			// Read the file and calculate the number of even chunks + the leftover chunk.
			var moduleBuffer = File.ReadAllBytes(p_Path);
			byte[] chunkBufer;
			var chunkSize = (int)(moduleBuffer.Length * DEFAULT_TRANSMIT_CHUNK_PERCENT);
			var chunkCount = moduleBuffer.Length / chunkSize;
			var leftOverSize = moduleBuffer.Length % chunkSize;
			var i = 0;

			// Send each chunk and wait per chunck set.
			if (chunkCount > 0)
			{
				chunkBufer = new byte[chunkSize];
				while (i < chunkCount * chunkSize)
				{
					Array.Copy(moduleBuffer, i, chunkBufer, 0, chunkSize);
					p_Context.Response.BinaryWrite(chunkBufer);
					i += chunkSize;

					Thread.Sleep(p_MilliSecondsDelayPerChunk);
				}
			}

			// Send the last leftover chunk (no waiting).
			if (leftOverSize != 0)
			{
				chunkBufer = new byte[leftOverSize];
				Array.Copy(moduleBuffer, i, chunkBufer, 0, leftOverSize);
				p_Context.Response.BinaryWrite(chunkBufer);
			}
		}
	}
}

		///// <summary>
		/////     Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
		///// </summary>
		///// <param name="context" >
		/////     An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.
		///// </param>
		//public void ProcessRequest(HttpContext context)
		//{
		//	var physicalPath = context.Server.MapPath(context.Request.Path);

		//	// Different modules are slowed down by different amounts for illustration purposes in the quickstart.
		//	if (context.Request.Path.Contains("ModuleB")) this.SlowlyTransmitFile(context, physicalPath, 100);
		//	else if (context.Request.Path.Contains("ModuleD")) this.SlowlyTransmitFile(context, physicalPath, 200);
		//	else if (context.Request.Path.Contains("ModuleE"))
		//	{
		//		// Module F depends on Module E, so I make it take much longer so that the difference is visible in the quickstart.
		//		this.SlowlyTransmitFile(context, physicalPath, 600);
		//	}
		//	else if (context.Request.Path.Contains("ModuleF")) this.SlowlyTransmitFile(context, physicalPath, 300);
		//	else this.SlowlyTransmitFile(context, physicalPath, 0);
		//}
		//#endregion

		//// This method provides the actual slow-down and incremental transmittion.
		//private void SlowlyTransmitFile(HttpContext context, string path, int milliSecondsDelayPerChunk)
		//{
		//	// The output must be unbufferred to allow for the client to receive chunks separately.
		//	context.Response.BufferOutput = false;

		//	// So that the client can display progress as chunks are downloaded, we output the Content-length header.
		//	var fileSize = -1L;
		//	if (File.Exists(path))
		//	{
		//		var fileInfo = new FileInfo(path);
		//		fileSize = fileInfo.Length;
		//	}
		//	context.Response.AppendHeader("Content-Length", fileSize.ToString(CultureInfo.CurrentCulture));

		//	// Read the file and calculate the number of even chunks + the leftover chunk.
		//	var moduleBuffer = File.ReadAllBytes(path);
		//	byte[] chunkBufer;
		//	var chunkSize = (int) (moduleBuffer.Length*DEFAULT_TRANSMIT_CHUNK_PERCENT);
		//	var chunkCount = moduleBuffer.Length/chunkSize;
		//	var leftOverSize = moduleBuffer.Length%chunkSize;
		//	var i = 0;

		//	// Send each chunk and wait per chunck set.
		//	if (chunkCount > 0)
		//	{
		//		chunkBufer = new byte[chunkSize];
		//		while (i < chunkCount*chunkSize)
		//		{
		//			Array.Copy(moduleBuffer, i, chunkBufer, 0, chunkSize);
		//			context.Response.BinaryWrite(chunkBufer);
		//			i += chunkSize;

		//			Thread.Sleep(milliSecondsDelayPerChunk);
		//		}
		//	}

		//	// Send the last leftover chunk (no waiting).
		//	if (leftOverSize != 0)
		//	{
		//		chunkBufer = new byte[leftOverSize];
		//		Array.Copy(moduleBuffer, i, chunkBufer, 0, leftOverSize);
		//		context.Response.BinaryWrite(chunkBufer);
		//	}
		//}
/*
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XapDownloadHandler.cs" company="Identitas AG">
//   Copyright © Identitas AG, 2011 . All rights reserved.
// </copyright>
// <summary>
//   Slows the incremental download of XAP files for Modularity Quickstart purposes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ClientGUI.Web
{
	using System;
	using System.IO;
	using System.Threading;
	using System.Web;

	/// <summary>
	/// Slows down the incremental download of XAP files for busy indicator tests.
	/// </summary>
	public class XapDownloadHandler : IHttpHandler
	{
		// The default percentage of the file size per chunk transmitted is 10%
		private const double DefaultTransmitChunkPercent = 0.1;

		/// <summary>
		/// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
		/// </summary>
		/// <value></value>
		/// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
		public bool IsReusable
		{
			get { return true; }
		}

		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
		/// </summary>
		/// <param name="p_Context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		public void ProcessRequest(HttpContext p_Context)
		{
			var physicalPath = p_Context.Server.MapPath(p_Context.Request.Path);
			SlowlyTransmitFile(p_Context, physicalPath, 300);
		}

		// This method provides the actual slow-down and incremental transmittion.
		private static void SlowlyTransmitFile(HttpContext p_Context, string p_Path, int p_MilliSecondsDelayPerChunk)
		{
			// The output must be unbufferred to allow for the client to receive chunks separately.
			p_Context.Response.BufferOutput = false;

			// So that the client can display progress as chunks are downloaded, we output the Content-length header.
			var fileSize = -1L;
			if (File.Exists(p_Path))
			{
				var fileInfo = new FileInfo(p_Path);
				fileSize = fileInfo.Length;
			}

			p_Context.Response.AppendHeader("Content-Length", fileSize.ToString());

			// Read the file and calculate the number of even chunks + the leftover chunk.
			var moduleBuffer = File.ReadAllBytes(p_Path);
			byte[] chunkBufer;
			var chunkSize = (int)((double)moduleBuffer.Length * DefaultTransmitChunkPercent);
			var chunkCount = moduleBuffer.Length / chunkSize;
			var leftOverSize = moduleBuffer.Length % chunkSize;
			var i = 0;

			// Send each chunk and wait per chunck set.
			if (chunkCount > 0)
			{
				chunkBufer = new byte[chunkSize];
				while (i < chunkCount * chunkSize)
				{
					Array.Copy(moduleBuffer, i, chunkBufer, 0, chunkSize);
					p_Context.Response.BinaryWrite(chunkBufer);
					i += chunkSize;

					Thread.Sleep(p_MilliSecondsDelayPerChunk);
				}
			}

			// Send the last leftover chunk (no waiting).
			if (leftOverSize != 0)
			{
				chunkBufer = new byte[leftOverSize];
				Array.Copy(moduleBuffer, i, chunkBufer, 0, leftOverSize);
				p_Context.Response.BinaryWrite(chunkBufer);
			}
		}
	}
}
*/