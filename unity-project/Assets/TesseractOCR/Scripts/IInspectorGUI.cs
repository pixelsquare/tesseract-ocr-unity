//------------------------------------------------------------------------------
// <copyright file="IInspectorGUI.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/10/03</date>
// <summary>Interface implementation for custom inspector editor</summary>
//------------------------------------------------------------------------------

using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace PixelSquare.TesseractOCR
{
    /// <summary>
    /// Interface implementation for custom inspector editor
    /// </summary>
    /// <remarks></remarks>
    public interface IInspectorGUI
    {
#if UNITY_EDITOR
        /// <summary>
        /// Executes when the asset was selected on the editor
        /// </summary>
        public void OnInspectorInit();

        /// <summary>
        /// Draws custom inspector for the asset
        /// </summary>
        public void OnInspectorGUI();
#endif
    }

} // namespace PixelSquare