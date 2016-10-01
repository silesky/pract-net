#!/usr/bin/env bash
kill -9 `pgrep -f webpack` && kill -9 `pgrep -f dotnet`
git pull
dotnet run &
cd wwwroot && npm run devpack &
cd ..
