#!/bin/sh

rm -rf bin; echo ">> Folder deleted." &&
dotnet ef database update; echo ">> Database updated." &&
dotnet build
