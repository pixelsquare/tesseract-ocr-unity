//------------------------------------------------------------------------------
// <copyright file="TesseractOCRUtility.cs" company="PixelSquare">
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

namespace PixelSquare.TesseractOCR.Utility
{
	/// <summary>
	/// TODO: Describe the class outline here
	/// </summary>
	/// <remarks> Detailed information about the class. </remarks>
	public class TesseractOCRUtility
	{
        public static byte[] ParseImageBytes(Image image)
        {
            Debug.Assert(image, "Image must not be nulled!");
            return ParseImageBytes(image.sprite);
        }

        public static byte[] ParseImageBytes(RawImage image)
        {
            Debug.Assert(image, "Image must not be nulled!");
            return ParseImageBytes(image.mainTexture);
        }

        public static byte[] ParseImageBytes(Sprite sprite)
        {
            Debug.Assert(sprite, "Sprite must not be nulled!");
            return ParseImageBytes(sprite.texture);
        }

        public static byte[] ParseImageBytes(Texture texture)
        {
            Debug.Assert(texture, "Texture must not be nulled!");
            return ParseImageBytes((Texture2D)texture);
        }

        public static byte[] ParseImageBytes(Texture2D texture)
        {
            Debug.Assert(texture, "Texture must not be nulled!");
            return ParseImageBytes(texture.GetRawTextureData(), texture.width, texture.height);
        }

        public static byte[] ParseImageBytes(byte[] imageData, int width, int height)
        {
            return imageData;
        }

        public static byte[] FlipHorizontal(byte[] imageBuffer, int width, int height, int size = 3)
        {
			int dataLen = imageBuffer.Length;
			byte[] result = new byte[dataLen];

			List<byte> r = new List<byte>();
			List<byte> tmp = new List<byte>();
			for(int i = 0; i < dataLen / size; i++)
            {
				for(int j = 0; j < size; j++)
                {
					tmp.Add(imageBuffer[i + j]);
                }

				if(tmp.Count >= width * size)
                {
					tmp.Reverse();
					r.AddRange(tmp);
					tmp.Clear();
                }
            }

			result = r.ToArray();

            //// TODO: FIX
            //for(int i = 0; i < dataLen / size; i++)
            //{
            //    for(int j = 0; j < size; j++)
            //    {
            //        result[i * size + j] = imageBuffer[(i + 1) * size - j - 1];
            //    }
            //}

            return result;
        }

		public static byte[] FlipVertical(byte[] imageBuffer, int width, int height, int size = 3)
        {
			int dataLen = imageBuffer.Length;
			byte[] result = new byte[dataLen];

			for(int i = 0; i < height; i++)
			{
				int j = height - i - 1;
				Array.Copy(imageBuffer, i * width * size, result, j * width * size, width * size);
			}

			// Without width and height
			//for(int i = 0; i < dataLen / size; i++)
			//{
			//	Array.Copy(imageData, result.Length - (i + 1) * size, result, i * size, size);
			//}

			return result;
        }
	}
	
} // namespace PixelSquare