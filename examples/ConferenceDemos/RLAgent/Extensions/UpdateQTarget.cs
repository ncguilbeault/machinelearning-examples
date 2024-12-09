using TorchSharp;
using Bonsai;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[WorkflowElementCategory(ElementCategory.Sink)]
[Description("Reinforcement Learning Agent")]
public class UpdateQTarget
{
    [TypeConverter(typeof(AgentNameConverter))]
    public string Agent { get; set; }

    // Observable process function
    public IObservable<T> Process<T>(IObservable<T> source)
    {
        return source.Do(_ => {
            var agent = RLAgentManager.GetAgent(Agent);
            agent.UpdateQTarget();
        });
    }
}