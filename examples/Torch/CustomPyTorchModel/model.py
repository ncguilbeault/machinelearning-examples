import torch
import torch.nn as nn
import torch.nn.functional as F


class MnistClassifier(nn.Module):
    """
    A simple example of a custom PyTorch model.
    This model consists of two convolutional layers followed by two fully connected layers with dropout.
    It is designed to classify images from the MNIST dataset.
    """

    def __init__(self):
        """
        Initialize the model layers.
        """

        super(MnistClassifier, self).__init__()
        self.conv1 = nn.Conv2d(1, 32, 3, 1)
        self.conv2 = nn.Conv2d(32, 64, 3, 1)
        self.dropout1 = nn.Dropout(0.25)
        self.dropout2 = nn.Dropout(0.5)
        self.fc1 = nn.Linear(9216, 128)
        self.fc2 = nn.Linear(128, 10)

    def forward(self, x: torch.Tensor) -> torch.Tensor:
        """
        Forward pass of the model.

        Args
        ----
        x: torch.Tensor
            The input tensor of shape (batch_size, 1, 28, 28).

        Returns
        -------
        prediction: torch.Tensor
            The log probabilities of each class (batch_size, 10).
        """

        x = self.conv1(x)
        x = F.relu(x)
        x = self.conv2(x)
        x = F.relu(x)
        x = F.max_pool2d(x, 2)
        x = self.dropout1(x)
        x = torch.flatten(x, 1)
        x = self.fc1(x)
        x = F.relu(x)
        x = self.dropout2(x)
        x = self.fc2(x)
        output = F.log_softmax(x, dim=1)
        return output
