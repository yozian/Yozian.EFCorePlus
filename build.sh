dotnet publish src/Yozian.EFCorePlus/Yozian.EFCorePlus.csproj \
    --force \
    -c Release \
    -o "../../nuget/lib/netstandard2.0"

cd nuget/lib/netstandard2.0/
find . -type f ! -name "Yozian.*.dll" -exec rm -rf {} \;
cd -