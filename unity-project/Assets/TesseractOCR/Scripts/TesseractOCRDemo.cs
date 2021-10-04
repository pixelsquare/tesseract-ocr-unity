//------------------------------------------------------------------------------
// <copyright file="TesseractOCRDemo.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary> TODO: Describe the file's implementation overview here </summary>
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace PixelSquare
{
    using TesseractOCR;
    using TesseractOCR.Utility;

    /// <summary>
    /// TODO: Describe the class outline here
    /// </summary>
    /// <remarks> Detailed information about the class. </remarks>
    public class TesseractOCRDemo : MonoBehaviour 
    {
        [SerializeField] private string m_LanguageId = "eng+jpn";
        [SerializeField] private RawImage m_Image = null;
        [SerializeField] private Text m_Text = null;

        private IOpticalCharacterReader m_Ocr = null;
        //private Texture2D m_Texture = null;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        //public IEnumerator Start() 
        //{
        //    string url = Path.Combine(Application.streamingAssetsPath, "mixed.png");

        //    if(url.Contains("://") || url.Contains(":///"))
        //    {
        //        using(UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        //        {
        //            yield return www.SendWebRequest();

        //            if(www.result == UnityWebRequest.Result.Success)
        //            {
        //                m_Texture = DownloadHandlerTexture.GetContent(www);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        byte[] bytes = File.ReadAllBytes(url);
        //        m_Texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
        //        m_Texture.filterMode = FilterMode.Point;
        //        m_Texture.LoadImage(bytes);
        //    }

        //    m_Image.texture = m_Texture;

        //    InitializeTesseract();
        //}


        public void Start()
        {
            InitializeTesseract();
        }

        public void OnDisable()
        {
            if(m_Ocr != null)
            {
                m_Ocr.Dispose();
            }
        }

        public void ConvertImageToText()
        {
            //Texture2D texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            //texture.filterMode = FilterMode.Point;
            ////texture.LoadRawTextureData(m_Texture.GetRawTextureData());
            //texture.SetPixels(m_Texture.GetPixels());
            //texture.Apply();
            //Debug.Log(texture.GetRawTextureData().Length);

            m_Ocr.SetImage(m_Image);
            m_Text.text = m_Ocr.GetText();
        }

        public void InitializeTesseract()
        {
            m_Ocr = new TesseractOCRImpl();
            m_Ocr.Initialize(m_LanguageId);
            //Debug.Log(m_Ocr.GetLanguageId());
            Debug.Log(m_Ocr.GetVersion());
        }
    }
    
} // namespace PixelSquare