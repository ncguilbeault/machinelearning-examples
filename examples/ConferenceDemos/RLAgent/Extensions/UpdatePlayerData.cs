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
[WorkflowElementCategory(ElementCategory.Transform)]
public class UpdatePlayerData
{
    [XmlIgnore]
    public PlayerData PlayerData { get; set; }
    public IObservable<PlayerData> Process(IObservable<Point> source)
    {
        return source.Select(value => {
            PlayerData.Update(value);
            return PlayerData;
        });
    }
}