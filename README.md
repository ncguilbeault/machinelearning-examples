# Bonsai - Machine Learning Examples

The Bonsai - Machine Learning Library is a library built on top of Bonsai-Rx which contains reactive infrastructure for machine learning operations.

In this repository, we provide a number of examples/demos for how to get started using the suite of Bonsai.ML packages that are currently available. We provide documentation for different examples and demonstrate ways to incorporate Bonsai.ML modules/workflows into existing Bonsai-Rx workflows for various tasks.

### Running Examples

Inside the examples repository contains examples pertaining to specific packages and tasks. Inside each of these folders, you will find a .bonsai folder and a requirements.txt file which are necessary to bootstrap the environment.

To bootstrap the python virtual environment, you can run:

```cmd
python -m venv .venv # Initializes the virtual environment
.venv\Scripts\activate.bat # Activates the python virtual environment on Windows OS
pip install -r requirements.txt # Install the required python packages for the demo
```

To install and configure the bonsai environment, you can run:

```cmd
powershell .bonsai\Setup.ps1 # This will setup the bonsai environment using the included Bonsai.config file
```

Launch the Bonsai.exe appliction inside of the .bonsai directory and open the specific workflow example.
