//------------------------------------------------------------------------------
// <copyright file="TesseractOCRCameraDemo.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/10/04</date>
// <summary>Tesseract OCR Camera Demonstration</summary>
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

using TMPro;

namespace PixelSquare
{
    using TesseractOCR;
    using TesseractOCR.Utility;

    /// <summary>
    /// Tesseract OCR Camera Demonstration
    /// </summary>
    /// <remarks></remarks>
    public class TesseractOCRCameraDemo : MonoBehaviour
    {
        /// <summary>
        /// Language id
        /// </summary>
        [SerializeField] private string m_LanguageId = "eng+jpn";

        /// <summary>
        /// Sample image to extract text from
        /// </summary>
        [SerializeField] private RawImage m_SampleRawImage = null;

        /// <summary>
        /// Input field where we can edit the extracted text
        /// </summary>
        [SerializeField] private InputField m_InputField = null;

        /// <summary>
        /// Version Text
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_VersionText = null;

        private IOpticalCharacterReader m_Ocr = null;
        private WebCamTexture m_WebCamTexture = null;
        private Texture2D m_SnapshotTexture = null;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        public IEnumerator Start()
        {
            WebCamDevice[] devices = WebCamTexture.devices;

            for(int i = 0; i < devices.Length; i++)
            {
                Debug.Log(string.Format("Webcam Name: {0}", devices[i].name));

                yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

                if(Application.HasUserAuthorization(UserAuthorization.WebCam))
                {
                    Debug.Log(string.Format("User granted webcam access. [{0}]", devices[i].name));
                }
            }

            m_WebCamTexture = new WebCamTexture();
            m_SampleRawImage.texture = m_WebCamTexture;
            m_WebCamTexture.Play();

            m_SampleRawImage.transform.rotation *= Quaternion.AngleAxis(m_WebCamTexture.videoRotationAngle, -Vector3.forward);

            InitializeTesseract();
        }

        /// <summary>
        /// Unity Event on game object destroy
        /// </summary>
        public void OnDestroy()
        {
            if(m_Ocr != null)
            {
                m_Ocr.Dispose();
            }
        }

        /// <summary>
        /// Initializes tesseract ocr library
        /// </summary>
        public void InitializeTesseract()
        {
            m_Ocr = new TesseractOCRImpl();
            m_Ocr.Initialize(m_LanguageId);
            m_VersionText.text = string.Format("Tesseract Version: {0}", m_Ocr.GetVersion());
        }

        /// <summary>
        /// Converts image to text
        /// </summary>
        public void ConvertImageToText()
        {
            m_SnapshotTexture = new Texture2D(m_WebCamTexture.width, m_WebCamTexture.height, TextureFormat.RGB24, false);
            Color32[] colorBuffer = TesseractOCRUtility.ImageRotateClockwise(m_WebCamTexture.GetPixels32(), m_WebCamTexture.width, m_WebCamTexture.height);

            m_SnapshotTexture.Resize(m_WebCamTexture.height, m_WebCamTexture.width);
            m_SnapshotTexture.SetPixels32(colorBuffer);
            m_SnapshotTexture.Apply();

            m_Ocr.SetImage(m_SnapshotTexture);
            m_InputField.text = m_Ocr.GetText();
        }
    }

} // namespace PixelSquare