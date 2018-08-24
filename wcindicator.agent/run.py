from pyA20.gpio import port
from pyA20.gpio import gpio

from threading import Thread
from datetime import datetime
from time import sleep

import json
import time

# config
inputPort = port.PA11
reportServerAddress = 'http://chcesiku.pl'
statusEndPoint = '/api/status'
heartbeatEndPoint = '/api/heartbeat'
interval = 10
# end config

import urllib2
def http_post(url, header, data):
    req = urllib2.Request(url, data, headers=header)
    response = urllib2.urlopen(req)
    return response


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

        response = http_post(self.reportServer, headers, json.dumps(data))

        # TODO: Add exceptions handling, and retries
        d = response.read()
        print('status= %s.data=%s' % (response.code, d))


def startHeartbeat(interval, reportServer):
    while True:
        response = http_post(reportServer,{},'')
        print('Heartbeat status= %s' % response.code)
        time.sleep(interval)


gpio.init()
gpio.setcfg(inputPort, gpio.INPUT)
gpio.pullup(inputPort, 0)
gpio.pullup(inputPort, gpio.PULLDOWN)

manager = WCStateManager(reportServerAddress+statusEndPoint)

t = Thread(target=startHeartbeat, args=(interval, reportServerAddress+heartbeatEndPoint))
t.start()

while True:
    if gpio.input(inputPort) == 1:
        manager.setWCState(1)
    else:
        manager.setWCState(0)


