@echo off

echo  ### Build ### 
dotnet build

echo  ### Clean ### 
rd /s /q test\PoViEmu.Tests.ABI\TestResults
rd /s /q test\PoViEmu.Tests.CPU\TestResults
rd /s /q test\PoViEmu.Tests.Gfx\TestResults
rd /s /q test\PoViEmu.Tests.ISA\TestResults

echo  ### Test ### 
dotnet test --collect:"XPlat Code Coverage"

echo  ### Report ### 
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage -reporttypes:Html

echo  ### Done. ###

