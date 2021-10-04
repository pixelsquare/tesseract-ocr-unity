//------------------------------------------------------------------------------
// <copyright file="TesseractOCRBridge.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary>A bridge to connect the native library (unmanaged code) to C# (managed code)</summary>
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
    /// A bridge to connect the native library (unmanaged code) to C# (managed code)
    /// </summary>
    /// <remarks></remarks>
    public static class TesseractOCRBridge 
    {
        /// <summary>
        /// Library Name
        /// </summary>
        private const string DLL_NAME = "tesseract50";

        /// <summary>
        /// Creates a handle that points to the native class instance
        /// </summary>
        /// <returns>Pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPICreate")]
        public static extern IntPtr CreateHandle();

        /// <summary>
        /// Deletes the handle pointer
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIDelete")]
        public static extern void DeleteHandle(IntPtr handle);

        /// <summary>
        /// Clears the handle pointer
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIClear")]
        public static extern void ClearTesseract(IntPtr handle);

        /// <summary>
        /// Ends the handle pointer
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIEnd")]
        public static extern void EndTesseract(IntPtr handle);

        /// <summary>
        /// Initializes Tesseract OCR's library
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        /// <param name="datapath">Tess data directory</param>
        /// <param name="language">Language ID</param>
        /// <returns>0 = Success, -1 = Failed</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIInit3")]
        public static extern int Initialize(IntPtr handle, string datapath, string language);

        /// <summary>
        /// Initializes Tesseract OCR's library
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        /// <param name="datapath">Tess data directory</param>
        /// <param name="language">Language ID</param>
        /// <param name="oem">OCR's Engine Mode</param>
        /// <returns>0 = Success, -1 = Failed</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIInit2")]
        public static extern int Initialize(IntPtr handle, string datapath, string language, int oem);

        /// <summary>
        /// Sets the image data to be used
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        /// <param name="imageData">Image data buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Image's BPP</param>
        /// <param name="bytesPerLine">Image's BPL</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPISetImage")]
        public static extern void SetImageData(IntPtr handle, byte[] imageData, int width, int height, int bytesPerPixel, int bytesPerLine);

        /// <summary>
        /// Gets the text from the image in UTF-8 format
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        /// <returns>Text Pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetUTF8Text")]
        public static extern IntPtr GetTextData(IntPtr handle);

        /// <summary>
        /// Sets the page segmentation mode.
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        /// <param name="mode">Segmentation Mode</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPISetPageSegMode")]
        public static extern void SetPageSegmentationMode(IntPtr handle, SegmentationMode mode);

        /// <summary>
        /// Gets the page segmentation mode
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        /// <returns>Segmentation Mode</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetPageSegMode")]
        public static extern int GetPageSegmentationMode(IntPtr handle);

        /// <summary>
        /// Gets the tess data directory
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        /// <returns>Tess data directory string pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetDatapath")]
        public static extern IntPtr GetTesseractDataPath(IntPtr handle);

        /// <summary>
        /// Gets the language used by tesseract
        /// </summary>
        /// <param name="handle">Handle Pointer</param>
        /// <returns>Language string pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetInitLanguagesAsString")]
        public static extern IntPtr GetTesseractLanguage(IntPtr handle);

        // TODO: Might crash sometimes. Still looking for a fix
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIDetectOrientationScript")]
        public static extern bool OrientationScript(IntPtr handle, out int degrees, out float confidence, out string scriptName, out float scriptConfidence);

        // TODO: Not getting the correct enum integer
        //[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIOem")]
        //protected static extern UInt32 GetTesseractEngineMode();

        /// <summary>
        /// Gets tesseract's version
        /// </summary>
        /// <returns>Version string pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessVersion")]
        public static extern IntPtr GetTesseractVersion();
    }
    
} // namespace PixelSquare