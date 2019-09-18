#!/bin/sh
dotnet publish \
    --runtime linux-musl-x64 \
    -c Release \
    --no-self-contained \
&& docker build \
    --tag=prophet-vk-journalist \
    --file=./Dockerfile \
    ./bin/Release/netcoreapp3.0/linux-musl-x64/publish \
&& docker tag prophet-vk-journalist alexanderkarpov/prophet-vk-journalist \
&& docker push alexanderkarpov/prophet-vk-journalist
