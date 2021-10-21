$version = Get-Date -Format "yy.MM.dHHmmss"
dotnet pack ./src/BlazorEasyAuth/BlazorEasyAuth.csproj -o $Env:localnugetpath -p:PackageVersion=$version -c Release