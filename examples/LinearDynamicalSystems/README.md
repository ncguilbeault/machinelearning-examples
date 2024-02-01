# Getting Started

### Dependencies

To run the examples, you must have the following tools installed:

- [Python (v3.10.12)](https://www.python.org/downloads/)
- [Powershell on Linux](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-linux?view=powershell-7.4)

### Running Examples

All of the examples are self-contained and only bootstrapping the python and Bonsai environments. 

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

Once installed, activate and run the bonsai environment with:

```cmd
.bonsai\Bonsai.exe
```

Open the specific workflow example and start the bonsai workflow.