using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ResetPlayerPosition
{
    private int _maxX = 10;
    public int MaxX 
    { 
        get
        {
            return _maxX;
        }
        set
        {
            _maxX = value; 
        }
    }

    private int _maxY = 10;
    public int MaxY
    {
        get
        {
            return _maxY;
        }
        set
        {
            _maxY = value;
        }
    }

    private int _minX = -10;
    public int MinX
    {
        get
        {
            return _minX;
        }
        set
        {
            _minX = value;
        }
    }

    private int _minY = -10;
    public int MinY 
    {
        get
        {
            return _minY;
        }
        set
        {
            _minY = value;
        }
    }
    
    public IObservable<Point> Process(IObservable<Point> source)
    {
        return source.Select(value => {
            var output = value;
            if (output.X > _maxX)
            {
                output.X -= _maxX * 2 + output.X - _maxX;
            }
            if (output.X < _minX)
            {
                output.X -= _minX * 2 + output.X - _minX;
            }
            if (output.Y > _maxY)
            {
                output.Y -= _maxY * 2 + output.Y - _maxY;
            }
            if (output.Y < _minY)
            {
                output.Y -= _minY * 2 + output.Y - _minY;
            }
            return output;
        });
    }
}
