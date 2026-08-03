#ETAP 1: Zbudowanie projektu wewnątrz kontenera
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Tworzymy folder w kontenerze i wchodzimy do niego
WORKDIR /IssueTicketService

# Kopiujemy csproje aby móc zapewnić wszystkie zależności w projekcie
COPY ["IssueTicketService.Api/IssueTicketService.Api.csproj", "IssueTicketService.Api/"]
COPY ["IssueTicketService.Application/IssueTicketService.Application.csproj", "IssueTicketService.Application/"]
COPY ["IssueTicketService.Core/IssueTicketService.Core.csproj", "IssueTicketService.Core/"]
COPY ["IssueTicketService.Infrastructure/IssueTicketService.Infrastructure.csproj", "IssueTicketService.Infrastructure/"]

# restore zaciąga zależności dla projektu na podstawie głównego projektu, który znajduje się w podanej lokalizacji,
# bez wskazania ścieżki, ze względu na brak pliku sln w obecnej lokalizacji restore może samo sobie nie poradzić.
# Aby tego uniknąc musimy najpierw wejść do WORKDIR /IssueTicketService.Api/IssueTicketService.Api a następnie w 
# dockerfile możemy wywołać samo dotnet restore. !!! Należy pamiętać aby po tej operacji wrócic do folderu głównego !!!
RUN dotnet restore "IssueTicketService.Api/IssueTicketService.Api.csproj"

#Dla Modular Monolith
# Kopiujemy strukturę i zostawiamy tylko pliki projektów oraz solucję
#COPY . .
#RUN find . -type f -not -name '*.csproj' -not -name '*.sln' -delete

# TUTAJ: Nie musisz pisać "IssueTicketService.sln". 
# Dotnet sam znajdzie ten plik w katalogu głównym i przywróci całą solucję.
#RUN dotnet restore

# Kopjujemy wszystkie pliki źródłowe do kontenera (Po za tymi zawartymi w .dockerignore)
COPY . . 
# Zbudowanie aplikacji w trybie Release
RUN dotnet publish "IssueTicketService.Api/IssueTicketService.Api.csproj" -c Release -o /WebApp/Publish

#Etap drugi uruchomienie naszej aplikacji wewnątrz kontenera

# Użycie lekkiego runtime zamiast ciężkiego SDK do już właściwego uruchomienia aplikacji. Użycie kolejnego FROM
# Rozpoczyna nam drugi pakiet instrukcji i nasz ,,final'' musi zostać poinformowany skąd dokładnie bierze pliki (--from)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
# Wejście do folderu w którym dotnet zbudował nam aplikację 
WORKDIR /WebApp
#Kopiujemy do tego etapu już skompilowane pliki z etapu 1
COPY --from=build /WebApp/Publish .


#EXPOSE 8080 - możemy wystawić port, przez który nasz kontener będzie komunikować się ze światem
ENV ASPNETCORE_URLS=http://+:8080

#Uruchomienie aplikacji komendom
ENTRYPOINT ["dotnet", "IssueTicketService.Api.dll"]