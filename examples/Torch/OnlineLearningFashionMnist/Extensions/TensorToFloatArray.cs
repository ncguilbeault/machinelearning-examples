using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using TorchSharp;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class TensorToFloatArray
{
    public IObservable<float[]> Process(IObservable<byte[,,]> source)
    {
            return source.Select(value => {
            var floats = new float[value.Length];
            var i = 0;
            for (int j = 0; j < value.GetUpperBound(2); j++)
            {
                for (int k = 0; k < value.GetUpperBound(1); k++)
                {
                    floats[i++] = (float)value[0, k, j];
                }
            }
            for (int l = 0; l < 15; l++)
            {
                Console.WriteLine(string.Format("Float {0}: {1}", l, floats[l]));
            }
            return floats;
        });
    }
}
