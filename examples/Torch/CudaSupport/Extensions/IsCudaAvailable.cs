using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using TorchSharp;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Source)]
public class IsCudaAvailable
{
    public IObservable<int> Process()
    {
        torch.InitializeDeviceType(DeviceType.CUDA);
        var cudaIsAvailable = torch.cuda.is_available();
        Console.WriteLine("CUDA is available: " + cudaIsAvailable);
        var cudnnIsAvailable = torch.cuda.is_cudnn_available();
        Console.WriteLine("CuDNN is available: " + cudnnIsAvailable);
        return Observable.Return(1);
    }
}
