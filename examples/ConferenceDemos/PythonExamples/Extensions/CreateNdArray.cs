using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Source)]
public class CreateNdArray
{
    public IObservable<int[,]> Process()
    {
        return Observable.Return(new int[,] { { 1, 2, 3 }, { 4, 5, 6 } });
    }
}
