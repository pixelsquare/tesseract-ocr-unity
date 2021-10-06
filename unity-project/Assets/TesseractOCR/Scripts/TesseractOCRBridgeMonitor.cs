//------------------------------------------------------------------------------
// <copyright file="TesseractOCRBridgeMonitor.cs" company="PixelSquare">
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
        /// Creates a handle for monitor
        /// </summary>
        /// <returns>Monitor Handle Pointer</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessMonitorCreate")]
        public static extern IntPtr CreateMonitorHandle();

        /// <summary>
        /// Deletes the handle for monitor
        /// </summary>
        /// <param name="monitorHandle">Monitor Handle Pointer</param>
        [DllImport(DLL_NAME, EntryPoint = "TessMonitorDelete")]
        public static extern void DeleteMonitorHandle(IntPtr monitorHandle);

        /// <summary>
        /// Gets the progress of the monitor
        /// </summary>
        /// <param name="monitorHandle">Monitor Handle Pointer</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, EntryPoint = "TessMonitorGetProgress")]
        public static extern int GetMonitorProgress(IntPtr monitorHandle);

        /// <summary>
        /// Sets the monitor deadline in milliseconds
        /// </summary>
        /// <param name="monitorHandle">Monitor Handle Pointer</param>
        /// <param name="deadline">Deadline in milliseconds</param>
        [DllImport(DLL_NAME, EntryPoint = "TessMonitorSetDeadlineMSecs")]
        public static extern void SetMonitorDeadlineMSecs(IntPtr monitorHandle, int deadline);
    }

} // namespace PixelSquare