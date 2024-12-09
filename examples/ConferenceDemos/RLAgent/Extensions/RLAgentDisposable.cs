using System;
using System.Threading;

sealed class RLAgentDisposable : IDisposable
{
    IDisposable resource;

    public RLAgentDisposable(RLAgent agent, IDisposable disposable)
    {
        if (agent == null)
            throw new ArgumentNullException("agent");
        if (disposable == null)
            throw new ArgumentNullException("disposable");
        Agent = agent;
        resource = disposable;
    }

    public RLAgent Agent { get; private set; }

    public bool IsDispose
    {
        get { return resource == null; }
    }

    public void Dispose()
    {
        var disposable = Interlocked.Exchange(ref resource, null);
        if (disposable != null)
            disposable.Dispose();
    }
}