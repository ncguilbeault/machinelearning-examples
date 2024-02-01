# Getting Started

### Dependencies

To run the examples, you must have the following tools installed:

- [Python (v3.10.12)](https://www.python.org/downloads/)
- [dotnet-sdk (v8)](https://dotnet.microsoft.com/en-us/download)

### Running Examples

All of the examples are all self-contained and only require a few commands to configure the python and Bonsai environments. 

To bootstrap the python virtual environment, change directory to the example folder and run the following:

```cmd
python -m venv .venv 
source .venv/bin/activate
pip install -r requirements.txt
```

To install and configure the bonsai environment, run:

```cmd
pwsh .bonsai/Setup.ps1
```

Once installed, activate and run the bonsai environment with:

```cmd
source .bonsai/activate
bonsai
```

Open the specific workflow example and start the bonsai workflow.