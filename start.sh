#!/usr/bin/env bash
git pull
dotnet run &
cd wwwroot && npm run devpack &
cd ..
