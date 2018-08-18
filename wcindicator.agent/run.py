from pyA20.gpio import port
from pyA20.gpio import gpio

from datetime import datetime
from time import sleep

import httplib


# config
inputPort = port.PE11
reportServerAddress = ''
# end config


class WCStateManager:
    def __init__(self, reportServer):
        self.wcState = 0
        self.lastStateChange = datetime.now()
        self.reportServer = reportServer

    def getStateName(self):
        if self.wcState == 0:
            return 'Free'
        else:
            return 'Occupied'

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
        data = {'ChangeDate': self.lastStateChange.isoformat(), 'Status': self.getStateName(), 'LastStatusDuration': diff.seconds}
        headers = {'Content-type': 'application/json'}
        conn = httplib.HTTPSConnection(self.reportServer)
        conn.request('POST', '', data, headers)
        response = conn.getresponse()
        while response.status != 200:
            sleep(2)
            print('Status send failed retrying')
            response = conn.getresponse()
        print('Status send successful')


gpio.init()
gpio.setcfg(inputPort, gpio.INPUT)
gpio.pullup(inputPort, 0)
gpio.pullup(inputPort, gpio.PULLDOWN)

manager = WCStateManager(reportServerAddress)

while True:
    if gpio.input(inputPort) == 1:
        manager.setWCState(1)
    else:
        manager.setWCState(0)


