using Bonsai;
using Bonsai.Design;
using Bonsai.Vision.Design;
using System;
using System.Collections.Generic;
using OpenCV.Net;

[assembly: TypeVisualizer(typeof(BoundingBoxOverlay), 
    Target = typeof(MashupSource<ImageMashupVisualizer, BoundingBoxVisualizer>))]

public class BoundingBoxOverlay : DialogTypeVisualizer
{
    private ImageMashupVisualizer visualizer;

    /// <inheritdoc/>
    public override void Show(object value)
    {

        var image = visualizer.VisualizerImage;
        var boundingBoxes = (BoundingBoxCollection)value;

        for (int i = 0; i < boundingBoxes.Count; i++)
        {
            var boundingBox = boundingBoxes[i];
            CV.Rectangle(image, boundingBox.Corners[0], boundingBox.Corners[1], new Scalar(0, 0, 255, 255), 2);
        }
    }
    
    /// <inheritdoc/>
    public override void Load(IServiceProvider provider)
    {
        visualizer = (ImageMashupVisualizer)provider.GetService(typeof(MashupVisualizer));
    }

    /// <inheritdoc/>
    public override void Unload()
    {
    }
}