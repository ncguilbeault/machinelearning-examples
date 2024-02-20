# Getting Started

In general, when using the LinearDynamicalSystems package, you need to configure your Bonsai workflow to run the correct python environment which has the [lds_python](https://github.com/joacorapela/lds_python) package was installed.

If you followed the installation guide, you will have a basic Bonsai-python environment for using the LinearDynamicalSystems package. However, if you wish to run the examples, we highly recommend following this guide to create and configure the environments needed for each example. Alternatively, you can use the environment you created in the previous guide and install the necessary package dependencies required to run each workflow.

All of the examples are self-contained and can be found by going to the [Bonsai.ML - Examples](https://github.com/ncguilbeault/machinelearning-examples) repo. For each example, you can bootstrap the python and Bonsai environments from within each example directory. Below, we demonstrate how to bootstrap the example environments that are needed to run the example workflow.

### Bootstrapping Environments in Windows

Start by cloning the [Bonsai.ML - Examples](https://github.com/ncguilbeault/machinelearning-examples) repo from the command line:

```cmd
git clone https://github.com/ncguilbeault/machinelearning-examples.git
```

Change directory to one of the examples. For instance:

```cmd
cd machinelearning-examples\examples\LinearDynamicalSystems\Kinematics\SimulatedData
```

To create the python virtual environment and install the package, run the following:

```cmd
python -m venv .venv 
.\.venv\Scripts\activate
pip install lds_python@git+https://github.com/joacorapela/lds_python@168d4c05bb4b014998c7d3a2a57d143244a44bdd
```

To create the bonsai environment and install the packages, run:

```cmd
cd .bonsai
.\Setup.ps1
```

Once installed, run the bonsai application with:

```cmd
.\Bonsai.exe
```

Open the workflow example and start the bonsai workflow.

### Bootstrapping Environments in Linux

Open up a terminal and start by cloning the [Bonsai.ML - Examples](https://github.com/ncguilbeault/machinelearning-examples) repo:

```cmd
git clone https://github.com/ncguilbeault/machinelearning-examples.git
```

Change directory to one of the examples. For instance:

```cmd
cd examples/LinearDynamicalSystems/Kinematics/SimulatedData
```

To bootstrap the python virtual environment, run the following:

```cmd
python3 -m venv .venv 
source .venv/bin/activate
pip install -r requirements.txt
```

To bootstrap the bonsai environment, run:

```cmd
cd .bonsai
pwsh Setup.ps1
```

Once installed, activate the bonsai environment and run:

```cmd
cd ..
source .bonsai/activate
bonsai
```

or run the Bonsai.exe file directly with mono:

```cmd
mono .bonsai/Bonsai.exe
```

Open the workflow example and start the bonsai workflow.

#### Notes on Running Bonsai in Linux

When running the example workflows in Linux, it is important that you follow the general [Bonsai installation guide on Linux](https://github.com/orgs/bonsai-rx/discussions/1101) to ensure that the underlying Bonsai package dependencies are available to the system. Currently, the examples have been tested on Ubuntu 22.04. Running Bonsai on Linux is still being tested and should be used with caution.
