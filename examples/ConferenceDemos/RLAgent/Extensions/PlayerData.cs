using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using System.Collections.Generic;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class PlayerData
{
    private int _score = 0;
    public int Score 
    { 
        get 
        {
            return _score;
        }
    }

    private List<Point> _positions;
    public List<Point> Positions
    {
        get
        {
            return _positions;
        }
    }

    private List<Point> _previousPositions = new List<Point>();
    public List<Point> PreviousPositions
    {
        get
        {
            return _previousPositions;
        }
    }
    private bool _addPosition;
    public PlayerData()
    {
    }    
    public PlayerData(Point initialPosition)
    {
        _positions = new List<Point> { initialPosition };
    }
    public void Reward()
    {
        _score += 1;
        _addPosition = true;
    }
    public void Update(Point newPosition)
    {
        if (!_addPosition)
            _positions.RemoveAt(0);
        _previousPositions = new List<Point>(_positions);
        _addPosition = false;
        _positions.Add(newPosition);
    }
    public IObservable<PlayerData> Process(IObservable<Point> source)
    {
        return source.Select(value => new PlayerData(value));
    }
}
