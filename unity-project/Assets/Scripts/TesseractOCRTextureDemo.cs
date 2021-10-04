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

using System.IO;
using System.Collections;
using System.Collections.Generic;

using TMPro;

namespace PixelSquare
{
    using TesseractOCR;

    /// <summary>
    /// TODO: Describe the class outline here
    /// </summary>
    /// <remarks> Detailed information about the class. </remarks>
    public class TesseractOCRTextureDemo : MonoBehaviour 
    {
        [SerializeField] private string m_LanguageId = "eng+jpn";
        [SerializeField] private RawImage m_Image = null;
        [SerializeField] private InputField m_InputField = null;
        [SerializeField] private TextMeshProUGUI m_VersionText = null;

        private IOpticalCharacterReader m_Ocr = null;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        public void Start()
        {
            InitializeTesseract();
        }

        public void OnDestroy()
        {
            if(m_Ocr != null)
            {
                m_Ocr.Dispose();
            }
        }

        public void ConvertImageToText()
        {
            m_Ocr.SetImage(m_Image);
            m_InputField.text = m_Ocr.GetText();
        }

        public void InitializeTesseract()
        {
            m_Ocr = new TesseractOCRImpl();
            m_Ocr.Initialize(m_LanguageId);
            m_VersionText.text = string.Format("Version: {0}", m_Ocr.GetVersion());
        }
    }
    
} // namespace PixelSquare