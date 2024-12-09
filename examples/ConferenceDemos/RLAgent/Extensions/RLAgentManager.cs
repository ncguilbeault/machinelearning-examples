using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

internal static class RLAgentManager
{
    private static Dictionary<string, RLAgent> agents = new Dictionary<string, RLAgent>();

    internal static RLAgent GetAgent(string name)
    {
        if (agents.ContainsKey(name))
        {
            return agents[name];
        }
        else
        {
            throw new InvalidOperationException("Agent not found");
        }
    }

    internal static RLAgentDisposable Reserve(
        string name, 
        int seed, 
        int replayMemoryBufferSize, 
        double gamma, 
        double epsilonStart,
        double epsilonEnd,
        double epsilonDecay,
        int actionSpace,
        int batchSize,
        double learningRate)
    {
        var agent = new RLAgent()
        {
            Name = name,
            Seed = seed,
            ReplayMemoryBufferSize = replayMemoryBufferSize,
            Gamma = gamma,
            EpsilonStart = epsilonStart,
            EpsilonEnd = epsilonEnd,
            EpsilonDecay = epsilonDecay,
            ActionSpace = actionSpace,
            BatchSize = batchSize,
            LearningRate = learningRate
        };

        var dispose = Disposable.Create(() =>
        {
            agent.Dispose();
            agents.Remove(name);
        });

        agents.Add(name, agent);
        
        return new RLAgentDisposable(agent, dispose);
    }
}