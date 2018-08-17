from enum import Enum
from datetime import datetime


class WCState(Enum):
    Free = 0
    Occupied = 1


class WCStateManager:
    def __init__(self):
        self.wcState = WCState.Free
        self.lastStateChange = datetime.now()

    def setWCState(self, state):
        if self.wcState == state:
            pass
        else:
            diff = datetime.now() - self.lastStateChange
            self.wcState = state
            self.sendState(diff)

    def sendState(self, diff):
        return