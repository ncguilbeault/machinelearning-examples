using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class PrintByteArray
{
    public IObservable<byte[,]> Process(IObservable<byte[,]> source)
    {
        return source.Select(value => {
            int n = 0;
            for (int i = 0; i < value.GetUpperBound(0); i++)
            {
                for (int j = 0; j < value.GetUpperBound(1); j++)
                {
                    Console.WriteLine(string.Format("Byte[{0}]: {1}", n++, value[i,j]));
                    if (n > 15) break;
                }
            }
            return value;
        });
    }
}
