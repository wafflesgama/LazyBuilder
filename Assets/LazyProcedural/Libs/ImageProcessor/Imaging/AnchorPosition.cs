﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnchorPosition.cs" company="James Jackson-South">
//   Copyright (c) James Jackson-South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// <summary>
//   Enumerated anchor positions to apply to resized images.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImageProcessor.Imaging
{
    /// <summary>
    /// Enumerated anchor positions to apply to resized images.
    /// </summary>
    public enum AnchorPosition
    {
        /// <summary>
        /// Anchors the position of the image to the center of it's bounding container.
        /// </summary>
        Center,

        /// <summary>
        /// Anchors the position of the image to the top of it's bounding container.
        /// </summary>
        Top,

        /// <summary>
        /// Anchors the position of the image to the bottom of it's bounding container.
        /// </summary>
        Bottom,

        /// <summary>
        /// Anchors the position of the image to the left of it's bounding container.
        /// </summary>
        Left,

        /// <summary>
        /// Anchors the position of the image to the right of it's bounding container.
        /// </summary>
        Right,

        /// <summary>
        /// Anchors the position of the image to the top left side of it's bounding container.
        /// </summary>
        TopLeft,

        /// <summary>
        /// Anchors the position of the image to the top right side of it's bounding container.
        /// </summary>
        TopRight,

        /// <summary>
        /// Anchors the position of the image to the bottom right side of it's bounding container.
        /// </summary>
        BottomRight,

        /// <summary>
        /// Anchors the position of the image to the bottom left side of it's bounding container.
        /// </summary>
        BottomLeft
    }
}
