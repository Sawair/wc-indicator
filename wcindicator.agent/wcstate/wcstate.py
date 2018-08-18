from enum import Enum
from datetime import datetime
from time import sleep

import requests

# That classes are writen for python 3.6

class WCState(Enum):
    Free = 0
    Occupied = 1


class WCStateManager:
    def __init__(self, reportServer):
        self.wcState = WCState.Free
        self.lastStateChange = datetime.now()
        self.reportServer = reportServer

    def setWCState(self, state):
        if self.wcState == state:
            pass
        else:
            now = datetime.now()
            diff = now - self.lastStateChange
            self.lastStateChange = now
            self.wcState = state
            self.sendState(diff)

    def sendState(self, diff):
        data = {'ChangeDate': self.lastStateChange.isoformat(), 'Status': self.wcState.name, 'LastStatusDuration': diff.seconds}
        result = requests.post(self.reportServer, json=data)
        while result.status_code != 200:
            sleep(2)
            print('Status send failed retrying')
            result = requests.post(self.reportServer, json=data)
        print('Status send successful')


