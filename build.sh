dotnet publish src/Yozian.EFCorePlus/Yozian.EFCorePlus.csproj \
    --force \
    -c Release \
    -o "../../nuget/lib/netstandard2.0"
    
rm nuget/lib/netstandard2.0/Yozian.EFCorePlus.deps.json