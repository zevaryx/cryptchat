@echo off

protoc protos\*.proto -Iprotos\ --csharp_out=protos\ --csharp_opt=file_excention=.g.cs
protoc protos\responses\*.proto -Iprotos\responses --csharp_out=protos\responses --csharp_opt=file_excention=.g.cs
protoc protos\requests\*.proto -Iprotos\requests --csharp_out=protos\requests --csharp_opt=file_excention=.g.cs

robocopy /move /xo protos\ . *.cs
robocopy /move /xo protos\requests\ Requests\ *.cs
robocopy /move /xo protos\responses\ Responses\ *.cs