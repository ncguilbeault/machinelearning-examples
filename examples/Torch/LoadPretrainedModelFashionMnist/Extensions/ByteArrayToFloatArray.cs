using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ByteArrayToFloatArray
{
    public IObservable<float[]> Process(IObservable<byte[]> source)
    {
        return source.Select(value => {
            var floats = value.Select(b => {
                return (float)b;
            }).ToArray();
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(string.Format("Float {0}: {1}", i, floats[i]));
            }
            return floats;
        });
    }
}
