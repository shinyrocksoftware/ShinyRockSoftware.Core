dotnet sonarscanner begin -k:"ShinyRock" -d:sonar.host.url="http://localhost:9000" -d:sonar.token="sqa_6e15aa4f39575eed35812bd527d77c5a04555868"
dotnet build
dotnet sonarscanner end -d:sonar.token="sqa_6e15aa4f39575eed35812bd527d77c5a04555868"