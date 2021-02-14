@echo off
:Loop
ping -n 1 dd.weatheroffice.ec.gc.ca && ping -n 1 smtp.ncr.int.ec.gc.ca && ping -n 1 wmon01dtchlebl2
if not %errorlevel% equ 0 (Timeout /T 300 /nobreak 
goto Loop)
 
echo Connection established
start http://131.235.1.167/PushNotification/index.aspx
ping -n 1 -w 10000 1.1 > nul
start http://131.235.1.167/PushNotification/nb_rainfall.aspx
ping -n 1 -w 10000 1.1 > nul
start http://131.235.1.167/PushNotification/nl_rainfall.aspx
ping -n 1 -w 10000 1.1 > nul
start http://131.235.1.167/PushNotification/ns_rainfall.aspx
ping -n 1 -w 10000 1.1 > nul
start http://131.235.1.167/PushNotification/pe_rainfall.aspx
ping -n 1 -w 10000 1.1 > nul
taskkill /im iexplore.exe
exit
