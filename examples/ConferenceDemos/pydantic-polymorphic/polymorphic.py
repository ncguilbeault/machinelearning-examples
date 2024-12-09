from pydantic import BaseModel
import json

class Trial(BaseModel):
    id: int
    name: str

class Session(BaseModel):
    id: int
    name: str
    trial: Trial

model_schema = Session.model_json_schema()
with open('session.json', 'w') as f:
    json.dump(model_schema, f, indent=4)