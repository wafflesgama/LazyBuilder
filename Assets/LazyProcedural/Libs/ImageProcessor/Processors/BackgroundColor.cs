﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BackgroundColor.cs" company="James Jackson-South">
//   Copyright (c) James Jackson-South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// <summary>
//   Changes the background color of an image.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImageProcessor.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    using ImageProcessor.Common.Exceptions;
    using ImageProcessor.Imaging.Helpers;

    /// <summary>
    /// Changes the background color of an image.
    /// </summary>
    public class BackgroundColor : IGraphicsProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundColor"/> class.
        /// </summary>
        public BackgroundColor() => this.Settings = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the DynamicParameter.
        /// </summary>
        public dynamic DynamicParameter { get; set; }

        /// <summary>
        /// Gets or sets any additional settings required by the processor.
        /// </summary>
        public Dictionary<string, string> Settings { get; set; }

        /// <summary>
        /// Processes the image.
        /// </summary>
        /// <param name="factory">The current instance of the 
        /// <see cref="T:ImageProcessor.ImageFactory" /> class containing
        /// the image to process.</param>
        /// <returns>
        /// The processed image from the current instance of the <see cref="T:ImageProcessor.ImageFactory" /> class.
        /// </returns>
        public Image ProcessImage(ImageFactory factory)
        {
            Bitmap newImage = null;
            Image image = factory.Image;

            try
            {
                int width = image.Width;
                int height = image.Height;

                Color backgroundColor = this.DynamicParameter;
                newImage = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                // Make a graphics object from the empty bitmap.
                using (var graphics = Graphics.FromImage(newImage))
                {
                    GraphicsHelper.SetGraphicsOptions(graphics, true);

                    // Fill the background.
                    graphics.Clear(backgroundColor);

                    // Draw passed in image onto graphics object.
                    graphics.DrawImage(image, 0, 0, width, height);
                }

                if (backgroundColor.A < byte.MaxValue)
                {
                    factory.CurrentBitDepth = (long)BitDepth.Bit32;
                }

                image.Dispose();
                image = newImage;
            }
            catch (Exception ex)
            {
                newImage?.Dispose();

                throw new ImageProcessingException("Error processing image with " + this.GetType().Name, ex);
            }

            return image;
        }
    }
}