//------------------------------------------------------------------------------
// <copyright file="TesseractOCRSettings.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/10/03</date>
// <summary>Tesseract OCR settings asset.</summary>
//------------------------------------------------------------------------------

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace PixelSquare.TesseractOCR
{
    using Utility;

    /// <summary>
    /// Tesseract OCR settings asset.
    /// This class contains all the needed settings for tesseract OCR.
    /// </summary>
    /// <remarks></remarks>
    public class TesseractOCRSettings : ScriptableObject, IInspectorGUI
    {
        /// <summary>
        /// Tess data information
        /// </summary>
        [System.Serializable]
        public struct TessDataInfo
        {
            public bool selected;
            public string name;
            public string path;
            public long fileSize;
            public string fileSizeString;
        }

        /// <summary>
        /// Tesseract's resources directory
        /// </summary>
        private const string ASSET_RESOURCES_PATH = "TesseractOCR/Resources";

        /// <summary>
        /// Tesseract's streaming assets directory
        /// </summary>
        private const string TESSDATA_STREAMING_PATH = "StreamingAssets/tessdata";

        /// <summary>
        /// Extension for this asset
        /// </summary>
        private const string ASSET_EXTENSION = ".asset";

        /// <summary>
        /// Singleton instance for this class
        /// </summary>
        private static TesseractOCRSettings s_Instance = null;

        /// <summary>
        /// Singleton property for this class
        /// </summary>
        public static TesseractOCRSettings Instance
        {
            get
            {
                Type instanceType = typeof(TesseractOCRSettings);
                string instanceName = instanceType.Name;

                s_Instance = Resources.Load<TesseractOCRSettings>(instanceName);

#if UNITY_EDITOR
                if(s_Instance == null)
                {
                    s_Instance = ScriptableObject.CreateInstance<TesseractOCRSettings>();

                    string resourcesPath = Path.Combine(Application.dataPath, ASSET_RESOURCES_PATH);
                    string relativePath = Path.Combine("Assets", ASSET_RESOURCES_PATH);
                    string fullAssetPath = Path.Combine(relativePath, instanceName);
                    fullAssetPath = Path.ChangeExtension(fullAssetPath, ASSET_EXTENSION);

                    if(!Directory.Exists(resourcesPath))
                    {
                        Directory.CreateDirectory(resourcesPath);
                    }

                    AssetDatabase.CreateAsset(s_Instance, fullAssetPath);
                    AssetDatabase.Refresh();
                }
#endif

                return s_Instance;
            }
        }

        /// <summary>
        /// List of tess data 
        /// </summary>
        [SerializeField] private TessDataInfo[] m_TessdataList = new TessDataInfo[0];

#if UNITY_EDITOR
        [SerializeField] private string m_TessdataDirectory = null;

        private int m_TotalSelectedTessdata = 0;
        private long m_TotalTessdataSize = 0;
        private bool m_IsAllDataSelected = false;

        private Vector2 m_TessdataListScrollView = new Vector2();
#endif

        /// <summary>
        /// Execute once when the asset is newly created
        /// </summary>
        public void Awake()
        {
#if UNITY_EDITOR
            m_TessdataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "tessdata_fast");
            SetTesseractDataDirty();
#endif
        }

        /// <summary>
        /// Execute everytime the asset is selected in the editor
        /// </summary>
        public void OnEnable()
        {
#if UNITY_EDITOR
            m_TotalTessdataSize = GetTotalTessdataSize();
            m_TotalSelectedTessdata = GetSelectedTessdata().Length;
#endif
        }

        /// <summary>
        /// Gets all selected tess data
        /// </summary>
        /// <returns></returns>
        public TessDataInfo[] GetSelectedTessdata()
        {
            return Array.FindAll<TessDataInfo>(m_TessdataList, a => a.selected);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Executes when the asset is selected in the editor
        /// </summary>
        public void OnInspectorInit()
        {
        }

        /// <summary>
        /// Draws the GUI for the editor
        /// </summary>
        public void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Tessdata Directory");
            m_TessdataDirectory = EditorGUILayout.TextField(m_TessdataDirectory);

            if(GUILayout.Button("..."))
            {
                m_TessdataDirectory = EditorUtility.OpenFolderPanel("Select Tessdata Directory", Path.Combine(Directory.GetCurrentDirectory(), ".."), "");
                SetTesseractDataDirty();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Please select trained data to import.");

            GUILayout.FlexibleSpace();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal(GUILayout.Width(60.0f));
            EditorGUILayout.LabelField("Select All", GUILayout.Width(60.0f));
            m_IsAllDataSelected = EditorGUILayout.Toggle(m_IsAllDataSelected, GUILayout.Width(20.0f));
            EditorGUILayout.EndHorizontal();

            if(EditorGUI.EndChangeCheck())
            {
                AssetDatabase.StartAssetEditing();

                for(int i = 0; i < m_TessdataList.Length; i++)
                {
                    m_TessdataList[i].selected = m_IsAllDataSelected;

                    if(m_IsAllDataSelected)
                    {
                        ImportTessdata(m_TessdataList[i].path);
                        EditorUtility.DisplayProgressBar("Importing All Tessdata", string.Format("Importing {0} ({1}/{2})", m_TessdataList[i].name, i, m_TessdataList.Length), (float)i / (float)m_TessdataList.Length);
                    }
                    else
                    {
                        RemoveTessdata(m_TessdataList[i].path);
                        EditorUtility.DisplayProgressBar("Removing All Tessdata", string.Format("Removing {0} ({1}/{2})", m_TessdataList[i].name, i, m_TessdataList.Length), (float)i / (float)m_TessdataList.Length);
                    }
                }

                AssetDatabase.StopAssetEditing();

                AssetDatabase.Refresh();
                m_TotalTessdataSize = GetTotalTessdataSize();
                m_TotalSelectedTessdata = GetSelectedTessdata().Length;
            }

            EditorGUILayout.EndHorizontal();

            if(m_TessdataList.Length > 0)
            {
                EditorGUILayout.BeginVertical("Box");
                m_TessdataListScrollView = EditorGUILayout.BeginScrollView(m_TessdataListScrollView, GUILayout.Height(200.0f));

                for(int i = 0; i < m_TessdataList.Length; i++)
                {
                    EditorGUI.BeginChangeCheck();

                    EditorGUILayout.BeginHorizontal();
                    m_TessdataList[i].selected = EditorGUILayout.ToggleLeft(m_TessdataList[i].name + " - " + m_TessdataList[i].fileSizeString, m_TessdataList[i].selected);
                    EditorGUILayout.EndHorizontal();

                    if(EditorGUI.EndChangeCheck())
                    {
                        if(m_TessdataList[i].selected)
                        {
                            ImportTessdata(m_TessdataList[i].path);
                        }
                        else
                        {
                            RemoveTessdata(m_TessdataList[i].path);
                        }

                        AssetDatabase.Refresh();
                        m_TotalTessdataSize = GetTotalTessdataSize();
                        m_TotalSelectedTessdata = GetSelectedTessdata().Length;
                    }
                }

                EditorUtility.ClearProgressBar();

                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();

                EditorGUILayout.LabelField("Selected Count: " + m_TotalSelectedTessdata);
                EditorGUILayout.LabelField("Total Tessdata Size: " + TesseractOCRUtility.FormatBytes(m_TotalTessdataSize));
            }
            else
            {
                EditorGUILayout.HelpBox("Unable to find tessdata from the directory. \nThe project will not work properly.", MessageType.Error, true);

                EditorGUILayout.LabelField("Please visit Tessdata Github and clone your desired repository.");
                if(GUILayout.Button("Tessdata Github"))
                {
                    Help.BrowseURL("https://github.com/tesseract-ocr?q=tessdata");
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Imports tess data to streaming assets path
        /// </summary>
        /// <param name="assetDataPath">Source Asset Data Path</param>
        private void ImportTessdata(string assetDataPath)
        {
            string filename = Path.GetFileName(assetDataPath);
            string streamingAssetsPath = Path.Combine(Application.dataPath, TESSDATA_STREAMING_PATH);
            string fullAssetPath = Path.Combine(streamingAssetsPath, filename);

            if(!Directory.Exists(streamingAssetsPath))
            {
                Directory.CreateDirectory(streamingAssetsPath);
            }

            if(!File.Exists(fullAssetPath))
            {
                File.Copy(assetDataPath, fullAssetPath);
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// Removes tess data to streaming assets path
        /// </summary>
        /// <param name="assetDataPath">Source Asset Data Path</param>
        private void RemoveTessdata(string assetDataPath)
        {
            string filename = Path.GetFileName(assetDataPath);
            string streamingAssetsPath = Path.Combine(Application.dataPath, TESSDATA_STREAMING_PATH);
            string fullAssetPath = Path.Combine(streamingAssetsPath, filename);

            if(!Directory.Exists(streamingAssetsPath))
            {
                Directory.CreateDirectory(streamingAssetsPath);
            }

            if(File.Exists(fullAssetPath))
            {
                File.Delete(fullAssetPath);
                File.Delete(fullAssetPath + ".meta");
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// Refreshes tesseract data gui
        /// </summary>
        private void SetTesseractDataDirty()
        {
            if(Directory.Exists(m_TessdataDirectory))
            {
                string[] trainedDataFiles = Directory.GetFiles(m_TessdataDirectory, "*.traineddata", SearchOption.TopDirectoryOnly);

                string streamingAssetsPath = Path.Combine(Application.dataPath, TESSDATA_STREAMING_PATH);
                string[] tessdataFiles = Directory.GetFiles(streamingAssetsPath, "*.traineddata", SearchOption.TopDirectoryOnly);

                int trainedDataLen = trainedDataFiles.Length;
                m_TessdataList = new TessDataInfo[trainedDataLen];

                for(int i = 0; i < trainedDataLen; i++)
                {
                    FileInfo fileInfo = new FileInfo(trainedDataFiles[i]);

                    m_TessdataList[i] = new TessDataInfo()
                    {
                        selected = Array.Exists<string>(tessdataFiles, a => a.Contains(Path.GetFileName(trainedDataFiles[i]))),
                        name = Path.GetFileName(trainedDataFiles[i]),
                        path = trainedDataFiles[i],
                        fileSize = fileInfo.Length,
                        fileSizeString = TesseractOCRUtility.FormatBytes(fileInfo.Length)
                    };
                }
            }

            m_TotalTessdataSize = GetTotalTessdataSize();
            m_TotalSelectedTessdata = Array.FindAll<TessDataInfo>(m_TessdataList, a => a.selected).Length;
        }

        /// <summary>
        /// Gets total tess data size
        /// </summary>
        /// <returns>Total Tessdata Size</returns>
        private long GetTotalTessdataSize()
        {
            long result = 0; 
            string streamingAssetsPath = Path.Combine(Application.dataPath, TESSDATA_STREAMING_PATH);
            string[] tessdataFiles = Directory.GetFiles(streamingAssetsPath, "*.traineddata", SearchOption.TopDirectoryOnly);

            for(int i = 0; i < tessdataFiles.Length; i++)
            {
                result += new FileInfo(tessdataFiles[i]).Length;
            }

            return result;
        }

        /// <summary>
        /// Context menu for this settings
        /// </summary>
        [MenuItem("TesseractOCR/Settings")]
        private static void InitializeEditorWindow()
        {
            Selection.activeObject = Instance;
            EditorGUIUtility.PingObject(Instance);
        }
#endif
    }

} // namespace PixelSquare