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
		[SerializeField] private RawImage m_Image = null;
		[SerializeField] private Text m_Text = null;

		private IOpticalCharacterReader m_Ocr = null;

		/// <summary>
		/// Use this for initialization
		/// </summary>
		public void Start() 
		{
			m_Ocr = new TesseractOCRImpl();
            Debug.Log(m_Ocr.Initialize());
            Debug.Log(m_Ocr.GetVersion());
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
			m_Ocr.SetImage(m_Image);
			m_Text.text = m_Ocr.GetText();
        }
	}
	
} // namespace PixelSquare