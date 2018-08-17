from enum import Enum
from datetime import datetime

import requests


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
        data = {'ChangeDate': self.lastStateChange, 'Status': self.wcState, 'LastStatusDuration': diff}
        result = requests.post(self.reportServer, data=data)
        while result != 200:
            print('Status send failed retrying')
            result = requests.post(self.reportServer, data=data)
        print('Status send successful')


