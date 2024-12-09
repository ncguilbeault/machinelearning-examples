using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using System.Collections.Generic;
using System.Xml.Serialization;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Sink)]
public class EndEpisode
{
    [TypeConverter(typeof(AgentNameConverter))]
    public string Agent { get; set; }
    public IObservable<T> Process<T>(IObservable<T> source)
    {
        return source.Do(input =>
        {
            var agent = RLAgentManager.GetAgent(Agent);
            agent.EndEpisode();
        });
    }
}