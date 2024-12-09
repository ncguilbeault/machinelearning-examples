using Bonsai;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using TorchSharp;

[Combinator]
[WorkflowElementCategory(ElementCategory.Source)]
[Description("Reinforcement Learning Agent")]
public class GetRLAgent
{
    [TypeConverter(typeof(AgentNameConverter))]
    public string Agent { get; set; }

    // Observable process function
    public IObservable<RLAgent> Process()
    {
        return Observable.Return(RLAgentManager.GetAgent(Agent));
    }

    public IObservable<RLAgent> Process<T>(IObservable<T> source)
    {
        return source.Select(_ => RLAgentManager.GetAgent(Agent));
    }
}