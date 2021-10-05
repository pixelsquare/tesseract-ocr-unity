//------------------------------------------------------------------------------
// <copyright file="TesseractOCRImageRotation.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/10/05</date>
// <summary>Tesseract OCR Image Rotation Demonstration</summary>
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;

using System.Collections;
using System.Collections.Generic;

namespace PixelSquare
{
    /// <summary>
    /// Tesseract OCR Image Rotation Demonstration
    /// Used to check the algorithm for rotating and flipping image buffer
    /// </summary>
    /// <remarks></remarks>
    public class TesseractOCRImageRotation : MonoBehaviour
    {
        /// <summary>
        /// Sample texture image
        /// </summary>
        [SerializeField] private RawImage m_SampleImage = null;

        /// <summary>
        /// Rotates the image in clockwise direction
        /// </summary>
        public void RotateClockwise()
        {
            Texture2D sampleTex = (Texture2D)m_SampleImage.texture;
            Color32[] colors = TesseractOCR.Utility.TesseractOCRUtility.ImageRotateClockwise(sampleTex.GetRawTextureData(), sampleTex.width, sampleTex.height);

            Texture2D resultTex = new Texture2D(sampleTex.width, sampleTex.height, TextureFormat.RGB24, false);
            resultTex.filterMode = FilterMode.Point;
            resultTex.Resize(sampleTex.height, sampleTex.width);
            resultTex.SetPixels32(colors);
            resultTex.Apply();

            m_SampleImage.texture = resultTex;
            m_SampleImage.SetNativeSize();
        }

        /// <summary>
        /// Rotates the image in counter clockwise direction
        /// </summary>
        public void RotateCounterClockwise()
        {
            Texture2D sampleTex = (Texture2D)m_SampleImage.texture;
            Color32[] colors = TesseractOCR.Utility.TesseractOCRUtility.ImageRotateCounterClockwise(sampleTex.GetRawTextureData(), sampleTex.width, sampleTex.height);

            Texture2D resultTex = new Texture2D(sampleTex.width, sampleTex.height, TextureFormat.RGB24, false);
            resultTex.filterMode = FilterMode.Point;
            resultTex.Resize(sampleTex.height, sampleTex.width);
            resultTex.SetPixels32(colors);
            resultTex.Apply();

            m_SampleImage.texture = resultTex;
            m_SampleImage.SetNativeSize();
        }

        /// <summary>
        /// Flips the image vertically
        /// </summary>
        public void FlipVertical()
        {
            Texture2D sampleTex = (Texture2D)m_SampleImage.texture;
            Color32[] colors = TesseractOCR.Utility.TesseractOCRUtility.ImageFlipVertical(sampleTex.GetRawTextureData(), sampleTex.width, sampleTex.height);

            Texture2D resultTex = new Texture2D(sampleTex.width, sampleTex.height, TextureFormat.RGB24, false);
            resultTex.filterMode = FilterMode.Point;
            resultTex.SetPixels32(colors);
            resultTex.Apply();

            m_SampleImage.texture = resultTex;
            m_SampleImage.SetNativeSize();
        }

        /// <summary>
        /// Flips the image horizontally
        /// </summary>
        public void FlipHorizontal()
        {
            Texture2D sampleTex = (Texture2D)m_SampleImage.texture;
            Color32[] colors = TesseractOCR.Utility.TesseractOCRUtility.ImageFlipHorizontal(sampleTex.GetRawTextureData(), sampleTex.width, sampleTex.height);

            Texture2D resultTex = new Texture2D(sampleTex.width, sampleTex.height, TextureFormat.RGB24, false);
            resultTex.filterMode = FilterMode.Point;
            resultTex.SetPixels32(colors);
            resultTex.Apply();

            m_SampleImage.texture = resultTex;
            m_SampleImage.SetNativeSize();
        }

    }

} // namespace PixelSquare