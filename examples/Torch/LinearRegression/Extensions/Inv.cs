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
public class Inv
{
    public IObservable<torch.Tensor> Process(IObservable<torch.Tensor> source)
    {
        return source.Select(value => torch.linalg.inv(value));
    }
}
