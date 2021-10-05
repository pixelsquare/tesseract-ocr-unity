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

        /// <summary>
        /// Convert byte array to color32 array
        /// </summary>
        /// <param name="imageBuffer">Image Buffer</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Color32 Array</returns>
        public static Color32[] BytesToColor32(byte[] imageBuffer, int bytesPerPixel = 3)
        {
            int bufferLen = imageBuffer.Length / bytesPerPixel;
            Color32[] result = new Color32[bufferLen];

            int x = 0;
            for(int i = 0; i < imageBuffer.Length; i += bytesPerPixel)
            {
                result[x++] = new Color32(imageBuffer[i], imageBuffer[i + 1], imageBuffer[i + 2], 0xFF);
            }

            return result;
        }

        /// <summary>
        /// Convert color32 array to byte array
        /// </summary>
        /// <param name="colorBuffer">Color32 Buffer</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Byte Array</returns>
        public static byte[] Color32ToBytes(Color32[] colorBuffer, int bytesPerPixel = 3)
        {
            int bufferLen = colorBuffer.Length * bytesPerPixel;
            byte[] result = new byte[bufferLen];

            int x = 0;
            for(int i = 0; i < bufferLen; i += 3)
            {
                result[i] = colorBuffer[x].r;
                result[i + 1] = colorBuffer[x].g;
                result[i + 2] = colorBuffer[x].b;
                x++;
            }

            return result;
        }

        /// <summary>
        /// Rotate image in clockwise direction
        /// </summary>
        /// <param name="imageBuffer">Image Buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Color32 Array</returns>
        public static Color32[] ImageRotateClockwise(byte[] imageBuffer, int width, int height, int bytesPerPixel = 3)
        {
            return ImageRotateClockwise(BytesToColor32(imageBuffer, bytesPerPixel), width, height, bytesPerPixel);
        }

        /// <summary>
        /// Rotate image in clockwise direction
        /// </summary>
        /// <param name="colorBuffer">Color Buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Color32 Array</returns>
        public static Color32[] ImageRotateClockwise(Color32[] colorBuffer, int width, int height, int bytesPerPixel = 3)
        {
            Color32[] result = new Color32[width * height];

            int index = 0;
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    int sourceIdx = (width * (j + 1)) - (i + 1);
                    result[index] = colorBuffer[sourceIdx];
                    index++;
                }
            }

            return result;
        }

        /// <summary>
        /// Rotate image in counter clockwise direction
        /// </summary>
        /// <param name="imageBuffer">Image Buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Color32 Array</returns>
        public static Color32[] ImageRotateCounterClockwise(byte[] imageBuffer, int width, int height, int bytesPerPixel = 3)
        {
            return ImageRotateCounterClockwise(BytesToColor32(imageBuffer, bytesPerPixel), width, height, bytesPerPixel);
        }

        /// <summary>
        /// Rotate image in counter clockwise direction
        /// </summary>
        /// <param name="colorBuffer">Color Buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Color32 Array</returns>
        public static Color32[] ImageRotateCounterClockwise(Color32[] colorBuffer, int width, int height, int bytesPerPixel = 3)
        {
            Color32[] result = new Color32[width * height];

            int index = 0;
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    int sourceIdx = (width * (height - j)) - width + i;
                    result[index] = colorBuffer[sourceIdx];
                    index++;
                }
            }

            return result;
        }

        /// <summary>
        /// Flips an image horizontally
        /// </summary>
        /// <param name="imageBuffer">Image Buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Color32 Array</returns>
        public static Color32[] ImageFlipHorizontal(byte[] imageBuffer, int width, int height, int bytesPerPixel = 3)
        {
            return ImageFlipHorizontal(BytesToColor32(imageBuffer, bytesPerPixel), width, height, bytesPerPixel);
        }

        /// <summary>
        /// Flips an image horizontally
        /// </summary>
        /// <param name="colorBuffer">Color Buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Color32 Array</returns>
        public static Color32[] ImageFlipHorizontal(Color32[] colorBuffer, int width, int height, int bytesPerPixel = 3)
        {
            Color32[] result = new Color32[width * height];

            int index = 0;
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    int sourceIdx = width - j - 1 + (width * i);
                    result[index] = colorBuffer[sourceIdx];
                    index++;
                }
            }

            return result;
        }

        /// <summary>
        /// Flips an image vertically
        /// </summary>
        /// <param name="imageBuffer">Image Buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Color32 Array</returns>
        public static Color32[] ImageFlipVertical(byte[] imageBuffer, int width, int height, int bytesPerPixel = 3)
        {
            return ImageFlipVertical(BytesToColor32(imageBuffer, bytesPerPixel), width, height, bytesPerPixel);
        }

        /// <summary>
        /// Flips an image vertically
        /// </summary>
        /// <param name="colorBuffer">Color Buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        /// <param name="bytesPerPixel">Bytes Per Pixel</param>
        /// <returns>Color32 Array</returns>
        public static Color32[] ImageFlipVertical(Color32[] colorBuffer, int width, int height, int bytesPerPixel = 3)
        {
            Color32[] result = new Color32[width * height];

            int index = 0;
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    int sourceIdx = (height * width) - ((i + 1) * width - j);
                    result[index] = colorBuffer[sourceIdx];
                    index++;
                }
            }

            return result;
        }
    }
    
} // namespace PixelSquare