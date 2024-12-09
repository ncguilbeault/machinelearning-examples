using TorchSharp;
using Bonsai;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[WorkflowElementCategory(ElementCategory.Sink)]
[Description("Updates the replay memory")]
public class UpdateReplayMemory
{
    [TypeConverter(typeof(AgentNameConverter))]
    public string Agent { get; set; }

    // Observable process function
    public IObservable<Tuple<torch.Tensor, int, double, bool>> Process(IObservable<Tuple<torch.Tensor, int, double, bool>> source)
    {
        return source.Do(environment => {
            var state = environment.Item1;
            var action = environment.Item2;
            var reward = environment.Item3;
            var done = environment.Item4;
            var agent = RLAgentManager.GetAgent(Agent);
            agent.UpdateReplayMemory(state, action, reward, done);
        });
    }
}