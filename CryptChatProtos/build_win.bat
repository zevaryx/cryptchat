@echo off
cd protos\responses
protoc *.proto --csharp_out=..\..\Responses --csharp_opt=file_extension=.g.cs
cd ..\requests
protoc *.proto --csharp_out=..\..\Requests --csharp_opt=file_extension=.g.cs