using TorchSharp;
using Bonsai;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[WorkflowElementCategory(ElementCategory.Transform)]
[Description("Updates the Q value network")]
public class UpdateQValue
{
    [TypeConverter(typeof(AgentNameConverter))]
    public string Agent { get; set; }

    public IObservable<float> Process<T>(IObservable<T> source)
    {
        return source.Select(_ => {
            var agent = RLAgentManager.GetAgent(Agent);
            return agent.UpdateQValue();
        });
    }
}