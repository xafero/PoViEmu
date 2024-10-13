@echo off

echo  ### Build ### 
dotnet build

echo  ### Clean ### 
rd /s /q test\PoViEmu.Tests\TestResults

echo  ### Test ### 
dotnet test --collect:"XPlat Code Coverage"

echo  ### Report ### 
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage -reporttypes:Html

echo  ### Done. ### 
