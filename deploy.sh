#!/bin/sh
dotnet publish \
    --runtime linux-musl-x64 \
    -c Release \
    --no-self-contained \
&& docker build \
    --tag=prophet \
    --file=./Dockerfile \
    ./bin/Release/netcoreapp3.0/linux-musl-x64/publish \
&& docker tag prophet alexanderkarpov/prophet \
&& docker push alexanderkarpov/prophet
