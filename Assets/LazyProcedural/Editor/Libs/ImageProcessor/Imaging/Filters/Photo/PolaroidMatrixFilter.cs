﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolaroidMatrixFilter.cs" company="James Jackson-South">
//   Copyright (c) James Jackson-South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// <summary>
//   Encapsulates methods with which to add a Polaroid filter to an image.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImageProcessor.Imaging.Filters.Photo
{
    using System.Drawing;
    using System.Drawing.Imaging;

    using ImageProcessor.Imaging.Helpers;

    /// <summary>
    /// Encapsulates methods with which to add a Polaroid filter to an image.
    /// </summary>
    internal class PolaroidMatrixFilter : MatrixFilterBase
    {
        /// <summary>
        /// Gets the <see cref="T:System.Drawing.Imaging.ColorMatrix"/> for this filter instance.
        /// </summary>
        public override ColorMatrix Matrix => ColorMatrixes.Polaroid;

        /// <summary>
        /// Processes the image.
        /// </summary>
        /// <param name="source">The current image to process</param>
        /// <param name="destination">The new Image to return</param>
        /// <returns>
        /// The processed <see cref="System.Drawing.Bitmap"/>.
        /// </returns>
        public override Bitmap TransformImage(Image source, Image destination)
        {
            using (var graphics = Graphics.FromImage(destination))
            {
                using (var attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(this.Matrix);

                    var rectangle = new Rectangle(0, 0, source.Width, source.Height);

                    graphics.DrawImage(source, rectangle, 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            // Fade the contrast
            destination = Adjustments.Contrast(destination, -25);

            // Add a glow to the image.
            destination = Effects.Glow(destination, Color.FromArgb(70, 255, 153, 102));

            // Add a vignette to finish the effect.
            destination = Effects.Vignette(destination, Color.FromArgb(220, 102, 34, 0));

            return (Bitmap)destination;
        }
    }
}
