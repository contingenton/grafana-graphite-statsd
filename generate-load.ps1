#!/usr/bin/env pwsh

param(
    [Switch]$Single
)

if($Single) {
    Invoke-RestMethod "http://localhost:8081/weatherforecast"
} else {
    while($true) {
        $random = Get-Random -Minimum 100 -Maximum 1000
        Start-Sleep -Milliseconds $random

        Invoke-RestMethod "http://localhost:8081/weatherforecast"
    }
}
