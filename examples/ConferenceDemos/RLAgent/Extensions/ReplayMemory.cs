using System.Collections.Generic;
using System;
using System.Collections;

public class ReplayMemory : IEnumerable<Experience>
{
    private int seed = 0;
    private Random random = new Random(0);

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

    private int size = 32;
    public int Size
    {
        get
        {
            return size;
        }
        set
        {
            if (value > 0)
                size = value;
            else
                size = 1;
        }
    }

    public int Count
    {
        get
        {
            return buffer.Count;
        }
    }

    public ReplayMemory()
    {
    }

    // Initialize replay memory buffer
    private List<Experience> buffer = new List<Experience>();

    // Add experience to replay memory buffer
    public void Add(Experience experience)
    {
        while (buffer.Count >= size)
        {
            buffer.RemoveAt(0);
        }
        buffer.Add(experience);
    }

    // Sample batch of experiences from replay memory buffer
    public List<Experience> Sample(int batchSize)
    {
        var batch = new List<Experience>();
        for (int i = 0; i < batchSize; i++)
        {
            var index = random.Next(buffer.Count);
            batch.Add(buffer[index]);
        }
        return batch;
    }

    // Sample batch of experiences from replay memory buffer with priority
    public List<Experience> SampleWithPriority(int batchSize, bool withReplacement = false)
    {
        if (batchSize > buffer.Count)
        {
            throw new ArgumentException("Batch size cannot be larger than the buffer size.");
        }

        // Create a temporary list for sampling to avoid modifying the original buffer
        var tempBuffer = new List<Experience>(buffer);

        var batch = new List<Experience>();

        for (int i = 0; i < batchSize; i++)
        {
            // Compute sum of priorities in the temporary buffer
            double sum = 0;
            foreach (var experience in tempBuffer)
            {
                sum += experience.priority;
            }

            // Compute cumulative distribution
            var distribution = new List<double>();
            var cumulativeSum = 0.0;
            foreach (var experience in tempBuffer)
            {
                cumulativeSum += experience.priority / sum;
                distribution.Add(cumulativeSum);
            }

            // Perform sampling
            var sample = random.NextDouble();
            var index = distribution.BinarySearch(sample);
            if (index < 0)
            {
                index = ~index;
            }

            // Add the sampled experience to the batch
            batch.Add(tempBuffer[index]);

            // Remove the selected experience from the temporary buffer
            if (!withReplacement)
                tempBuffer.RemoveAt(index);
        }

        return batch;
    }

    public IEnumerator<Experience> GetEnumerator()
    {
        return buffer.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return buffer.GetEnumerator();
    }
}