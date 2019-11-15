@echo off

protoc protos\*.proto -Iprotos\ --csharp_out=protos\
protoc protos\responses\*.proto -Iprotos\responses --csharp_out=protos\responses
protoc protos\requests\*.proto -Iprotos\requests --csharp_out=protos\requests

robocopy /move /xo protos\ . *.cs
robocopy /move /xo protos\requests\ Requests\ *.cs
robocopy /move /xo protos\responses\ Responses\ *.cs