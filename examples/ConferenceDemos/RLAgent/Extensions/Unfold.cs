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
public class Unfold
{
    private int kernelSize = 5;
    public int KernelSize
    {
        get { return kernelSize; }
        set
        {
            if (value > 1)
                kernelSize = value;
            else
                kernelSize = 1;
        }
    }

    private int stride = 5;
    public int Stride
    {
        get { return stride; }
        set
        {
            if (value > 1)
                stride = value;
            else
                stride = 1;
        }
    }

    private int padding = 5;
    public int Padding
    {
        get { return padding; }
        set
        {
            if (value > 1)
                padding = value;
            else
                padding = 1;
        }
    }

    public IObservable<torch.Tensor> Process(IObservable<torch.Tensor> source)
    {
        return source.Select(value => {
            using (torch.NewDisposeScope())
            {
                var padded = torch.nn.functional.pad(value, new long[] { padding, padding, padding, padding });
                var unfold1 = padded.unfold(0, kernelSize, kernelSize);
                var unfold2 = unfold1.unfold(1, kernelSize, kernelSize);
                return unfold2.sum(2, 3).MoveToOuterDisposeScope();
            }
            // return torch.nn.functional.unfold(value, kernel_size: KernelSize, stride: Stride, padding: Padding);
        });
    }
}
