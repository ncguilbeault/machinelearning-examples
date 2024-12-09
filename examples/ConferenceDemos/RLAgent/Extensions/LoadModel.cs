using TorchSharp;
using Bonsai;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[WorkflowElementCategory(ElementCategory.Source)]
[Description("Reinforcement Learning Agent")]
public class LoadModel
{
    public string ModelBasePath { get; set; }

    // Observable process function
    public IObservable<RLAgent> Process(IObservable<RLAgent> source)
    {
        return source.Do(agent =>
        {
            agent.Load(ModelBasePath);
        });
    }
}