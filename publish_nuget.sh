#!/bin/bash

BASEDIR=$(dirname "$0")
BASEDIR=$(builtin cd "$BASEDIR" || exit; pwd)
NUGET_PKGS_DIR="$BASEDIR"/.nuget-packages

# syntax: dotnet nuget push <nupkg_file> --api-key <api_key> --source https://f.feedz.io/mineral/core/nuget

# Clean the Nuget packages folder
echo "Cleaning the Nuget packages folder..."
#rm -rf "$NUGET_PKGS_DIR"/*

# Build solution
echo "Building solution..."
#dotnet build ShinyRock.Core.sln

# Get the NuGet key
echo "Getting the NuGet key..."
nuget_key=$(head -n 1 "$BASEDIR"/_/nuget_key.txt)

for filename in "$NUGET_PKGS_DIR"/*.nupkg; do
    echo ""
    echo "---"
    echo "Publishing ${filename##*/} to Nuget..."
    dotnet nuget push "$filename" --api-key "$nuget_key" --source https://f.feedz.io/mineral/core/nuget
done