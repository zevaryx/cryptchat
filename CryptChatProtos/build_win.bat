@echo off

protoc protos\*.proto -Iprotos\ --csharp_out=protos\
protoc protos\responses\*.proto -Iprotos\responses --csharp_out=protos\responses
protoc protos\requests\*.proto -Iprotos\requests --csharp_out=protos\requests

xcopy /K /D /H /Y protos\requests\*.cs Requests\
xcopy /K /D /H /Y protos\responses\*.cs Responses\