using OpenCV.Net;
using System;
using System.Collections.Generic;
using Bonsai;
using Bonsai.Design;

public class BoundingBox
{
    public Point[] Corners { get; set; }
}

[TypeVisualizer(typeof(BoundingBoxVisualizer))]
public class BoundingBoxCollection : List<BoundingBox>
{
    public BoundingBoxCollection()
    {
    }

    public BoundingBoxCollection(IEnumerable<BoundingBox> collection)
        : base(collection)
    {
    }
}

public class BoundingBoxVisualizer : DialogTypeVisualizer
{
    public override void Load(IServiceProvider provider)
    {
    }

    public override void Unload()
    {
    }

    public override void Show(object value)
    {
    }
}
