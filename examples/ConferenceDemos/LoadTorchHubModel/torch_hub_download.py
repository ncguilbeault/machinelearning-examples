# import torch

# model_type = "MiDaS_small"
# model = torch.hub.load("intel-isl/MiDaS", model_type)
# model.eval()

# example_inputs = {"forward": torch.rand(1, 3, 384, 384)}
# model_traced = torch.jit.trace_module(model, example_inputs)

# torch.jit.save(model_traced, "MiDaS.pt")

# import torch

# # load model
# model = torch.hub.load('hustvl/yolop', 'yolop', pretrained=True)
# img = torch.randn(1,3,640,640)
# model.eval()
# model_traced = torch.jit.trace(model, img)
# torch.jit.save(model_traced, "yolop.pt")

import torch

# Model
model = torch.hub.load('ultralytics/yolov5', 'yolov5s', pretrained=True)
model.eval()
img = torch.randn(1,3,640,640)
model_traced = torch.jit.trace(model, img)
torch.jit.save(model_traced, "yolov5s.pt")