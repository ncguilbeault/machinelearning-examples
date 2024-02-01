# Bonsai - Machine Learning Examples

The Bonsai - Machine Learning Library is a library built on top of Bonsai-Rx which contains reactive infrastructure for machine learning operations.

In this repository, we provide examples/demos for how to get started using the suite of Bonsai.ML packages that are currently available. We provide documentation for each example and discuss ways to incorporate Bonsai.ML modules/workflows into existing Bonsai-Rx workflows.

### Running Examples

Inside the examples directory, you will find directories pertaining to specific packages. Each package directory contains directories for examples/applications for each package. Inside each of these task/application folders, you will find a .bonsai folder and a requirements.txt file which are necessary to bootstrap the environment.

To bootstrap the python virtual environment, change directory to the specific task folder and run:

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
