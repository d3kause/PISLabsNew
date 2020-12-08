FROM microsoft/dotnet:2.1-sdk as compiler
COPY . /root/src
RUN dotnet publish -c Release -v Detailed -o /root/PISLabs /root/src

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 as base
COPY --from=compiler /root/PISLabs /root/PISLabs
WORKDIR /root/PISLabs
CMD ["dotnet", "PISLabs.dll"]
