#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:12

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
COPY package*.json ./

RUN npm install
# If you are building your code for production
# RUN npm ci --only=production

# Bundle app source
COPY . .

EXPOSE 4200
CMD [ "node", "server.js" ]


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CustomerManagementPortal/CustomerManagementPortal.csproj", "CustomerManagementPortal/"]
COPY ["LogicLayer/LogicLayer.csproj", "LogicLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
RUN dotnet restore "CustomerManagementPortal/CustomerManagementPortal.csproj"
COPY . .
WORKDIR "/src/CustomerManagementPortal"
RUN dotnet build "CustomerManagementPortal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerManagementPortal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerManagementPortal.dll"]