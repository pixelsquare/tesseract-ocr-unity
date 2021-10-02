//------------------------------------------------------------------------------
// <copyright file="TesseractOCRTypes.cs" company="PixelSquare">
//    Copyright © 2021 PixelSquare
// </copyright>
// <author>Anthony G.</author>
// <date>2021/09/30</date>
// <summary> TODO: Describe the file's implementation overview here </summary>
//------------------------------------------------------------------------------

namespace PixelSquare.TesseractOCR.Enums
{
    public enum EngineMode : uint
    {
        TESSERACT_ONLY = 0,
        LSTM_ONLY,
        TESSERACT_LSTM_COMBINED,
        DEFAULT
    }

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

    public enum Orientation : uint
    {
        PAGE_UP = 0,
        PAGE_RIGHT,
        PAGE_DOWN,
        PAGE_LEFT
    }

    public enum WritingDirection : uint
    {
        LEFT_TO_RIGHT = 0,
        RIGHT_TO_LEFT,
        TOP_TO_BOTTOM
    }

    public enum TextlineOrder : uint
    {
        LEFT_TO_RIGHT = 0,
        RIGHT_TO_LEFT,
        TOP_TO_BOTTOM
    }

} // namespace PixelSquare