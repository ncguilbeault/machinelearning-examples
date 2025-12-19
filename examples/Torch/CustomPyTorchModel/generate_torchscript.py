import os
import sys
import torch

from model import CustomPyTorchModel


if __name__ == '__main__':

    # Run on GPU if available
    if torch.cuda.is_available():
        device = torch.device("cuda")
    else:
        device = torch.device("cpu")

    # Initialize the model
    model = CustomPyTorchModel().to(device)

    # Check if model checkpoints directory exists
    if not os.path.exists(os.path.join("models", "checkpoints")):
        print("Model checkpoints directory not found. Please train the model first.")
        sys.exit(1)

    # Load the best model checkpoint
    model.load_state_dict(torch.load(os.path.join("models", "checkpoints", "custom_pytorch_model_best.pt")))

    # Save the final trained model using torch.jit.script
    example_input = torch.rand(1, 1, 28, 28).to(device)
    with torch.no_grad():
        model.eval()
        scripted_model = torch.jit.script(model)
        scripted_model.save(os.path.join("models", "custom_pytorch_model.torchscript"))