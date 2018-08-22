from pyA20.gpio import port
from pyA20.gpio import gpio

inputPort = port.PE11

gpio.init()
gpio.setcfg(inputPort, gpio.INPUT)
gpio.pullup(inputPort, 0)
gpio.pullup(inputPort, gpio.PULLDOWN)

while True:
    if gpio.input(inputPort) == 1:
        print 'ocupied'
    else:
        print 'free'