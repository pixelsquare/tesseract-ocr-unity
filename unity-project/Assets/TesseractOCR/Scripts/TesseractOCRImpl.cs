//------------------------------------------------------------------------------
// <copyright file="TesseractOCRImpl.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary>Implementation of Tesseract OCR library to C#.</summary>
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PixelSquare.TesseractOCR
{
    using Enums;
    using Utility;

    /// <summary>
    /// Implementation of Tesseract OCR library to C#.
    /// 
    /// Tessdata:
    /// It uses StreamingAssets path to preserve the state of tess data
    /// and loads it to persistent memory.
    /// 
    /// </summary>
    /// <remarks>Implementation of Tesseract OCR library to C#.</remarks>
    public class TesseractOCRImpl : IOpticalCharacterReader
    {
        /// <summary>
        /// Corresponds to RGB
        /// </summary>
        private const int BYTES_PER_PIXEL = 3;

        /// <summary>
        /// Tess data directory name
        /// </summary>
        private const string TESS_DATA_DIR_NAME = "tessdata";

        /// <summary>
        /// Tesseract config directory name
        /// </summary>
        private const string TESSERACT_CONFIG_DIR_NAME = "configs";

        /// <summary>
        /// Tesseract config file name
        /// </summary>
        private const string TESSERACT_CONFIG_FILE_NAME = "custom_config";

        /// <summary>
        /// Tess data extension
        /// </summary>
        private const string TESS_DATA_EXT = ".traineddata";

        /// <summary>
        /// Native pointer reference
        /// </summary>
        private IntPtr m_Handle = IntPtr.Zero;

        /// <summary>
        /// Currenlty available data
        /// </summary>
        private HashSet<string> m_AvailableDataSet = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);


        /// <summary>
        /// Used to initialize Tesseract OCR library
        /// </summary>
        /// <param name="languageId">Language ID</param>
        public void Initialize(string languageId)
        {
            if(m_Handle == IntPtr.Zero)
            {
                m_Handle = TesseractOCRBridge.CreateTesseractHandle();
            }

            CoroutineRunner.RunCoroutine(InitializeRoutine(languageId));
        }

        /// <summary>
        /// Sets the image to be used
        /// </summary>
        /// <param name="image">Image</param>
        public void SetImage(Image image)
        {
            Debug.Assert(image, "Image must not be nulled!");

            if(image != null)
            {
                SetImage(image.sprite);
            }
        }

        /// <summary>
        /// Sets the image to be used
        /// </summary>
        /// <param name="image">Raw Image</param>
        public void SetImage(RawImage image)
        {
            Debug.Assert(image, "Image must not be nulled!");

            if(image != null)
            {
                SetImage(image.mainTexture);
            }
        }

        /// <summary>
        /// Sets the image to be used
        /// </summary>
        /// <param name="sprite">Sprite</param>
        public void SetImage(Sprite sprite)
        {
            Debug.Assert(sprite, "Sprite must not be nulled!");

            if(sprite != null)
            {
                SetImage(sprite.texture);
            }
        }

        /// <summary>
        /// Sets the image to be used
        /// </summary>
        /// <param name="texture">Texture</param>
        public void SetImage(Texture texture)
        {
            Debug.Assert(texture, "Texture must not be nulled!");

            if(texture != null)
            {
                SetImage((Texture2D)texture);
            }
        }

        /// <summary>
        /// Sets the image to be used
        /// </summary>
        /// <param name="texture">Texture2D</param>
        public void SetImage(Texture2D texture)
        {
            Debug.Assert(texture, "Texture must not be nulled!");

            if(texture != null)
            {
                SetImage(texture.GetRawTextureData(), texture.width, texture.height);
            }
        }

        /// <summary>
        /// Sets the image to be used
        /// </summary>
        /// <param name="imageData">Image data buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        public void SetImage(byte[] imageData, int width, int height)
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                Color32[] colorBuffer = TesseractOCRUtility.ImageFlipVertical(imageData, width, height);
                imageData = TesseractOCRUtility.Color32ToBytes(colorBuffer);
                TesseractOCRBridge.SetImageData(m_Handle, imageData, width, height, BYTES_PER_PIXEL, BYTES_PER_PIXEL * width);
            }
        }

        /// <summary>
        /// Sets the configuration file
        /// </summary>
        /// <param name="filename">Config Filename</param>
        /// <param name="isDebug">Is Debug</param>
        public void SetConfigurationFile(string filename, bool isDebug = false)
        {
            if(m_Handle != IntPtr.Zero)
            {
                if(!isDebug)
                {
                    TesseractOCRBridge.SetConfigurationFile(m_Handle, filename);
                }
                else
                {
                    TesseractOCRBridge.SetDebugConfigurationFile(m_Handle, filename);
                }
            }
        }

        /// <summary>
        /// Gets the text from the image in UTF-8 format
        /// </summary>
        /// <returns>UTF-8 Text</returns>
        public string GetText()
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                IntPtr textPtr = TesseractOCRBridge.GetTextData(m_Handle);
                Debug.Assert(textPtr != IntPtr.Zero, "Text must not be nulled!");

                if(textPtr != IntPtr.Zero)
                {
                    return Marshal.PtrToStringAnsi(textPtr);
                }
            }

            return "";
        }

        /// <summary>
        /// Gets the language id used by the library
        /// </summary>
        /// <returns>Language ID</returns>
        public string GetLanguageId()
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                IntPtr languagePtr = TesseractOCRBridge.GetTesseractLanguage(m_Handle);
                Debug.Assert(languagePtr != IntPtr.Zero, "Datapath must not be nulled!");

                if(languagePtr != IntPtr.Zero)
                {
                    return Marshal.PtrToStringAnsi(languagePtr);
                }
            }

            return "";
        }

        /// <summary>
        /// Gets the datapath where the tessdata is stored
        /// </summary>
        /// <returns>Tess data directory path</returns>
        public string GetDataPath()
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                IntPtr dataPathPtr = TesseractOCRBridge.GetTesseractDataPath(m_Handle);
                Debug.Assert(dataPathPtr != IntPtr.Zero, "Datapath must not be nulled!");

                if(dataPathPtr != IntPtr.Zero)
                {
                    return Marshal.PtrToStringAnsi(dataPathPtr);
                }
            }

            return "";
        }

        /// <summary>
        /// Gets the version of Tesseract OCR library
        /// </summary>
        /// <returns>TesseractOCR Version</returns>
        public string GetVersion()
        {
            IntPtr versionPtr = TesseractOCRBridge.GetTesseractVersion();
            Debug.Assert(versionPtr != IntPtr.Zero, "Version must not be nulled");

            if(versionPtr != IntPtr.Zero)
            {
                return Marshal.PtrToStringAnsi(versionPtr);
            }

            return "";
        }

        /// <summary>
        /// Library cleanup
        /// </summary>
        public void Dispose()
        {
            if(m_Handle != IntPtr.Zero)
            {
                TesseractOCRBridge.EndTesseractHandle(m_Handle);
                TesseractOCRBridge.DeleteTesseractHandle(m_Handle);
                m_Handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Used to initialize the routine asynchronously
        /// </summary>
        /// <param name="languageId">Language ID</param>
        /// <returns>IEnumerator</returns>
        private IEnumerator InitializeRoutine(string languageId)
        {
            // Copies all tess data to persistent directory
            yield return CoroutineRunner.RunCoroutine(InitializeTessdataRoutine());

            string[] languageSplit = languageId.Split('+');

            // Check whether we have stored the correct tesseract language data
            for(int i = 0; i < languageSplit.Length; i++)
            {
                if(!m_AvailableDataSet.Contains(languageSplit[i]))
                {
                    Debug.LogWarning(string.Format("Tesseract language id is not available. Please check your settings for the correct data import. [{0}]", languageSplit[i]));
                }
            }

            if(m_Handle != IntPtr.Zero)
            {
                // Perform initialization from the native library
                if(TesseractOCRBridge.Initialize(m_Handle, GetTessdataPersistentPath(), languageId) == 0)
                {
                    // Copies the configuration file to persistent directory and sets the config file to use
                    yield return CoroutineRunner.RunCoroutine(InitializeConfigFileRoutine(true));

                    Debug.Log("Initialize TesseractOCR Success!");
                }
                else
                {
                    Debug.Log("Initialize TesseractOCR Failed!");
                }
            }
        }

        /// <summary>
        /// Initializes tess data and its directory
        /// </summary>
        /// <returns>IEnumerator</returns>
        private IEnumerator InitializeTessdataRoutine()
        {
            string persistentDataPath = GetTessdataPersistentPath();

            if(Directory.Exists(persistentDataPath))
            {
                string[] tessdataFiles = Directory.GetFiles(persistentDataPath, "*" + TESS_DATA_EXT, SearchOption.TopDirectoryOnly);

                // Fetch available stored tesseract language data from persistent datapath
                for(int i = 0; i < tessdataFiles.Length; i++)
                {
                    m_AvailableDataSet.Add(Path.GetFileNameWithoutExtension(tessdataFiles[i]));
                }
            }

            HashSet<string> selectedTessdataSet = new HashSet<string>();
            TesseractOCRSettings.TessDataInfo[] tessdataInfoList = TesseractOCRSettings.Instance.GetSelectedTessdata();

            // Get all required tesseract language data
            for(int i = 0; i < tessdataInfoList.Length; i++)
            {
                string filename = Path.GetFileNameWithoutExtension(tessdataInfoList[i].name);
                yield return CoroutineRunner.RunCoroutine(LoadTesseractDataRoutine(filename, true));
                m_AvailableDataSet.Add(filename);
                selectedTessdataSet.Add(filename);
            }

            // Exclude unused tesseract language data
            HashSet<string> removedTessdataSet = new HashSet<string>(m_AvailableDataSet);
            removedTessdataSet.ExceptWith(selectedTessdataSet);

            // Remove unused tesseract language data to reduce application size
            foreach(string removedTessdata in removedTessdataSet)
            {
                m_AvailableDataSet.Remove(removedTessdata);
                string fullFilePath = Path.Combine(persistentDataPath, removedTessdata + TESS_DATA_EXT);
                File.Delete(fullFilePath);
            }
        }

        /// <summary>
        /// Initializes custom user-defined configuration file.
        /// </summary>
        /// <param name="overwrite">Overwrite</param>
        /// <returns>IEnumerator</returns>
        private IEnumerator InitializeConfigFileRoutine(bool overwrite = false)
        {
            string streamingDataPath = Path.Combine(GetTessdataStreamingPath(), TESSERACT_CONFIG_DIR_NAME);
            string persistentDataPath = Path.Combine(GetTessdataPersistentPath(), TESSERACT_CONFIG_DIR_NAME);

            if(!Directory.Exists(persistentDataPath))
            {
                Directory.CreateDirectory(persistentDataPath);
            }

            if(streamingDataPath.Contains("://") || streamingDataPath.Contains(":///"))
            {
                string streamingDataUri = Path.Combine(streamingDataPath, TESSERACT_CONFIG_FILE_NAME);

                using(UnityWebRequest www = UnityWebRequest.Get(streamingDataUri))
                {
                    yield return www.SendWebRequest();

                    if(www.isDone && www.result == UnityWebRequest.Result.Success)
                    {
                        string fullFilePath = Path.Combine(persistentDataPath, TESSERACT_CONFIG_FILE_NAME);
                        File.WriteAllText(fullFilePath, www.downloadHandler.text);
                    }
                }
            }
            else
            {
                if(Directory.Exists(streamingDataPath))
                {
                    string srcFilePath = Path.Combine(streamingDataPath, TESSERACT_CONFIG_FILE_NAME);
                    string destFilePath = Path.Combine(persistentDataPath, TESSERACT_CONFIG_FILE_NAME);

                    if(!File.Exists(destFilePath) || overwrite)
                    {
                        File.Copy(srcFilePath, destFilePath, overwrite);
                    }
                }
            }

            string configFilePath = Path.Combine(persistentDataPath, TESSERACT_CONFIG_FILE_NAME);

            if(File.Exists(configFilePath))
            {
                SetConfigurationFile(TESSERACT_CONFIG_FILE_NAME);
            }
        }

        /// <summary>
        /// Loads a single tesseract data to persistent directory
        /// </summary>
        /// <param name="dataName">Tesseract data name</param>
        /// <param name="overwrite">Should overwrite existing data</param>
        /// <returns>IEnumerator</returns>
        private IEnumerator LoadTesseractDataRoutine(string dataName, bool overwrite = false)
        {
            string sanitizedFilename = Path.GetFileNameWithoutExtension(dataName);

            string streamingDataPath = GetTessdataStreamingPath();
            string persistentDataPath = GetTessdataPersistentPath();

            if(!Directory.Exists(persistentDataPath))
            {
                Directory.CreateDirectory(persistentDataPath);
            }

            // Check whether we're on mobile phones
            if(streamingDataPath.Contains("://") || streamingDataPath.Contains(":///"))
            {
                string streamingDataUri = Path.Combine(streamingDataPath, sanitizedFilename + TESS_DATA_EXT);

                using(UnityWebRequest www = UnityWebRequest.Get(streamingDataUri))
                {
                    yield return www.SendWebRequest();

                    if(www.isDone && www.result == UnityWebRequest.Result.Success)
                    {
                        string fullFilePath = Path.Combine(persistentDataPath, sanitizedFilename + TESS_DATA_EXT);
                        File.WriteAllBytes(fullFilePath, www.downloadHandler.data);
                    }
                    
                    if(www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.Log(www.error);
                    }
                }
            }
            else
            {
                if(Directory.Exists(streamingDataPath) && !m_AvailableDataSet.Contains(sanitizedFilename))
                {
                    string srcFilePath = Path.Combine(streamingDataPath, sanitizedFilename + TESS_DATA_EXT);
                    string destFilePath = Path.Combine(persistentDataPath, sanitizedFilename + TESS_DATA_EXT);

                    if(!File.Exists(destFilePath) || overwrite)
                    {
                        File.Copy(srcFilePath, destFilePath, overwrite);
                    }
                }
            }

        }

        /// <summary>
        /// Recognizes the image if the library can parse it
        /// Monitor handle can be nulled
        /// </summary>
        /// <returns>0 = success, -1 = failed</returns>
        private int Recognize()
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                return TesseractOCRBridge.Recognize(m_Handle, IntPtr.Zero);
            }

            return -1;
        }

        /// <summary>
        /// Sets the page segmentation mode.
        /// </summary>
        /// <param name="mode">Segmentation Mode</param>

        private void SetPageSegmentationMode(SegmentationMode mode)
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                TesseractOCRBridge.SetPageSegmentationMode(m_Handle, mode);
            }
        }

        /// <summary>
        /// Gets the page segmentation mode
        /// </summary>
        /// <returns>Segmentation Mode</returns>
        private SegmentationMode GetPageSegmentationMode()
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                return TesseractOCRBridge.GetPageSegmentationMode(m_Handle);
            }

            return SegmentationMode.SINGLE_BLOCK;
        }

        /// <summary>
        /// Gets the OCR Engine Mode
        /// </summary>
        /// <returns>OCR Engine Mode</returns>
        private OCREngineMode GetOCREngineMode()
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                return TesseractOCRBridge.GetTesseractEngineMode(m_Handle);
            }

            return OCREngineMode.DEFAULT;
        }

        /// <summary>
        /// Sets a configuration variable
        /// </summary>
        /// <param name="name">Parameter Name</param>
        /// <param name="value">Parameter Value</param>
        /// <param name="isDebug">Is Debug</param>
        private void SetConfigVariable(string name, string value, bool isDebug = false)
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                if(!isDebug)
                {
                    TesseractOCRBridge.SetVariable(m_Handle, name, value);
                }
                else
                {
                    TesseractOCRBridge.SetDebugVariable(m_Handle, name, value);
                }
            }
        }

        /// <summary>
        /// Gets a type string configuration variable
        /// </summary>
        /// <param name="name">Parameter Name</param>
        /// <returns>String Result</returns>
        private string GetConfigStringVariable(string name)
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                IntPtr stringPtr = TesseractOCRBridge.GetVariable(m_Handle, name);
                Debug.Assert(stringPtr != IntPtr.Zero, "Handle must not be nulled!");

                if(stringPtr != IntPtr.Zero)
                {
                    return Marshal.PtrToStringAnsi(stringPtr);
                }
            }

            return "";
        }

        /// <summary>
        /// Gets a type integer configuration variable
        /// </summary>
        /// <param name="name">Parameter Name</param>
        /// <returns>Integer Result</returns>
        private int GetConfigIntVariable(string name)
        {
            int result = 0;

            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                if(TesseractOCRBridge.GetVariable(m_Handle, name, out result))
                {
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a type boolean configuration variable
        /// </summary>
        /// <param name="name">Parameter Name</param>
        /// <returns>Integer Result</returns>
        private bool GetConfigBoolVariable(string name)
        {
            bool result = false;

            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                if(TesseractOCRBridge.GetVariable(m_Handle, name, out result))
                {
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a type double configuration variable
        /// </summary>
        /// <param name="name">Parameter Name</param>
        /// <returns>Double Result</returns>
        private double GetConfigDoubleVariable(string name)
        {
            double result = 0;

            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                if(TesseractOCRBridge.GetVariable(m_Handle, name, out result))
                {
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Clears tesseract engine's persistent cache
        /// </summary>
        private void ClearPersistentCache()
        {
            Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            if(m_Handle != IntPtr.Zero)
            {
                TesseractOCRBridge.ClearPersistentCache(m_Handle);
            }
        }

        /// <summary>
        /// Gets tess data's persistent data path
        /// </summary>
        /// <returns>Tess data's persistent data path</returns>
        private string GetTessdataPersistentPath()
        {
            return Path.Combine(Application.persistentDataPath, TESS_DATA_DIR_NAME);
        }

        /// <summary>
        /// Gets tess data's streaming data path
        /// </summary>
        /// <returns>Tess data's streaming data path</returns>
        private string GetTessdataStreamingPath()
        {
            return Path.Combine(Application.streamingAssetsPath, TESS_DATA_DIR_NAME);
        }
    }
    
} // namespace PixelSquare