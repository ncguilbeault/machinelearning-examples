using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using static TorchSharp.torch;
using static TorchSharp.torch.linalg;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class Inv
{
    public IObservable<Tensor> Process(IObservable<Tensor> source)
    {
        return source.Select(value => inv(value));
    }
}
