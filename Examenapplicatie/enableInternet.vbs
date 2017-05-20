strComputer = "."
Set objWMIService = GetObject( _
    "winmgmts:\\" & strComputer & "\root\cimv2")
Set colNetCards = objWMIService.ExecQuery _
    ("Select * From Win32_NetworkAdapterConfiguration " _
        & "Where IPEnabled = True")
For Each objNetCard in colNetCards
    objNetCard.RenewDHCPLease()
Next