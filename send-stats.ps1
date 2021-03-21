#!/usr/bin/env pwsh

param (
    $CounterName = "email.count",
    $Increment = 1,
    $StatsDEndpoint = "127.0.0.1",
    $StatsDPort = 8125
)

function Send-UdpDatagram
{
      Param ([string] $EndPoint, 
      [int] $Port, 
      [string] $Message)

      $IP = [System.Net.Dns]::GetHostAddresses($EndPoint) 
      $Address = [System.Net.IPAddress]::Parse($IP) 
      $EndPoints = New-Object System.Net.IPEndPoint($Address, $Port) 
      $Socket = New-Object System.Net.Sockets.UDPClient 
      $EncodedText = [Text.Encoding]::ASCII.GetBytes($Message) 
      $SendMessage = $Socket.Send($EncodedText, $EncodedText.Length, $EndPoints) 
      $Socket.Close() 
} 


while($true) {
    $random = Get-Random -Minimum 100 -Maximum 1000
    Start-Sleep -Milliseconds $random

    Write-Host "$(Get-Date -Format 'hh:mm:ss') - $($CounterName):$Increment|c"
    Send-UdpDatagram $StatsDEndpoint $StatsDPort "$($CounterName):$Increment|c"
}
