# Zebrafish Centroid Tracking

In the following example, you can find how the Kalman Filter can be used for kinematic inference using centroid tracking data collected from a freely swimming zebrafish.

### Dataset

You can download the `ZebrafishExampleVid.avi` video file used in the example workflow [here](https://doi.org/10.5281/zenodo.10629221). The video needs to be downloaded into the `datasets` folder in the machinelearning-examples repository. Alternatively, you can run the following 

### Workflow

Below is the workflow.

:::workflow
![Kinematics - Zebrafish Tracking](ZebrafishTracking.bonsai)
:::

In this example, we created a Kalman Filter for infering kinematics from simulated data. We created a property called `SamplingFrequency` which dictates both the time step parameter of our model and also determines the rate at which the new simulated data values are generated.
