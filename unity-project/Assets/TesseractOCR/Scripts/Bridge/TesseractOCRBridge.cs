//------------------------------------------------------------------------------
// <copyright file="TesseractOCRBridge.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary>A bridge that connects the native library (unmanaged code) to C# (managed code)</summary>
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
    public static partial class TesseractOCRBridge 
    {
        /// <summary>
        /// Library Name
        /// </summary>
        private const string DLL_NAME = "tesseract50";

        #region Tesseract Handle

        /// <summary>
        /// Creates a handle that points to the native class instance
        /// </summary>
        /// <returns>Pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPICreate")]
        public static extern IntPtr CreateTesseractHandle();

        /// <summary>
        /// Deletes the handle pointer
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIDelete")]
        public static extern void DeleteTesseractHandle(IntPtr tesseractHandle);

        /// <summary>
        /// Clears the handle pointer
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIClear")]
        public static extern void ClearTesseractHandle(IntPtr tesseractHandle);

        /// <summary>
        /// Ends the handle pointer
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIEnd")]
        public static extern void EndTesseractHandle(IntPtr tesseractHandle);

        /// <summary>
        /// Clears the persitent cache
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIClearPersistentCache")]
        public static extern void ClearPersistentCache(IntPtr tesseractHandle);

        #endregion

        /// <summary>
        /// Initializes Tesseract OCR's library
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="datapath">Tess data directory</param>
        /// <param name="language">Language ID</param>
        /// <returns>0 = Success, -1 = Failed</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIInit3")]
        public static extern int Initialize(IntPtr tesseractHandle, string datapath, string language);

        /// <summary>
        /// Initializes Tesseract OCR's library
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="datapath">Tess data directory</param>
        /// <param name="language">Language ID</param>
        /// <param name="oem">OCR's Engine Mode</param>
        /// <returns>0 = Success, -1 = Failed</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIInit2")]
        public static extern int Initialize(IntPtr tesseractHandle, string datapath, string language, int oem);

        /// <summary>
        /// Sets the image data to be used
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="imageData">Image data buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Image's BPP</param>
        /// <param name="bytesPerLine">Image's BPL</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPISetImage")]
        public static extern void SetImageData(IntPtr tesseractHandle, byte[] imageData, int width, int height, int bytesPerPixel, int bytesPerLine);

        /// <summary>
        /// Gets the text from the image in UTF-8 format
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <returns>Text Pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetUTF8Text")]
        public static extern IntPtr GetTextData(IntPtr tesseractHandle);

        /// <summary>
        /// Recognizes the image if the library can parse it
        /// Monitor handle can be nulled
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="monitorHandle">Monitor Handle Pointer</param>
        /// <returns>0 = success, -1 = failed</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIRecognize")]
        public static extern int Recognize(IntPtr tesseractHandle, IntPtr monitorHandle);

        /// <summary>
        /// Sets the page segmentation mode.
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="mode">Segmentation Mode</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPISetPageSegMode")]
        public static extern void SetPageSegmentationMode(IntPtr tesseractHandle, SegmentationMode mode);

        /// <summary>
        /// Gets the page segmentation mode
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <returns>Segmentation Mode</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetPageSegMode")]
        public static extern SegmentationMode GetPageSegmentationMode(IntPtr tesseractHandle);

        /// <summary>
        /// Gets the OCR Engine Mode
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <returns>OCR Engine Mode</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIOem")]
        public static extern OCREngineMode GetTesseractEngineMode(IntPtr tesseractHandle);

        /// <summary>
        /// Gets the language used by tesseract
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <returns>Language string pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetInitLanguagesAsString")]
        public static extern IntPtr GetTesseractLanguage(IntPtr tesseractHandle);

        /// <summary>
        /// Gets the tess data directory
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <returns>Tess data directory string pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetDatapath")]
        public static extern IntPtr GetTesseractDataPath(IntPtr tesseractHandle);

        //// WIP: Might crash sometimes. Still looking for a fix. Only availble on Legacy Engine
        //[DllImport(DLL_NAME, EntryPoint = "TessBaseAPIDetectOrientationScript")]
        //public static extern bool OrientationScript(IntPtr tesseractHandle, out int degrees, out float confidence, out string scriptName, out float scriptConfidence);

        //// WIP: Used to free up string (scriptName) memory allocated by OrientationScript function
        //[DllImport(DLL_NAME, EntryPoint = "TessDeleteText")]
        //public static extern void DeleteText(string text);


        /// <summary>
        /// Gets tesseract's version
        /// </summary>
        /// <returns>Version string pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessVersion")]
        public static extern IntPtr GetTesseractVersion();
    }
    
} // namespace PixelSquare