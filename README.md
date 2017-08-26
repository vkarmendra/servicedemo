# servicedemo
Topshelf Serilog Powershell script 

*** To install topshelf service using power shell script

> powershell -ExecutionPolicy ByPass -File .\installservice.ps1 '&lt;ServiceDisplayName>' '&lt;TopShelfServicePath>'

Example

powershell -ExecutionPolicy ByPass -File .\installservice.ps1 'LogService' '.\bin\debug\topshelfdemo.exe'

