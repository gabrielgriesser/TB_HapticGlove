start SENSO_BLE_SERVER.exe /saddr=0.0.0.0 /sport=53452 /channels=ffff0000e0
start SENSO_BLE_SERVER.exe /saddr=0.0.0.0 /sport=53453 /channels=00000fffef
start SENSO_UI.exe /saddr=127.0.0.1 /sport=53452 /caddr=0.0.0.0 /cport=53450 /udpaddr=127.0.0.1 /udpport=53451 /left /reconnect /name=DK2_L /enable_lh /drift /steamvr
start SENSO_UI.exe /saddr=127.0.0.1 /sport=53453 /caddr=0.0.0.0 /cport=53451 /udpaddr=127.0.0.1 /udpport=53450 /right /reconnect /name=DK2_R /enable_lh /drift /steamvr 
