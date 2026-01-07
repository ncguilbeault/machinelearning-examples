# Training an RNN to approximate a non-linear sinusoidal function online

The following example shows how to use Bonsai.ML.Torch to build custom models and training procedures directly in Bonsai. In this example, we will build a special type of model, called a recurrent neural network (RNN), which takes observations and the history of the network state to approximate a sinusoid function over time.

## Model Specification

The workflow simulates data using the following:

$$
y = \sin(x), x \in [0, 2\pi)
$$

We use an RNN to model this process such that at each moment, the model uses the current observation $x_t$, and the model's previous hidden state $h_{t-1}$ to infer what is the next state. The recurrent neural network uses the following equation to compute the next hidden state $h_t$:

$$
h_t=\tanh(x_t W_{ih}^T + b_{ih} + h_{t-1} W_{hh}^T + b_{hh})
$$

We then use a fully connected layer to take the model's hidden state and extract our final estimate of the output $\hat{y}_t$:

$$
\hat{y} = h_t W^T + b
$$

When we train the model, we first run inference over a batch of samples, where for each sample, the model observes it's previous hidden state $h_{t-1}$ and the observation $x_t$. When the batch of samples arrives, we calculate the mean squared error (MSE) of the batch of data by taking the difference between the model's output $\hat{y}_t$ and the target variable $y_t$, summed over the batch of data of size $N$:

$$
L = \sum_{i=1}^N\ell_i, \ell_i = |y_i - \hat{y}_i|^2
$$

We then use the Adam optimizer to train the weights of our model, $W_{ih}$, $W_{hh}$, $W$, as well as the bias terms $b_{ih}$, $b_{hh}$, and $b$.

### Workflow

Below is the example workflow.

:::workflow
![Train RNN](TrainRNN.bonsai)
:::

The workflow can be broken down into the following sections.

1. `SimulatedData` - Uses a `Timer`, scaled by a factor of 10, to generate new data points $x$ at each moment in time $t$. These values are normalized to fall within the range $[0, 2\pi)$ and subsequently fed through our target function $f(x_t) = sin(x_t)$.
2. `ModelDefinition` - This is where we create and specify our model. We create a recurrent neural network module, parameterized by the number of hidden units in the network and the number of layers. We use the same number of hidden units to parameterize our final linear readout layer.
3. `TrainingSpecification` - Contains our definitions of the training procedure, including the criterion we are going to use, the optimizer, and scheduler. Our loss function will compute the mean squared error, and we use the Adam optimizer to back propagate our error through the network.
4. `TrainingProcedure` - The training procedure takes our input variable $x_t$ and runs inference using the current input and the previous hidden state $h_{t-1}$. We then compute the mean squared error of our prediction $\hat{y}$ with the target variable $y$ and accumulate the loss over successive inputs. Once we reach the desired number of steps or batch size, we compute our gradients and back propagate the error through the network. Since we are recursively feeding the hidden state at each time point, it's important that we detach the hidden state from the computational graph that torch maintains in order to compute the gradients before we set our gradients to 0. Finally, we reset our accumulated loss and hidden state back to their initial values.
5. `Visualizer` - Displays the target value, $y$, alongside our model's prediction $\hat{y}$. It also displays the total loss that was incurred for each batch.
