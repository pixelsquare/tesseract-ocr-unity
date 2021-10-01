//------------------------------------------------------------------------------
// <copyright file="IOpticalCharacterReader.cs" company="PixelSquare">
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

namespace PixelSquare.TesseractOCR
{
	public interface IOpticalCharacterReader : System.IDisposable
    {
        bool Initialize();

        void SetImage(Image image);

        void SetImage(RawImage image);

        void SetImage(Sprite sprite);

        void SetImage(Texture texture);

        void SetImage(Texture2D texture);

        void SetImage(byte[] imageData, int width, int height);

        string GetText();

        string GetVersion();
    }
	
} // namespace PixelSquare