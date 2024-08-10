#!/bin/sh

echo " ### Build ### "
dotnet build

echo " ### Clean ### "
rm -R test/PoViEmu.Tests/TestResults

echo " ### Test ### "
dotnet test --collect:"XPlat Code Coverage"

echo " ### Report ### "
~/.dotnet/tools/reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage -reporttypes:Html

echo " ### Done. ### "

