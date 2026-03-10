import os
import torch
import torch.nn as nn
import torch.nn.functional as F
import torch.optim as optim
from torchvision import datasets, transforms
from torch.optim.lr_scheduler import StepLR
from model import MnistClassifier

if __name__ == "__main__":

    # Run on GPU if available
    if torch.cuda.is_available():
        device = torch.device("cuda")
    else:
        device = torch.device("cpu")

    # Set random seed for reproducibility
    torch.manual_seed(0)

    # These transformations will be applied to the images
    transform = transforms.Compose(
        [transforms.ToTensor(), transforms.Normalize((0.1307,), (0.3081,))]
    )

    # Download and load the MNIST dataset
    train_dataset = datasets.MNIST(
        "../../../datasets", train=True, download=True, transform=transform
    )
    test_dataset = datasets.MNIST(
        "../../../datasets", train=False, download=True, transform=transform
    )

    train_loader = torch.utils.data.DataLoader(train_dataset)
    test_loader = torch.utils.data.DataLoader(test_dataset)

    # Initialize the model, optimizer, and scheduler
    model = MnistClassifier().to(device)
    optimizer = optim.Adadelta(model.parameters(), lr=1)
    scheduler = StepLR(optimizer, step_size=1, gamma=0.7)

    # Create directory to save the model
    os.makedirs("models", exist_ok=True)
    os.makedirs(os.path.join("models", "checkpoints"), exist_ok=True)

    # Training loop
    model.train()
    best_loss = float("inf")

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
                print(
                    "Train Epoch: {} [{}/{} ({:.0f}%)]\tLoss: {:.6f}".format(
                        epoch,
                        batch_idx * len(data),
                        len(train_loader.dataset),
                        100.0 * batch_idx / len(train_loader),
                        loss.item(),
                    )
                )

        scheduler.step()
        if (
            not os.path.exists(os.path.join("models", "checkpoints", "best_model.pt"))
            or total_loss < best_loss
        ):
            best_loss = total_loss
            torch.save(
                model.state_dict(),
                os.path.join("models", "checkpoints", "best_model.pt"),
            )
