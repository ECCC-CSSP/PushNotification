 
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
start http://131.235.1.167/PushNotification/bc_rainfall.aspx
ping -n 1 -w 10000 1.1 > nul
start http://131.235.1.167/PushNotification/qc_rainfall.aspx
ping -n 1 -w 10000 1.1 > nul
taskkill /im iexplore.exe
exit
