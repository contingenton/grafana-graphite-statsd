#!/usr/bin/env pwsh

while($true) {
    $random = Get-Random -Minimum 100 -Maximum 1000
    Start-Sleep -Milliseconds $random

    Invoke-RestMethod "http://localhost:8081/weatherforecast"
}
