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
public class RewardPlayerData
{
    public IObservable<PlayerData> Process(IObservable<PlayerData> source)
    {
        return source.Do(value => value.Reward());
    }
}