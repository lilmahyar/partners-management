# https://devblogs.microsoft.com/nuget/microsoft-author-signing-certificate-update/
# https://github.com/NuGet/Home/issues/10491

# the first, heavier image to build your code

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS builder

# Setup working directory for the project	 
WORKDIR /app
RUN curl -o /usr/local/share/ca-certificates/verisign.crt -SsL https://crt.sh/?d=1039083 && update-ca-certificates
COPY ./BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj ./BuildingBlocks/BuildingBlocks/ 
COPY ./PartnersManagement/PartnersManagement.csproj ./PartnersManagement/ 
COPY ./PartnersManagement.Api/PartnersManagement.Api.csproj ./PartnersManagement.Api/
 
# Restore nuget packages	 
RUN dotnet restore ./PartnersManagement.Api/PartnersManagement.Api.csproj 


# Copy project files
COPY ./BuildingBlocks/BuildingBlocks ./BuildingBlocks/BuildingBlocks/
COPY ./PartnersManagement ./PartnersManagement/ 
COPY ./PartnersManagement.Api ./PartnersManagement.Api/

# Build project with Release configuration
# and no restore, as we did it already
RUN dotnet build -c Release --no-restore ./PartnersManagement.Api/PartnersManagement.Api.csproj

# Publish project to output folder	 
# and no build, as we did it already	
WORKDIR /app/PartnersManagement.Api
RUN dotnet publish -c Release --no-build -o out


# second, final, lighter image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal

# Setup working directory for the project  
WORKDIR /app

# Copy published in previous stage binaries	 
  
# from the `builder` image
COPY --from=builder /app/PartnersManagement.Api/out  .		

ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker  

# sets entry point command to automatically	 
# run application on `docker run`	 
ENTRYPOINT ["dotnet", "PartnersManagement.Api.dll"]