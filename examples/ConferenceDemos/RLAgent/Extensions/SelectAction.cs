using TorchSharp;
using Bonsai;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[WorkflowElementCategory(ElementCategory.Transform)]
[Description("Reinforcement Learning Agent")]
public class SelectAction
{
    [TypeConverter(typeof(AgentNameConverter))]
    public string Agent { get; set; }

    // Observable process function
    public IObservable<int> Process(IObservable<torch.Tensor> source)
    {
        return source.Select(state => {
            var agent = RLAgentManager.GetAgent(Agent);
            return agent.SelectAction(state);
        });
    }
}