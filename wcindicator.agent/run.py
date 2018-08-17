from pyA20.gpio import port
from pyA20.gpio import gpio
from wcstate.wcstate import WCState
from wcstate.wcstate import WCStateManager

# config
inputPort = port.PE11
reportServerAddress = ''
# end config

gpio.init()
gpio.setcfg(inputPort, gpio.INPUT)
gpio.pullup(inputPort, 0)
gpio.pullup(inputPort, gpio.PULLDOWN)

manager = WCStateManager(reportServerAddress)

while True:
    if gpio.input(inputPort) == 1:
        manager.setWCState(WCState.Occupied)
    else:
        manager.setWCState(WCState.Free)


