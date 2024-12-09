using TorchSharp;

public class Experience
{
    public torch.Tensor state;
    public int action;
    public double reward;
    public torch.Tensor nextState;
    public bool done;
    public double priority;

    public Experience(torch.Tensor state, int action, double reward, torch.Tensor nextState, bool done, double priority)
    {
        this.state = state;
        this.action = action;
        this.reward = reward;
        this.nextState = nextState;
        this.done = done;
        this.priority = priority;
    }
}
