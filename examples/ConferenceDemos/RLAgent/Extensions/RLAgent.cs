using TorchSharp;
using Bonsai;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using TorchSharp.Modules;

public class RLAgent
{
    // Initialize agent name
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
    private Random random = new Random(0);

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
            random = new Random(seed);
        }
    }

    // Initialize replay memory buffer size
    private int replayMemoryBufferSize = 1000;
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

    private double epsilon;
    public double Epsilon
    {
        get
        {
            return epsilon;
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

    // Initialize action value function Q with random weights
    private SimplifiedConvNet QValue;
    public SimplifiedConvNet QValueInstance
    {
        get
        {
            return QValue;
        }
    }

    // Initialize target action value function Q' with random weights
    private SimplifiedConvNet QTarget;
    public SimplifiedConvNet QTargetInstance
    {
        get
        {
            return QTarget;
        }
    }

    // Define replay memory buffer
    private ReplayMemory replayMemory;

    private Experience previousExperience = null;

    private torch.optim.Optimizer optimizer;
    private torch.nn.Module<torch.Tensor, torch.Tensor, torch.Tensor> criterion;
    private int episode = 0;
    private int steps = 0;
    private torch.optim.lr_scheduler.LRScheduler scheduler;
    private torch.Device device;


    public RLAgent(torch.Device device = null)
    {
        if (device == null)
        {
            device = new torch.Device(DeviceType.CPU);
        }
        this.device = device;

        QValue = new SimplifiedConvNet("QValue", actionSpace).to(device);
        QValue.train();
        QTarget = new SimplifiedConvNet("QTarget", actionSpace).to(device);
        QTarget.train();
        QTarget.load_state_dict(QValue.state_dict());
        replayMemory = new ReplayMemory()
        {
            Size = replayMemoryBufferSize
        };
        epsilon = epsilonStart;
        optimizer = new AdamW(QValue.parameters(), lr: learningRate, amsgrad: true);
        criterion = torch.nn.MSELoss();
        scheduler = torch.optim.lr_scheduler.StepLR(optimizer, 1, 0.1);
        // scheduler = new torch.optim.lr_scheduler.impl.CyclicLR(optimizer, learningRate * 0.5f, learningRate * 2f, step_size_up: 500, step_size_down: 2000, cycle_momentum: false);
    }
    
    // Update target action value function Q' with Q
    public void UpdateQTarget(bool hardUpdate = false, double tau = 0.001)
    {
        if (hardUpdate)
        {
            QTarget.load_state_dict(QValue.state_dict());
            return;
        }

        using (torch.no_grad())
        {
            var targetParams = QTarget.state_dict();
            var qParams = QValue.state_dict();
            for (int i = 0; i < targetParams.Count; i++)
            {
                var keyValuePair = targetParams.ElementAt(i);
                var key = keyValuePair.Key;
                var targetNetState = targetParams[key];
                var policyNetState = qParams[key];
                targetParams[key] = policyNetState * tau + targetNetState * (1 - tau);
            }
            QTarget.load_state_dict(targetParams);
        }
    }

    // Update action value function Q with experience replay
    public float UpdateQValue()
    {
        if (replayMemory.Count < batchSize)
        {
            return 0.0F;
        }

        // Sample batch of experiences from replay memory buffer
        var experiences = replayMemory.Sample(batchSize);

        // Initialize lists for batch processing
        var statesList = new List<torch.Tensor>();
        var actionsList = new List<torch.Tensor>();
        var rewardsList = new List<torch.Tensor>();
        var nextStatesList = new List<torch.Tensor>();
        var dones = new List<torch.Tensor>();

        // Process experiences
        foreach (var experience in experiences)
        {
            statesList.Add(experience.state);
            actionsList.Add(torch.tensor(new long[] { experience.action }, device: device));
            rewardsList.Add(torch.tensor(new double[] { experience.reward }, dtype: torch.float32, device: device));
            nextStatesList.Add(experience.nextState);
            dones.Add(torch.tensor(new double[] { experience.done ? 1 : 0 }, dtype: torch.int16, device: device));
        }

        var states = torch.cat(statesList.ToArray());
        var actions = torch.cat(actionsList.ToArray()).unsqueeze(1);
        var rewards = torch.cat(rewardsList.ToArray());
        var nextStates = torch.cat(nextStatesList.ToArray());
        var donesTensor = torch.cat(dones.ToArray());

        // var nonFinalMask = donesTensor.eq(0);
        // Console.WriteLine("here1: " + nonFinalMask);
        // var nonFinalIndices = nonFinalMask.nonzero().squeeze();
        // Console.WriteLine("here2: " + nonFinalIndices);
        // var nonFinalNextStates = nextStates.index_select(0, nonFinalIndices);
        // Console.WriteLine("here3: " + nonFinalNextStates);

        // var predictedQValues = QValue.forward(states).gather(1, actions);
        // var nextStateValues = torch.zeros(batchSize, device: device);
        // Console.WriteLine("here4: " + nextStateValues);

        // using (torch.no_grad())
        // {
        //     var targetValues = QTarget.forward(nonFinalNextStates).max(1).Item1;
        //     nextStateValues.index_put_(nonFinalMask, targetValues);
        // }

        // var targets = rewards + (gamma * nextStateValues);
        // Console.WriteLine("here5: " + targets);

        // Use QValue network to evaluate the value of those actions
        var qValues = QValue.forward(states);
        var predictedQValues = qValues.gather(1, actions);

        // Use QValue network to select the best action
        var nextActionsMasked = QValue.forward(nextStates).argmax(1).unsqueeze(1);
        
        // Use QTarget network to evaluate the value of those actions
        var qValuesNext = QTarget.forward(nextStates);
        var maxQNext = qValuesNext.gather(1, nextActionsMasked).squeeze();

        var targets = rewards + gamma * maxQNext * (1 - donesTensor);

        optimizer.zero_grad();
        var loss = criterion.forward(predictedQValues, targets.unsqueeze(1));
        loss.backward();
        torch.nn.utils.clip_grad_value_(QValue.parameters(), 100);
        optimizer.step();

        return loss.to(DeviceType.CPU).ReadCpuSingle(0);
    }

    // Function to calculate priority for prioritized experience replay
    public double CalculatePriority(torch.Tensor state, int action, double reward, torch.Tensor nextState, bool done)
    {
        using (torch.no_grad())
        {
            if (nextState.numel() == 0)
                return torch.abs(reward).to(DeviceType.CPU).ReadCpuSingle(0);
            var qValues = QValue.forward(state).to(DeviceType.CPU);
            var qValuesNext = QTarget.forward(nextState).to(DeviceType.CPU);
            var predictedQValue = qValues[0, action];
            var maxQNext = qValuesNext.max(1).Item1;
            var target = reward + gamma * maxQNext * (1 - (done ? 1 : 0));
            var error = target - predictedQValue;
            return torch.abs(error).to(DeviceType.CPU).ReadCpuSingle(0);
        }
    }

    // Function to select action based on epsilon-greedy policy
    public int SelectAction(torch.Tensor state)
    {
        // epsilon = Math.Max(epsilonEnd, epsilon - epsilonDecay);
        epsilon = epsilonEnd + (epsilonStart - epsilonEnd) * Math.Exp(-steps/epsilonDecay);
        steps++;
        if (random.NextDouble() < epsilon)
        {
            return random.Next(actionSpace);
        }
        else
        {
            using (torch.no_grad())
            {
                var qValues = QValue.forward(state);

                return (int)qValues.argmax().to(DeviceType.CPU).ReadCpuInt64(0);
            }
        }
    }

    public void EndEpisode()
    {
        // previousState = torch.empty(0);
        if (previousExperience != null)
        {
            // var priority = CalculatePriority(previousExperience.state, previousExperience.action, previousExperience.reward, previousExperience.state, previousExperience.done);
            // previousExperience.priority = priority;
            replayMemory.Add(previousExperience);
        }
        scheduler.step();
        previousExperience = null;
        episode++;
    }

    public void UpdateReplayMemory(torch.Tensor state, int action, double reward, bool done)
    {
        if (previousExperience != null)
        {
            var priority = CalculatePriority(previousExperience.state, previousExperience.action, previousExperience.reward, state, previousExperience.done);
            // var priority = 1.0;
            // Add experience to replay memory buffer
            replayMemory.Add(new Experience(previousExperience.state, previousExperience.action, previousExperience.reward, state, previousExperience.done, priority));
        }
        previousExperience = new Experience(state, action, reward, state, done, 0);
    }

    public void Save(string basePath)
    {
        // Check if base path is relative path
        if (!System.IO.Path.IsPathRooted(basePath))
        {
            basePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, basePath);
        }

        // Create directory if it does not exist
        if (!System.IO.Directory.Exists(basePath))
        {
            System.IO.Directory.CreateDirectory(basePath);
        }

        QValue.save(System.IO.Path.Combine(basePath, "QValue.bin"));
        QTarget.save(System.IO.Path.Combine(basePath, "QTarget.bin"));
    }

    public void Load(string basePath)
    {
        // Check if base path is relative path
        if (!System.IO.Path.IsPathRooted(basePath))
        {
            basePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, basePath);
        }

        QValue.load(System.IO.Path.Combine(basePath, "QValue.bin"));
        QTarget.load(System.IO.Path.Combine(basePath, "QTarget.bin"));
    }

    public void Dispose()
    {
        QValue.Dispose();
        QTarget.Dispose();
    }
}