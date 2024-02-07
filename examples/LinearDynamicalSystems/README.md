# Getting Started

In general, when using the LinearDynamicalSystems package, you need to configure your Bonsai workflow to run the correct python environment which has the [lds_python](https://github.com/joacorapela/lds_python) package was installed. 

All of the examples provided are self-contained, such that you can bootstrap the python and Bonsai environments from within each example directory. Below, we demonstrate how to bootstrap the environments provided in the example folders to run the LinearDynamicalSystems package.

### Windows

To bootstrap the python virtual environment, change directory to the example folder and run the following:

```cmd
python -m venv .venv 
.venv\Scripts\activate
pip install -r requirements.txt
```

To install and configure the bonsai environment, run:

```cmd
powershell .bonsai\Setup.ps1
```

Once installed, run the bonsai application with:

```cmd
.bonsai\Bonsai.exe
```

Open the specific workflow example and start the bonsai workflow.
