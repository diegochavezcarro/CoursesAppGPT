# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore any dependencies (via NuGet)
COPY ["CoursesApi/CoursesApi.csproj", "CoursesApi/"]
RUN dotnet restore "CoursesApi/CoursesApi.csproj"

# Copy the project files and build our release
COPY . .
WORKDIR "/src/CoursesApi"
RUN dotnet build "CoursesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoursesApi.csproj" -c Release -o /app/publish

# Generate runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoursesApi.dll"]
