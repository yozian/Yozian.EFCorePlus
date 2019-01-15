#!/bin/bash
version=$1
cd nuget

if [ "$version" == "" ];then
   echo "version should be provided!"
   exit;
fi

nuget push Yozian.EFCorePlus.$1.nupkg  -source https://api.nuget.org/v3/index.json

cd ..