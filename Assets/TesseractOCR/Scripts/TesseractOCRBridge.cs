//------------------------------------------------------------------------------
// <copyright file="TesseractOCRBridge.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary> TODO: Describe the file's implementation overview here </summary>
//------------------------------------------------------------------------------

using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PixelSquare.TesseractOCR
{
	using Enums;

	/// <summary>
	/// TODO: Describe the class outline here
	/// </summary>
	/// <remarks> Detailed information about the class. </remarks>
	public class TesseractOCRBridge 
	{
		private const string DLL_NAME = "tesseract50";

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPICreate")]
		protected static extern IntPtr CreateHandle();

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIDelete")]
		protected static extern void DeleteHandle(IntPtr handle);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIClear")]
		protected static extern void ClearTesseract(IntPtr handle);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIEnd")]
		protected static extern void EndTesseract(IntPtr handle);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIInit3")]
		protected static extern int Initialize(IntPtr handle, string datapath, string language);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIInit2")]
		protected static extern int Initialize(IntPtr handle, string datapath, string language, int oem);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPISetImage")]
		protected static extern void SetImageData(IntPtr handle, byte[] imageData, int width, int height, int bytesPerPixel, int bytesPerLine);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetUTF8Text")]
		protected static extern IntPtr GetTextData(IntPtr handle);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPISetPageSegMode")]
		protected static extern void SetPageSegmentationMode(IntPtr handle, SegmentationMode mode);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetPageSegMode")]
		protected static extern int GetPageSegmentationMode(IntPtr handle);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetDatapath")]
		protected static extern IntPtr GetTesseractDataPath(IntPtr handle);

		[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetInitLanguagesAsString")]
		protected static extern IntPtr GetTesseractLanguage(IntPtr handle);

		// TODO: Might crash sometimes. Still looking for a fix
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIDetectOrientationScript")]
        protected static extern bool OrientationScript(IntPtr handle, out int degrees, out float confidence, out string scriptName, out float scriptConfidence);

		// TODO: Not getting the correct enum integer
		//[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIOem")]
		//protected static extern UInt32 GetTesseractEngineMode();

        [DllImport(DLL_NAME, EntryPoint = "TessVersion")]
		protected static extern IntPtr GetTesseractVersion();
	}
	
} // namespace PixelSquare