@echo off
rem Launch Push Notification, wait, then close the browser.
start http://131.235.1.167/PushNotification/pe_rainfall.aspx
ping -n 1 -w 10000 1.1 > nul
taskkill /im iexplore.exe
exit