projectName='Yozian.EFCorePlus'

dotnet publish src/$projectName/$projectName.csproj \
    --force \
    -c Release \
    -o "bin/publish"

cp src/$projectName/bin/publish/$projectName.dll nuget/lib/netstandard2.0/
cp src/$projectName/bin/publish/$projectName.pdb nuget/lib/netstandard2.0/
