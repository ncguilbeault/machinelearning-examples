import os
import torch
import torch.nn as nn
import torch.nn.functional as F
import torch.optim as optim
from torchvision import datasets, transforms
from torch.optim.lr_scheduler import StepLR


class CustomPyTorchModel(nn.Module):
    """
    A simple example of a custom PyTorch model.
    This model consists of two convolutional layers followed by two fully connected layers.
    It is designed to work with the MNIST dataset.
    """
    def __init__(self):

        '''
        Initialize the model layers.
        '''

        super(CustomPyTorchModel, self).__init__()
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

if __name__ == '__main__':

    # Run on GPU if available
    if torch.cuda.is_available():
        device = torch.device("cuda")
    else:
        device = torch.device("cpu")

    # Set random seed for reproducibility
    torch.manual_seed(42)

    # These transformations will be applied to the images
    transform = transforms.Compose([
        transforms.ToTensor(),
        transforms.Normalize((0.1307,), (0.3081,))
    ])

    # Download and load the MNIST dataset
    dataset1 = datasets.MNIST('../../../datasets', train=True, download=True,
                       transform=transform)
    dataset2 = datasets.MNIST('../../../datasets', train=False,
                       transform=transform)
    
    train_loader = torch.utils.data.DataLoader(dataset1)
    test_loader = torch.utils.data.DataLoader(dataset2)

    # Initialize the model, optimizer, and scheduler
    model = CustomPyTorchModel().to(device)
    optimizer = optim.Adadelta(model.parameters(), lr=1)
    scheduler = StepLR(optimizer, step_size=1, gamma=0.7)

    # Create directory to save the model
    os.makedirs("models", exist_ok=True)
    os.makedirs(os.path.join("models", "checkpoints"), exist_ok=True)

    # Training loop
    model.train()
    best_loss = float('inf')

    for epoch in range(1, 21):
        total_loss = 0
        # Train the model
        for batch_idx, (data, target) in enumerate(train_loader):
            data, target = data.to(device), target.to(device)
            optimizer.zero_grad()
            output = model(data)
            loss = F.nll_loss(output, target)
            total_loss += loss.item()
            loss.backward()
            optimizer.step()
            if batch_idx % 10 == 0:
                print('Train Epoch: {} [{}/{} ({:.0f}%)]\tLoss: {:.6f}'.format(
                    epoch, batch_idx * len(data), len(train_loader.dataset),
                    100. * batch_idx / len(train_loader), loss.item()))
                        
        scheduler.step()
        if not os.path.exists(os.path.join("models", "checkpoints", "custom_pytorch_model_best.pt")) or total_loss < best_loss:
            best_loss = total_loss
            torch.save(model.state_dict(), os.path.join("models", "checkpoints", "custom_pytorch_model_best.pt"))