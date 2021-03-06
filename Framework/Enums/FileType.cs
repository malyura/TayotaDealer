﻿using System.ComponentModel;

namespace Framework.Enums
{
    public enum FileType
    {
        [Description(".csv")]
        Csv,
        [Description(".xlsx")]
        Excel,
        [Description(".crdownload")]
        ChromeDownload,
        [Description(".part")]
        FirefoxDownload,
        [Description(".png")]
        PngImage
    }
}
