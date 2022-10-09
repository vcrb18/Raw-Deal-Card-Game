import json

with open("superstars.json", "r") as jsonFile:
    data = json.load(jsonFile)
    att_list = list()
    cardinfo_dic = {"ClassName": "Ignore", "Attributes": att_list}
    for d in data:
        d["CardInfo"] = cardinfo_dic

with open("superstars.json", "w") as jsonFile:
    json.dump(data, jsonFile)