#!/bin/sh
rm -rf ./dist \
&& dotnet publish \
    --runtime linux-x64 \
    -c Release \
    --self-contained \
    --output ./dist \
    --nologo \
&& scp -r ./dist/* dialogs:/home/dialogs/bin/prophet
