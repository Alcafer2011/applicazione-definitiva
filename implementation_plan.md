# Ripristino Totale Microservizi e Configurazione Docker

L'obiettivo è attivare completamente l'intera suite di microservizi (circa 55 in totale), correggere gli errori di compilazione per i servizi attualmente disabilitati e assicurarsi che tutti vengano caricati e funzionino correttamente in Docker Desktop.

## User Review Required

> [!WARNING]
> Stiamo per modificare la configurazione di circa 20 microservizi che al momento sono disabilitati a causa di errori di compilazione (per lo più dipendenze NuGet mancanti o file `appsettings.json` non presenti). Questo modificherà massivamente i file `.csproj` dei servizi all'interno della cartella `services/`.

> [!IMPORTANT]
> Il file `AI.EnterpriseOS.slnx` attuale è vuoto. Come parte del piano, genereremo un file di soluzione valido contenente tutti i progetti, in modo da poter compilare l'intera soluzione in una volta sola.

## Open Questions

- Esistono microservizi specifici nella cartella `services/` che **NON** devono essere attivati o che sono stati deprecati? 
- Sei d'accordo con la ricostruzione completa dei container Docker una volta risolti i problemi? Ci vorranno alcuni minuti.

## Proposed Changes

### 1. Risoluzione Errori di Compilazione dei Microservizi Disabilitati

La cartella `services/` contiene numerosi microservizi (es. `HR.Service`, `Finance.Service`, `ActionAI.Service`) che non compilano perché i loro file `.csproj` mancano delle dipendenze essenziali.

Per tutti i progetti all'interno della cartella `services/` (circa 22 progetti):
- Aggiungeremo le referenze necessarie ai file `.csproj`:
  - `MongoDB.Driver` (v3.1.0)
  - `Swashbuckle.AspNetCore` (v6.5.0)
- Creeremo o correggeremo i file `appsettings.json` e `appsettings.Development.json` ove mancanti, impostando la connessione a RabbitMQ e MongoDB.
- Se mancano file fondamentali come `Program.cs` o `Controllers.cs` (come evidenziato in un file di report precedente per `HR.Service`), verranno ricreati utilizzando un template standard.

### 2. Attivazione in docker-compose.yml

Tutti i servizi attualmente disabilitati sotto il commento `# Servizi dalla directory services/ - temporaneamente disabilitati per errori di compilazione` verranno attivati (s-commentati).

#### [MODIFY] [docker-compose.yml](file:///c:/Users/infoa/Desktop/AI-Enterprise-OS/docker-compose.yml)

### 3. Configurazione della Soluzione (.slnx)

Il file della soluzione è vuoto e impedisce una compilazione strutturata e i check dell'IDE.
Creeremo un comando per ricostruire un file `AI.EnterpriseOS.sln` standard popolandolo con tutti i circa 45 progetti (quelli root e quelli in `services/`).

#### [MODIFY] [AI.EnterpriseOS.sln](file:///c:/Users/infoa/Desktop/AI-Enterprise-OS/AI.EnterpriseOS.sln)

## Verification Plan

### Automated Tests
1. Verrà eseguito `dotnet build AI.EnterpriseOS.sln` per assicurarsi che tutti i ~45 servizi C# compilino simultaneamente senza errori.
2. Verrà eseguito `docker compose config` per validare la sintassi del `docker-compose.yml` aggiornato.

### Manual Verification
1. Esecuzione di `docker compose build --parallel` per le immagini modificate.
2. Esecuzione di `docker compose up -d`.
3. Controllo tramite `docker compose ps` che tutti i 55+ container (servizi + frontend + infrastruttura) abbiano lo stato `Up` e non stiano andando in crash (restarting).
