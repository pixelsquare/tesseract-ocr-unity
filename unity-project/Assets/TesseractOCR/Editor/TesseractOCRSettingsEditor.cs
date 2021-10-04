//------------------------------------------------------------------------------
// <copyright file="TesseractOCRSettingsEditor.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/10/03</date>
// <summary>Custom editor implementation for Tesseract OCR Settings Class</summary>
//------------------------------------------------------------------------------

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

namespace PixelSquare.TesseractOCR.Editor
{
    /// <summary>
    /// Custom editor implementation for Tesseract OCR Settings Class
    /// </summary>
    /// <remarks></remarks>
    [CustomEditor(typeof(TesseractOCRSettings))]
    public class TesseractOCRSettingsEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Inspector GUI Interface
        /// </summary>
        private IInspectorGUI m_InspectorGUI = null;

        /// <summary>
        /// Executes when the asset was selected on the editor
        /// </summary>
        public void OnEnable()
        {
            m_InspectorGUI = (IInspectorGUI)target;
            m_InspectorGUI.OnInspectorInit();
        }

        /// <summary>
        /// Draws custom inspector for the asset
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Script");
            EditorGUILayout.ObjectField(MonoScript.FromScriptableObject((TesseractOCRSettings)target), typeof(TesseractOCRSettings), false);
            EditorGUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.BeginVertical();
            m_InspectorGUI.OnInspectorGUI();
            EditorGUILayout.EndVertical();
        }
    }

} // namespace PixelSquare.Editor

#endif