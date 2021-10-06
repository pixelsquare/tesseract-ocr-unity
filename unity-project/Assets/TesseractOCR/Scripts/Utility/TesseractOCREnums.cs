//------------------------------------------------------------------------------
// <copyright file="TesseractOCRTypes.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary>Contains all tesseract's enums</summary>
//------------------------------------------------------------------------------

namespace PixelSquare.TesseractOCR.Enums
{
    /// <summary>
    /// Engine Mode
    /// </summary>
    public enum OCREngineMode : uint
    {
        TESSERACT_ONLY = 0,
        LSTM_ONLY,
        TESSERACT_LSTM_COMBINED,
        DEFAULT
    }

    /// <summary>
    /// Segmentation Mode
    /// </summary>
    public enum SegmentationMode : uint
    {
        OSD_ONLY = 0,
        AUTO_OSD,
        AUTO_ONLY,
        AUTO,
        SINGLE_COLUMN,
        SINGLE_BLOCK_VERT_TEXT,
        SINGLE_BLOCK,
        SINGLE_LINE,
        SINGLE_WORD,
        CIRCLE_WORD,
        SINGLE_CHAR,
        SPARSE_CHAR,
        SPARSE_TEXT_OSD,
        RAW_LINE,
        COUNT
    }

    /// <summary>
    /// Image Orientation
    /// </summary>
    public enum Orientation : uint
    {
        PAGE_UP = 0,
        PAGE_RIGHT,
        PAGE_DOWN,
        PAGE_LEFT
    }

    /// <summary>
    /// Writing Direction
    /// </summary>
    public enum WritingDirection : uint
    {
        LEFT_TO_RIGHT = 0,
        RIGHT_TO_LEFT,
        TOP_TO_BOTTOM
    }

    /// <summary>
    /// Textline Order
    /// </summary>
    public enum TextlineOrder : uint
    {
        LEFT_TO_RIGHT = 0,
        RIGHT_TO_LEFT,
        TOP_TO_BOTTOM
    }

    /// <summary>
    /// Page Iterator Level
    /// </summary>
    public enum PageIteratorLevel : uint
    {
        BLOCK = 0,
        PARAGRAPH,
        TEXTLINE,
        WORD,
        SYMBOL
    }

} // namespace PixelSquare