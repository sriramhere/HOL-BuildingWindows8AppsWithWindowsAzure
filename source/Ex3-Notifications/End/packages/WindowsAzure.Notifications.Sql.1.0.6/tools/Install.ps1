param($installPath, $toolsPath, $package, $project)

$path = [System.IO.Path]
$app_data = $path::Combine($path::GetDirectoryName($project.FileName), "App_Data")
if(!(Test-Path $app_data)) {	
	$project.ProjectItems.AddFolder("App_Data")
}

CACLS $app_data /E /G "NETWORK SERVICE:F" /T /C | out-null
ICACLS $app_data /grant :r "NETWORK SERVICE:F" /T /Q /C | out-null

$readmeFile = $path::Combine($path::GetDirectoryName($project.FileName), "App_Readme\WindowsAzure.Notifications.Sql.Readme.htm")
$DTE.ItemOperations.Navigate($readmeFile, [EnvDTE.vsNavigateOptions]::vsNavigateOptionsNewWindow)