import torch
import os

model_type = "yolov5s"
model = torch.hub.load("ultralytics/yolov5", model_type, pretrained=True)
model.eval()

example_input = torch.rand(1, 3, 640, 640)

os.makedirs("models", exist_ok=True)

with torch.no_grad():
    o1 = model(example_input)
    scripted_model = torch.jit.trace(model, example_input)
    torch.jit.save(scripted_model, f"models/{model_type}.pt")