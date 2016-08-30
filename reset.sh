#!/bin/sh

rm -rf bin; echo ">> Folder deleted." &&
dotnet ef database update; echo ">> Initial Database created." &&
dotnet run;
