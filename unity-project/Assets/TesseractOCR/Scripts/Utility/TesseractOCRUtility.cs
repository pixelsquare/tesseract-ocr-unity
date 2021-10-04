//------------------------------------------------------------------------------
// <copyright file="TesseractOCRUtility.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary>Utility class for tesseract ocr library.</summary>
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

namespace PixelSquare.TesseractOCR.Utility
{
    /// <summary>
    /// Utility class for tesseract ocr library.
    /// </summary>
    /// <remarks> Detailed information about the class. </remarks>
    public class TesseractOCRUtility
    {
        /// <summary>
        /// Formatted bytes suffixes
        /// </summary>
        private static readonly string[] sizeSuffix = { "B", "KB", "MB", "GB", "TB" };

        /// <summary>
        /// Formats a byte length to human readable expression
        /// </summary>
        /// <param name="bytes">Byte Length</param>
        /// <returns>Bytes in readable expression</returns>
        public static string FormatBytes(long bytes)
        {
            double len = bytes;
            int order = 0;

            while(len >= 1024 && order < bytes)
            {
                order++;
                len = len / 1024;
            }

            return string.Format("{0:0.##} {1}", len, sizeSuffix[order]);
        }

        //public static byte[] ParseImageBytes(Image image)
        //{
        //    Debug.Assert(image, "Image must not be nulled!");
        //    return ParseImageBytes(image.sprite);
        //}

        //public static byte[] ParseImageBytes(RawImage image)
        //{
        //    Debug.Assert(image, "Image must not be nulled!");
        //    return ParseImageBytes(image.mainTexture);
        //}

        //public static byte[] ParseImageBytes(Sprite sprite)
        //{
        //    Debug.Assert(sprite, "Sprite must not be nulled!");
        //    return ParseImageBytes(sprite.texture);
        //}

        //public static byte[] ParseImageBytes(Texture texture)
        //{
        //    Debug.Assert(texture, "Texture must not be nulled!");
        //    return ParseImageBytes((Texture2D)texture);
        //}

        //public static byte[] ParseImageBytes(Texture2D texture)
        //{
        //    Debug.Assert(texture, "Texture must not be nulled!");
        //    return ParseImageBytes(texture.GetRawTextureData(), texture.width, texture.height);
        //}

        //public static byte[] ParseImageBytes(byte[] imageData, int width, int height)
        //{
        //    return imageData;
        //}

        /// <summary>
        /// Flips the texture horizontally
        /// </summary>
        /// <param name="imageBuffer">Image data buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Image bytes per pixel</param>
        /// <returns>Flipped Image Data Buffer</returns>
        public static byte[] FlipHorizontal(byte[] imageBuffer, int width, int height, int bytesPerPixel = 3)
        {
            int dataLen = imageBuffer.Length;
            byte[] result = new byte[dataLen];

            List<byte> r = new List<byte>();
            List<byte> tmp = new List<byte>();
            for(int i = 0; i < dataLen / bytesPerPixel; i++)
            {
                for(int j = 0; j < bytesPerPixel; j++)
                {
                    tmp.Add(imageBuffer[i + j]);
                }

                if(tmp.Count >= width * bytesPerPixel)
                {
                    tmp.Reverse();
                    r.AddRange(tmp);
                    tmp.Clear();
                }
            }

            result = r.ToArray();

            //// TODO: FIX
            //for(int i = 0; i < dataLen / bytesPerPixel; i++)
            //{
            //    for(int j = 0; j < bytesPerPixel; j++)
            //    {
            //        result[i * bytesPerPixel + j] = imageBuffer[(i + 1) * bytesPerPixel - j - 1];
            //    }
            //}

            return result;
        }

        /// <summary>
        /// Flips the texture vertically
        /// </summary>
        /// <param name="imageBuffer">Image data buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Image bytes per pixel</param>
        /// <returns>Flipped Image Data Buffer</returns>
        public static byte[] FlipVertical(byte[] imageBuffer, int width, int height, int bytesPerPixel = 3)
        {
            int dataLen = imageBuffer.Length;
            byte[] result = new byte[dataLen];

            for(int i = 0; i < height; i++)
            {
                int j = height - i - 1;
                Array.Copy(imageBuffer, i * width * bytesPerPixel, result, j * width * bytesPerPixel, width * bytesPerPixel);
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