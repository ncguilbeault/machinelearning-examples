# Custom PyTorch Model Example

In this example, we demonstrate how to create a simple PyTorch model in Python, train it on the MNIST dataset, and then use Bonsai to load the model and run inference online.

## Python Guide

This example works best with the `uv` Python environment manager.

### Train the model

Open up a terminal and run the following commands:

```bash
cd examples/Torch/CustomPyTorchModel
uv run train.py
```

The script works as follows. First, it will download the MNIST dataset into the `datasets` folder at the root of the `examples` directory. Next, it creates a custom pytorch model, defined in the `model.py` file. This model is a simple convolutional neural network (CNN) with fully connected layers for classifying digits from the MNIST dataset. The script uses a standard training procedure by using the negative log likelihood to update the model's weights. After training the model, the script will save the best model checkpoint to a file named `models/checkpoints/best_model.pt`.

### Export to Torchscript

At this point, the model should be trained for a sufficient number of iterations and the weights of the best model will be saved in `models/checkpoints/best_model.pt`. Next, you will need to export the model to a format that Bonsai.ML.Torch can use, namely a torchscript model. To do this, run the following:

```bash
uv run generate_torchscript.py
```

This will take the best model and export it to a torch script file located at `models/model.torchscript`. At this point, the model can be used in Bonsai.

### Workflow

The workflow for this example looks like this:

:::workflow
![Custom PyTorch Model](RunCustomPyTorchModel.bonsai)
:::

The workflow loads the MNIST dataset from disk, as well as the custom PyTorch model from the file located at `models/model.torchscript`. Individual pairs of images and labels are enumerated over the dataset and the images are processed by normalizing them. The normalized image is then passed through the model's forward method, giving the log probabilities for each label given the image. The `argmax` of the output is then used to determine the predicted label for each image. The results are then displayed in the `MnistClassification` visualizer.
