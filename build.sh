#!/bin/bash

version="0.0.0"
if [ -n "$1" ]; then version="$1"
fi

tag="0.0.0"
if [ -n "$2" ]; then tag="$2"
fi

dotnet pack src/Tools.EventStore/Tools.EventStore.csproj -o ../../dist -p:Version="$version" -p:PackageVersion="$version" -p:Tag="$tag" -c Release