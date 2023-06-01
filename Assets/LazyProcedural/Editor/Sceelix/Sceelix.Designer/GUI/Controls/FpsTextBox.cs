﻿using System;
using DigitalRune.Game.UI.Controls;

namespace Sceelix.Designer.GUI.Controls
{
    // Displays the frame rate in FPS (frames per second).
    public class FpsTextBlock : TextBlock
    {
        // This class is a TextBlock.
        // In OnRender the time is measured, and every x seconds the average frame rate is 
        // computed and displayed as the text of this TextBlock.

        private readonly TimeSpan _sampleInterval = new TimeSpan(0, 0, 0, 1);
        private float _numberOfFrames;

        private TimeSpan _sampleTime;



        public FpsTextBlock()
        {
            Text = "FPS: -";
        }



        protected override void OnRender(UIRenderContext context)
        {
            _sampleTime += context.DeltaTime;
            _numberOfFrames++;

            if (_sampleTime > _sampleInterval)
            {
                Text = string.Format("FPS: {0}", (int) (_numberOfFrames/(float) _sampleTime.TotalSeconds + 0.5f));

                _sampleTime = TimeSpan.Zero;
                _numberOfFrames = 0;
            }

            base.OnRender(context);
        }
    }
}