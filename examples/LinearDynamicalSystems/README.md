# Getting Started

In general, when using the LinearDynamicalSystems package, you need to configure your Bonsai workflow to run the correct python environment which has the [lds_python](https://github.com/joacorapela/lds_python) package was installed.

If you followed the installation guide, you will have a basic Bonsai-python environment for using the LinearDynamicalSystems package. You will also have installed the basic dependencies required to build and run the Bonsai workflows. However, if you wish to run the examples, we highly recommend following this guide to create and configure the environments needed for each example. This guide has been made for users starting from scratch to get started. Alternatively, you can use the environment you created in the previous guide and install the necessary package dependencies required to run each workflow.

All of the examples are self-contained and can be found by going to the [Bonsai.ML - Examples](https://github.com/bonsai-rx/machinelearning-examples) repo. For each example, you can bootstrap the python and Bonsai environments from within each example directory. Below, we demonstrate how to get started using the examples by bootstrapping the example environments that are needed to run the example workflow.

### Windows

#### Dependencies

You must have the following installed on your PC in order to run the example workflows:

- [Python (v3.10)](https://www.python.org/downloads/) *Note: python must be installed using the windows installer and must be added to the system PATH*
- [Git](https://git-scm.com/downloads)
- [Microsoft Visual C++ Redistributable](https://aka.ms/vs/16/release/vc_redist.x64.exe)

*Note: when running windows in a virtual machine, it is necessary to install the OpenGL mesa drivers on windows to enable image visualizers and shaders [see here](https://github.com/pal1000/mesa-dist-win)*

#### Instructions

Open the terminal. Start by cloning the [Bonsai.ML - Examples](https://github.com/bonsai-rx/machinelearning-examples) repo with:

```cmd
git clone https://github.com/bonsai-rx/machinelearning-examples.git
```

Change directory to one of the examples. For instance, this will bring you to the simulated data example from the LinearDynamicalSystems.Kinematics package:

```cmd
cd .\machinelearning-examples\examples\LinearDynamicalSystems\Kinematics\SimulatedData
```

For the next step, you need to make sure that powershell scripts are executable from the terminal. To do this, open a powershell terminal using `Run as Administrator` and enter `yes` after the following:

```powershell
set-executionpolicy remotesigned
```

To create the python virtual environment and install the package, run the following:

```cmd
python -m venv .venv 
.\.venv\Scripts\activate
pip install lds_python@git+https://github.com/joacorapela/lds_python@4233363320e021f77f9b3e124846ec2e49c0e741
```

To create the bonsai environment and install the packages, run:

```cmd
cd .bonsai
.\Setup.ps1
```

Once installed, run the bonsai executable with:

```cmd
.\Bonsai.exe
```

Open the workflow example and start the bonsai workflow.

### Linux

#### Notes on Running Bonsai in Linux

Currently, the examples have only been tested on Ubuntu 22.04. Running Bonsai on Linux is still being tested and sheould be used with caution. When running the example workflows in Linux, it is important that you follow the general [Bonsai installation guide on Linux](https://github.com/orgs/bonsai-rx/discussions/1101) to ensure that the underlying Bonsai package dependencies are available to the system.

#### Dependencies

You must have the following installed on your PC in order to run the example workflows:

- [Python (v3.10)](https://www.python.org/downloads/) *comes installed with the latest version of Ubuntu 22.04*
- [Git](https://git-scm.com/downloads)
- [Mono](https://www.mono-project.com/download/stable/#download-lin)
- [OpenCV binaries](https://github.com/orgs/bonsai-rx/discussions/1101) *the simplest method is to follow the instructions on installing OpenCV from pre-built binaries*

#### Instructions

Open up a terminal and start by cloning the [Bonsai.ML - Examples](https://github.com/bonsai-rx/machinelearning-examples) repo:

```cmd
git clone https://github.com/bonsai-rx/machinelearning-examples.git
```

Change directory to one of the examples. For instance:

```cmd
cd machinelearning-examples/examples/LinearDynamicalSystems/Kinematics/SimulatedData
```

Install the python 3.10 virtual env package:

```cmd
sudo apt install -y python3.10-venv
```

Create the python virtual environment and install the lds python package with the following:

```cmd
python3 -m venv .venv 
source .venv/bin/activate
pip install lds_python@git+https://github.com/joacorapela/lds_python@4233363320e021f77f9b3e124846ec2e49c0e741
```

Install the `xmllint` and the `xmlstarlet` packages to run the bonsai setup script:

```cmd
sudo apt install -y libxml2-utils xmlstarlet
```

Run the `Setup.sh` script using the following:

```cmd
cd .bonsai
./Setup.sh
```

Once the bonsai environment has been created, activate the bonsai environment and run the Bonsai executable:

```cmd
source ./activate
bonsai
```

or run the Bonsai executable file directly with mono:

```cmd
mono Bonsai.exe
```

Open the workflow example and start the bonsai workflow.
