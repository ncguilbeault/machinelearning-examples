using Bonsai;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using TorchSharp;

[Combinator]
[WorkflowElementCategory(ElementCategory.Transform)]
[Description("Reinforcement Learning Agent")]
public class GetQValueParameters
{
    [TypeConverter(typeof(AgentNameConverter))]
    public string Agent { get; set; }

    // Observable process function
    public IObservable<T> Process<T>(IObservable<T> source)
    {
        return source.Do(state => {
            var agent = RLAgentManager.GetAgent(Agent);
            var qValues = agent.QValueInstance;
            var parameters = qValues.parameters();

            foreach (var p in parameters)
            {
                Console.WriteLine(p);
            }
        });
    }
}