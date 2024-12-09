using TorchSharp;
using Bonsai;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

[Combinator]
[WorkflowElementCategory(ElementCategory.Source)]
[Description("Creates a new Reinforcement Learning Agent")]
public class CreateRLAgent
{
    private string name = "RLAgent";
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    // Initialize random number generator
    private int seed = 0;

    // Seed for random number generator
    public int Seed
    {
        get
        {
            return seed;
        }
        set
        {
            seed = value;
        }
    }

    // Initialize replay memory buffer size
    private int replayMemoryBufferSize = 100;
    public int ReplayMemoryBufferSize
    {
        get
        {
            return replayMemoryBufferSize;
        }
        set
        {
            if (value > 0)
                replayMemoryBufferSize = value;
            else
                replayMemoryBufferSize = 1;
        }
    }

    // Discount factor for Q-learning
    private double gamma = 0.9;
    public double Gamma
    {
        get
        {
            return gamma;
        }
        set
        {
            gamma = value;
        }
    }

    // Starting epsilon for epsilon-greedy policy
    private double epsilonStart = 1.0;
    public double EpsilonStart
    {
        get
        {
            return epsilonStart;
        }
        set
        {
            epsilonStart = value;
        }
    }

    // Ending epsilon for epsilon-greedy policy
    private double epsilonEnd = 0.1;
    public double EpsilonEnd
    {
        get
        {
            return epsilonEnd;
        }
        set
        {
            epsilonEnd = value;
        }
    }

    // Decay rate for epsilon-greedy policy
    private double epsilonDecay = 1e-3;
    public double EpsilonDecay
    {
        get
        {
            return epsilonDecay;
        }
        set
        {
            epsilonDecay = value;
        }
    }

    // Action space size
    private int actionSpace = 4;
    public int ActionSpace
    {
        get
        {
            return actionSpace;
        }
        set
        {
            actionSpace = value;
        }
    }

    // Mini batch size for experience replay
    private int batchSize = 32;
    public int BatchSize
    {
        get
        {
            return batchSize;
        }
        set
        {
            batchSize = value;
        }
    }

    // Learning rate for Q-learning
    private double learningRate = 0.01;
    public double LearningRate
    {
        get
        {
            return learningRate;
        }
        set
        {
            learningRate = value;
        }
    }
    public IObservable<RLAgent> Process()
    {
        // return Observable.Return(new RLAgentDisposable()
        // {
        //     Seed = seed,
        //     ReplayMemoryBufferSize = replayMemoryBufferSize,
        //     Gamma = gamma,
        //     EpsilonStart = epsilonStart,
        //     EpsilonEnd = epsilonEnd,
        //     EpsilonDecay = epsilonDecay,
        //     ActionSpace = actionSpace,
        //     BatchSize = batchSize,
        //     LearningRate = learningRate
        // });
        return Observable.Using(
            () => RLAgentManager.Reserve(Name, seed, replayMemoryBufferSize, gamma, epsilonStart, epsilonEnd, epsilonDecay, actionSpace, batchSize, learningRate),
            resource => Observable.Return(resource.Agent)
                                    .Concat(Observable.Never(resource.Agent)));
    }
}