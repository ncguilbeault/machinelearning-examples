using System;
using TorchSharp;

public class ConvNet : torch.nn.Module<torch.Tensor,torch.Tensor>
{
    private torch.nn.Module<torch.Tensor, torch.Tensor> conv1;
    private torch.nn.Module<torch.Tensor, torch.Tensor> conv2;
    private torch.nn.Module<torch.Tensor, torch.Tensor> fc1;
    private torch.nn.Module<torch.Tensor, torch.Tensor> fc2;

    private torch.nn.Module<torch.Tensor, torch.Tensor> relu1 = torch.nn.ReLU();
    private torch.nn.Module<torch.Tensor, torch.Tensor> relu2 = torch.nn.ReLU();
    private torch.nn.Module<torch.Tensor, torch.Tensor> relu3 = torch.nn.ReLU();

    private torch.nn.Module<torch.Tensor, torch.Tensor> flatten = torch.nn.Flatten();

    private torch.nn.Module<torch.Tensor, torch.Tensor> dropout = torch.nn.Dropout(0.3);

    public ConvNet(string name, int outputSize, torch.Device device = null) : base(name)
    {
        conv1 = torch.nn.Conv2d(4, 32, 3, stride: 1);
        conv2 = torch.nn.Conv2d(32, 64, 4, stride: 2);
        fc1 = torch.nn.Linear(5184, 512);
        fc2 = torch.nn.Linear(512, outputSize);

        RegisterComponents();

        if (device != null && device.type != DeviceType.CPU)
            this.to(device);
    }

    public override torch.Tensor forward(torch.Tensor input)
    {
        var l11 = conv1.forward(input);
        var l12 = relu1.forward(l11);

        var l21 = conv2.forward(l12);
        var l22 = relu2.forward(l21);

        var l31 = flatten.forward(l22);

        var l41 = fc1.forward(l31);
        var l42 = relu3.forward(l41);

        var l51 = fc2.forward(l42);
        var l52 = dropout.forward(l51);

        return l52;
    }
}