# Getting Started

In general, when using the LinearDynamicalSystems package, you need to configure your Bonsai workflow to run the correct python environment which has the [lds_python](https://github.com/joacorapela/lds_python) package was installed. 

All of the examples are self-contained and can be found by going to the [Bonsai.ML - Examples](https://github.com/ncguilbeault/machinelearning-examples) repo. For each example, you can bootstrap the python and Bonsai environments from within each example directory. Below, we demonstrate how to bootstrap the the example environments.

### Bootstrapping Environments

Start by cloning the [Bonsai.ML - Examples](https://github.com/ncguilbeault/machinelearning-examples) repo:

```cmd
git clone https://github.com/ncguilbeault/machinelearning-examples.git
```

Change directory to one of the examples. For instance:

```cmd
cd examples/LinearDynamicalSystems/Kinematics/SimulatedData
```

To bootstrap the python virtual environment, run the following:

```cmd
python -m venv .venv 
.venv\Scripts\activate
pip install -r requirements.txt
```

To bootstrap the bonsai environment, run:

```cmd
powershell .bonsai\Setup.ps1
```

Once installed, run the bonsai application with:

```cmd
.bonsai\Bonsai.exe
```

Open the workflow example and start the bonsai workflow.
