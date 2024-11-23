using Bonsai;
using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using TorchSharp;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Source)]
public class LoadTensor
{
    public string TensorPath { get; set; }
    public IObservable<torch.Tensor> Process()
    {
        return Observable.Return(torch.Tensor.Load(TensorPath));
    }
}
