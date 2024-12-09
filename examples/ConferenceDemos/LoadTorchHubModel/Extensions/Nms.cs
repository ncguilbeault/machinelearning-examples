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
public class Nms
{
    private double ioU = 0.9;
    public double IoU 
    {
        get { return ioU; }
        set { ioU = value; }
    }
    public IObservable<torch.Tensor> Process(IObservable<Tuple<torch.Tensor, torch.Tensor>> source)
    {
        return source.Select(value => {
            return torchvision.ops.nms(value.Item1, value.Item2, ioU);
        });
    }
}
