//------------------------------------------------------------------------------
// <copyright file="TesseractOCRBridgePageIterator.cs" company="PixelSquare">
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
    using Enums;

    /// <summary>
    /// TODO: Describe the class outline here
    /// </summary>
    /// <remarks> Detailed information about the class. </remarks>
    public static partial class TesseractOCRBridge
    {
        /// <summary>
        /// Creates a page iterator handle
        /// </summary>
        /// <param name="handle">Tesseract Handle Pointer</param>
        /// <returns>Page Iterator Handle</returns>
        [DllImport(DLL_NAME, EntryPoint = "TessBaseAPIAnalyseLayout")]
        public static extern IntPtr CreatePageIteratorHandle(IntPtr tesseractHandle);

        /// <summary>
        /// Deletes the page iterator handle
        /// </summary>
        /// <param name="pageIteratorHandle">Page Iterator Handle Pointer</param>
        [DllImport(DLL_NAME, EntryPoint = "TessPageIteratorDelete")]
        public static extern void DeletePageIteratorHandle(IntPtr pageIteratorHandle);

        /// <summary>
        /// Gets the page orientation, direction, order and skew.
        /// </summary>
        /// <param name="pageIteratorHandle">Page Iterator Handle</param>
        /// <param name="orientation">Page Orientation</param>
        /// <param name="direction">Page Direction</param>
        /// <param name="order">Page Textline Order</param>
        /// <param name="skew">Page Skew</param>
        [DllImport(DLL_NAME, EntryPoint = "TessPageIteratorOrientation")]
        public static extern void GetPageOrientation(IntPtr pageIteratorHandle, out Orientation orientation, out WritingDirection direction, out TextlineOrder order, out float skew);

        /// <summary>
        /// Gets the page bounding box
        /// </summary>
        /// <param name="pageIteratorHandle">Page Iterator Handle</param>
        /// <param name="level">Page Iterator Level</param>
        /// <param name="left">Box Left</param>
        /// <param name="top">Box Top</param>
        /// <param name="right">Box Right</param>
        /// <param name="bottom">Box Bottom</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, EntryPoint = "TessPageIteratorBoundingBox")]
        public static extern bool GetPageBoundingBox(IntPtr pageIteratorHandle, PageIteratorLevel level, out int left, out int top, out int right, out int bottom);

    }

} // namespace PixelSquare