//------------------------------------------------------------------------------
// <copyright file="TesseractOCRImpl.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary> TODO: Describe the file's implementation overview here </summary>
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PixelSquare.TesseractOCR
{
	using Enums;
	using Utility;

	/// <summary>
	/// TODO: Describe the class outline here
	/// </summary>
	/// <remarks> Detailed information about the class. </remarks>
	public class TesseractOCRImpl : TesseractOCRBridge, IOpticalCharacterReader
	{
		private const int BYTES_PER_PIXEL = 3;

		private IntPtr m_Handle = IntPtr.Zero;

		public bool Initialize()
        {
			if(m_Handle == IntPtr.Zero)
            {
				m_Handle = CreateHandle();
            }

            int init = Initialize(m_Handle, Application.persistentDataPath + "/tessdata", "eng+jpn");
			return init == 0;
        }

		public void SetImage(Image image)
		{
			Debug.Assert(image, "Image must not be nulled!");
			SetImage(image.sprite);
        }

		public void SetImage(RawImage image)
		{
			Debug.Assert(image, "Image must not be nulled!");
			SetImage(image.mainTexture);
        }

		public void SetImage(Sprite sprite)
		{
			Debug.Assert(sprite, "Sprite must not be nulled!");
			SetImage(sprite.texture);
        }

		public void SetImage(Texture texture)
		{
			Debug.Assert(texture, "Texture must not be nulled!");
			SetImage((Texture2D)texture);
        }

		public void SetImage(Texture2D texture)
		{
			Debug.Assert(texture, "Texture must not be nulled!");
			SetImage(texture.GetRawTextureData(), texture.width, texture.height);
        }

		delegate void OrientationFunc(Orientation orientation, WritingDirection direction, TextlineOrder order, float skew_angle);

		public void SetImage(byte[] imageData, int width, int height)
		{
			Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");

            Debug.Log(GetLanguage());

            //SetPageSegmentationMode(m_Handle, Enums.SegmentationMode.SINGLE_BLOCK);
            //Debug.Log((SegmentationMode)GetPageSegmentationMode(m_Handle));

            //Debug.Log(GetTesseractEngineMode());

            imageData = TesseractOCRUtility.FlipVertical(imageData, width, height, BYTES_PER_PIXEL);
            SetImageData(m_Handle, imageData, width, height, BYTES_PER_PIXEL, BYTES_PER_PIXEL * width);
        }

		public string GetText()
		{
			Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");
			IntPtr textPtr = GetTextData(m_Handle);
			Debug.Assert(textPtr != IntPtr.Zero, "Text must not be nulled!");
			return Marshal.PtrToStringAnsi(textPtr);

		}

		public string GetVersion()
        {
			IntPtr versionPtr = GetTesseractVersion();
			Debug.Assert(versionPtr != IntPtr.Zero, "Version must not be nulled");
			return Marshal.PtrToStringAnsi(versionPtr);
		}

		public void Dispose()
        {
			if(m_Handle != IntPtr.Zero)
            {
				EndTesseract(m_Handle);
				DeleteHandle(m_Handle);
				m_Handle = IntPtr.Zero;
            }
        }

		private string GetDataPath()
		{
			Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");
			IntPtr dataPathPtr = GetTesseractDataPath(m_Handle);
			Debug.Assert(dataPathPtr != IntPtr.Zero, "Datapath must not be nulled!");
			return Marshal.PtrToStringAnsi(dataPathPtr); ;
        }

		private string GetLanguage()
		{
			Debug.Assert(m_Handle != IntPtr.Zero, "Handle must not be nulled!");
			IntPtr languagePtr = GetTesseractLanguage(m_Handle);
			Debug.Assert(languagePtr != IntPtr.Zero, "Datapath must not be nulled!");
			return Marshal.PtrToStringAnsi(languagePtr); ;
		}
	}
	
} // namespace PixelSquare