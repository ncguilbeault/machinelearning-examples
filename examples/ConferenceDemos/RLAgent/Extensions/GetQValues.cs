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
public class GetQValues
{
    [TypeConverter(typeof(AgentNameConverter))]
    public string Agent { get; set; }

    private torch.nn.Module<torch.Tensor, torch.Tensor> softmax = torch.nn.Softmax(1);

    // Observable process function
    public IObservable<torch.Tensor> Process(IObservable<torch.Tensor> source)
    {
        return source.Select(state => {
            var agent = RLAgentManager.GetAgent(Agent);
            using (torch.no_grad())
            {
                var qValues = agent.QValueInstance;
                return softmax.forward(qValues.forward(state));
            }
        });
    }
}