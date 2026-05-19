# 👶 BabyFællesskab & BabyLog

## Om BabyFællesskab
BabyFællesskab er et koncept for en babycafé kombineret med en digital platform til forældre med små børn.
Formålet er at skabe et trygt fællesskab, hvor forældre både fysisk og digitalt kan mødes, dele erfaringer og få støtte i hverdagen.
BabyFællesskab er designet som en skalerbar platform, hvor flere funktioner og moduler kan integreres i samme system, blandt andet BabyLog og Chat Community.

## Om BabyLog
BabyLog er en integreret applikation i BabyFællesskab.
Formålet med BabyLog er at hjælpe forældre med at registrere og følge barnets daglige rutiner og udvikling på en enkel og visuel måde.
Den nuværende version af systemet fokuserer primært på søvnregistrering og søvnanalyse.

# Funktioner i BabyLog
## Børnehåndtering
* Opret barn
* Se barnedetaljer
* Opdater barn
* Slet barn
## Søvnlog
* Registrer søvnperioder
* Se søvnhistorik
* Opdater søvnregistreringer
* Slet søvnregistreringer
* Se søvngrafer ugentligt, månedligt, årligt

# Teknologier i BabyLog
## Frontend
* Blazor
* Razor Components
* ApexCharts
## Backend
* ASP.NET Core Web API
## Database
* SQL Server
## Test
* BUnit

# Arkitektur
Projektet er udviklet med en lagdelt arkitektur og separation of concerns.
Systemet består blandt andet af:
* Razor Components
* ViewModels
* Services
* REST API kommunikation
* SQL database
BabyLog kommunikerer med BabyFællesskab gennem token-baseret autentifikation.

# GitHub Actions
Projektet anvender GitHub Actions til automatisk:
- build
- test
ved push og pull requests.

# Kør projektet
git clone https://github.com/yolandeomam/BabyLog.git 
dotnet restore 
dotnet build 
dotnet run

# Fremtidige forbedringer
* Madlog
* Blelog
* Udviklingsmilepæle
* Mobiloptimering

# Fremtidig CI/CD
Projektet kan senere udvides med automatisk deployment til Azure.
Det betyder, at GitHub Actions automatisk kan deploye:
- Blazor app
- ASP.NET Core API
når der pushes til `main.
Dette vil gøre workflowet til en fuld CI/CD pipeline.
