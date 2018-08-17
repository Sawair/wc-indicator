from wcstate.wcstate import WCStateManager
from wcstate.wcstate import WCState
import time

reportServerAddress = 'http://192.168.1.129:5000/api/status'

manager = WCStateManager(reportServerAddress)

manager.setWCState(WCState.Occupied)
time.sleep(61.5)
manager.setWCState(WCState.Free)
