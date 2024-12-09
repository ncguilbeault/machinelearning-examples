using TorchSharp;

public class SimplifiedConvNet : torch.nn.Module<torch.Tensor, torch.Tensor>
{
    private torch.nn.Module<torch.Tensor, torch.Tensor> conv1;
    private torch.nn.Module<torch.Tensor, torch.Tensor> fc1;
    private torch.nn.Module<torch.Tensor, torch.Tensor> fc2;

    private torch.nn.Module<torch.Tensor, torch.Tensor> relu = torch.nn.ReLU();
    private torch.nn.Module<torch.Tensor, torch.Tensor> flatten = torch.nn.Flatten();

    public SimplifiedConvNet(string name, int outputSize, torch.Device device = null) : base(name)
    {
        conv1 = torch.nn.Conv2d(4, 16, 3, stride: 1, padding: 1); // One convolutional layer
        fc1 = torch.nn.Linear(16 * 23 * 23, 64); // Adjust input size based on grid size
        fc2 = torch.nn.Linear(64, outputSize);

        RegisterComponents();

        if (device != null && device.type != DeviceType.CPU)
            this.to(device);
    }

    public override torch.Tensor forward(torch.Tensor input)
    {
        var convOut = relu.forward(conv1.forward(input));
        var flatOut = flatten.forward(convOut);
        var fcOut1 = relu.forward(fc1.forward(flatOut));
        var fcOut2 = fc2.forward(fcOut1);
        return fcOut2;
    }
}