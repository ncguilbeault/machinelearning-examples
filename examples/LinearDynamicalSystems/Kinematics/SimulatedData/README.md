# Simulated Data

In the following example, you can find how the Kalman Filter class can be used for kinematic inference using simulated data.

### Getting Started

To bootstrap the python virtual environment, change directory to the example folder and run:

```cmd
python -m venv .venv # Initializes the virtual environment
source .venv/bin/activate # Activates the python virtual environment on Linux OS
pip install -r requirements.txt # Install the required python packages for the demo
```

To install and configure the bonsai environment, you can run:

```cmd
pwsh .bonsai/Setup.ps1 # This will setup the bonsai environment using the included Bonsai.config file
```

Once installed, you can then activate the bonsai environment if you have the Bonsai Linux environment template tool installed with:

```cmd
source .bonsai/activate
bonsai
```

or simply running the Bonsai.exe application using Mono with:

```cmd
mono .bonsai/Bonsai.exe
```

Once bonsai is running, open the specific workflow example and start the bonsai workflow.

### Workflow

Below is the workflow for running the Kalman Filter Kinematics model on simulated data.

:::workflow
![Inferring Kinematics - Simulation](Simulation.bonsai)
:::
