from pydantic import BaseModel
import json

class SimpleDataClass(BaseModel):
    name: str
    data: float

model_schema = SimpleDataClass.model_json_schema()
with open('simple_data_class.json', 'w') as f:
    json.dump(model_schema, f, indent=4)