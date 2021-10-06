//------------------------------------------------------------------------------
// <copyright file="TesseractOCRBridgeConfiguration.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/10/06</date>
// <summary> TODO: Describe the file's implementation overview here </summary>
//------------------------------------------------------------------------------

using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PixelSquare.TesseractOCR
{
    /// <summary>
    /// TODO: Describe the class outline here
    /// </summary>
    /// <remarks> Detailed information about the class. </remarks>
    public static partial class TesseractOCRBridge
    {
        /// <summary>
        /// Sets tesseract's configuration file
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="filename">Filename</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIReadConfigFile")]
        public static extern void SetConfigurationFile(IntPtr tesseractHandle, string filename);

        /// <summary>
        /// Sets tesseract's debug configuration file
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="filename">Filename</param>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIReadDebugConfigFile")]
        public static extern void SetDebugConfigurationFile(IntPtr tesseractHandle, string filename);

        /// <summary>
        /// Sets a configuration variable
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="name">Parameter Name</param>
        /// <param name="value">Value</param>
        /// <returns>If it successfully added the new parameter</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPISetVariable")]
        public static extern bool SetVariable(IntPtr tesseractHandle, string name, string value);

        /// <summary>
        /// Sets a debug configuration variable
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="name">Parameter Name</param>
        /// <param name="value">Value</param>
        /// <returns>If it successfully added the new parameter</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPISetDebugVariable")]
        public static extern bool SetDebugVariable(IntPtr tesseractHandle, string name, string value);

        /// <summary>
        /// Gets a type string configuration variable
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="name">Parameter Name</param>
        /// <returns>String Value</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetStringVariable")]
        public static extern IntPtr GetVariable(IntPtr tesseractHandle, string name);

        /// <summary>
        /// Gets a type integer configuration variable
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="name">Parameter Name</param>
        /// <param name="value">Out Value</param>
        /// <returns>If it found the parameter name</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetIntVariable")]
        public static extern bool GetVariable(IntPtr tesseractHandle, string name, out int value);

        /// <summary>
        /// Gets a type boolean configuration variable
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="name">Parameter Name</param>
        /// <param name="value">Out Value</param>
        /// <returns>If it found the parameter name</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetBoolVariable")]
        public static extern bool GetVariable(IntPtr tesseractHandle, string name, out bool value);

        /// <summary>
        /// Gets a type double configuration variable
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="name">Parameter Name</param>
        /// <param name="value">Out Value</param>
        /// <returns>If it found the parameter name</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIGetDoubleVariable")]
        public static extern bool GetVariable(IntPtr tesseractHandle, string name, out double value);

        /// <summary>
        /// Prints all the variables used by the library
        /// Saves at the project root directory
        /// </summary>
        /// <param name="tesseractHandle">Tesseract Handle Pointer</param>
        /// <param name="filename">Filename</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIPrintVariablesToFile")]
        public static extern bool PrintVariablesToFile(IntPtr tesseractHandle, string filename);
    }

} // namespace PixelSquare