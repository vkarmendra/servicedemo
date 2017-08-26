if($args.Length -ne 0){
	$serviceName = $args[0]
}

# verify if the service already exists, and if yes remove it first
$service = get-service $serviceName -ErrorAction SilentlyContinue 
if ($service –ne $null)
{
    "$serviceName is already installed on this server";
	Stop-Service -displayname "LogService"
	if (Get-Service $serviceName -ErrorAction SilentlyContinue)
	{
		# using WMI to remove Windows service because PowerShell does not have CmdLet for this
		$serviceToRemove = Get-WmiObject -Class Win32_Service -Filter "name='$serviceName'"
		Stop-Service -displayname "LogService"
		$serviceToRemove.delete()
		"service removed"
	}
}
else
{
	# just do nothing
    "service does not exists"
}

"installing service"
$servicepath = $args[1] #".\bin\debug\TopShelfDemo.exe"
Write-Host "Installing $serviceName...";

& "$servicepath" install --sudo
& Start-Service -displayname "LogService"

"installation completed"