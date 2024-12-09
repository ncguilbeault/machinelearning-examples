using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using TorchSharp;
using OpenCV.Net;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class CreateBoundingBoxes
{
    private double confidenceThreshold = 0.5;
    public double ConfidenceThreshold 
    {
        get { return confidenceThreshold; }
        set { confidenceThreshold = value; }
    }
    private double iouThreshold = 0.5;
    public double IoUThreshold 
    {
        get { return iouThreshold; }
        set { iouThreshold = value; }
    }
    public IObservable<BoundingBoxCollection> Process(IObservable<torch.Tensor> source)
    {
        return source.Select(value => {
            var valueTensor = value[0];
            var boxes = valueTensor[torch.TensorIndex.Colon, torch.TensorIndex.Slice(0, 4)];
            var confidence = valueTensor[torch.TensorIndex.Colon, 4];
            var classScores = valueTensor[torch.TensorIndex.Colon, torch.TensorIndex.Slice(5)];
            var finalScores = confidence.unsqueeze(1) * classScores;
            var mask = confidence > confidenceThreshold;
            var filteredBoxes = boxes[mask];
            var filteredScores = finalScores[mask];
            var predictedClasses = filteredScores.argmax(1);
            
            var x1 = filteredBoxes[torch.TensorIndex.Colon, 0] - filteredBoxes[torch.TensorIndex.Colon, 2] / 2;
            var y1 = filteredBoxes[torch.TensorIndex.Colon, 1] - filteredBoxes[torch.TensorIndex.Colon, 3] / 2;
            var x2 = filteredBoxes[torch.TensorIndex.Colon, 0] + filteredBoxes[torch.TensorIndex.Colon, 2] / 2;
            var y2 = filteredBoxes[torch.TensorIndex.Colon, 1] + filteredBoxes[torch.TensorIndex.Colon, 3] / 2;
            boxes = torch.stack(new torch.Tensor[] { x1, y1, x2, y2 }, 1);

            var scores = filteredScores.max(1).Item1;
            var nmsIndices = torchvision.ops.nms(boxes, scores, iouThreshold);

            var boundingBoxes = new BoundingBoxCollection();
            for (int index = 0; index < nmsIndices.shape[0]; index++)
            {
                var pointArray = new Point[2];
                var box = boxes[nmsIndices[index]].to_type(torch.int32);
                pointArray[0] = new Point(box[0].ToInt32(), box[1].ToInt32());
                pointArray[1] = new Point(box[2].ToInt32(), box[3].ToInt32());
                boundingBoxes.Add(new BoundingBox { Corners = pointArray });
            }
            return boundingBoxes;
        });
    }
}
