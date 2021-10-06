//------------------------------------------------------------------------------
// <copyright file="IOpticalCharacterReader.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary>Interface for Optical Character Reader</summary>
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

namespace PixelSquare.TesseractOCR
{
    /// <summary>
    /// Interface for Optical Character Reader
    /// </summary>
    public interface IOpticalCharacterReader : System.IDisposable
    {
        /// <summary>
        /// Initializes the OCR library.
        /// </summary>
        /// <param name="languageId">Language ID</param>
        void Initialize(string languageId);

        /// <summary>
        /// Sets the image to be used by the library
        /// </summary>
        /// <param name="image">Image</param>
        void SetImage(Image image);

        /// <summary>
        /// Sets the image to be used by the library
        /// </summary>
        /// <param name="image">Raw Image</param>
        void SetImage(RawImage image);

        /// <summary>
        /// Sets the image to be used by the library
        /// </summary>
        /// <param name="sprite">Sprite</param>
        void SetImage(Sprite sprite);

        /// <summary>
        /// Sets the image to be used by the library
        /// </summary>
        /// <param name="texture">Texture</param>
        void SetImage(Texture texture);

        /// <summary>
        /// Sets the image to be used by the library
        /// </summary>
        /// <param name="texture">Texture2D</param>
        void SetImage(Texture2D texture);

        /// <summary>
        /// Sets the image to be used by the library
        /// </summary>
        /// <param name="imageData">Image data buffer</param>
        /// <param name="width">Image Width</param>
        /// <param name="height">Image Height</param>
        void SetImage(byte[] imageData, int width, int height);

        /// <summary>
        /// Sets the configuration file
        /// </summary>
        /// <param name="filename">Config Filename</param>
        /// <param name="isDebug">Is Debug</param>
        void SetConfigurationFile(string filename, bool isDebug);

        /// <summary>
        /// Gets the text from the image in UTF-8 format
        /// </summary>
        /// <returns>UTF-8 Text</returns>
        string GetText();

        /// <summary>
        /// Gets the language id used by the library
        /// </summary>
        /// <returns>Language ID</returns>
        string GetLanguageId();

        /// <summary>
        /// Gets the ocr data path directory
        /// </summary>
        /// <returns>Tess data directory</returns>
        string GetDataPath();

        /// <summary>
        /// Gets the library version
        /// </summary>
        /// <returns>Library Version</returns>
        string GetVersion();
    }
    
} // namespace PixelSquare