# CASE_RA

Dit is de repository van de EindCase van Ramon Abacherli.
Volg de kopjes in deze `Readme` in de gegeven volgorde om het project op te starten.

## Klonen

Kloon het project en ga met de Terminal/Command Prompt naar de map van het project. 

## Installatie Back-End

Om de `WebApi` en `SQL-Server database` te kunnen gebruiken is het nodig om de volgende dingen te doen:

- Zorg ervoor dat de `ConnectionString` in het bestand `appsettings.json` klopt met de eigen SQL-server installatie.

- Maak een database aan met de naam `CourseDb` in SQL-server.

- Typ het `Cd WebApi/WebApi` commando in je terminal, dus open het mapje van het `WebApi-project`.

- Gebruik het commando `dotnet ef database update` in je terminal. Hiermee wordt het project gebouwd en wordt de database aangemaakt.

Als het goed is heb je nu 3 lege tabellen in de database:
- dbo._EFMigrationsHistory
- dbo.CourseInstances
- dbo.Courses

Als dit zo is, dan is de installatie van de WebApi gereed. Gebruik nu het commando `dotnet run` in je terminal. De server zal worden opgestart met de volgende url: `http://localhost:5126`.

Gefeliciteerd, de server kan nu worden gebruikt.

## Installatie Front-End

Om de Angular voorkant te gebruiken, ga dan naar het `FrontEnd` project in de terminal.

Vervolgens is dit commando benodigd: 

`npx ng serve`

De voorkant zal nu op `http://localhost:4200` draaien. 

Gefeliciteerd, het volledige project kan nu worden gebruikt en getest!

## Ingebouwde Functionaliteiten

'Homepagina': 

- Overzicht van cursusinstanties per week, beginnend met de huidige week. Een week start op maandag en eindigt op zondag in dit programma.
- Mogelijkheid om de vorige en volgende week te bekijken, door met de pijlen te navigeren.
- Mogelijkheid om een weeknummer en jaar in de invoervelden te toetsen en op 'Zoeken' te drukken. De gezochte week wordt dan weergegeven.

'Importpagina':

- Importeer een bestand van cursussen + instanties. De UI zal een melding geven als dit lukt of een foutmelding bij een verkeerd bestand/formaat van het bestand. De tabel zal een overzicht van de import geven. Als hierna terug wordt gegaan naar de homepagina, dan kan nu gezocht worden naar deze instanties in de correcte week.


## Testen draaien

'Back-End':

- Navigeer naar het `WebApi` project in de terminal en gebruik het commando `dotnet test` om de unit tests te draaien. Deze zouden allemaal moeten slagen.

'Front-End':
- Navigeer naar het `FrontEnd` project in de terminal en gebruik het commando `npx ng test` om de tests te draaien. Deze zouden allemaal moeten slagen.

<br>
<br>
<strong>PS: @JP, thanks voor de leuke lessen! Fijn weekend en tot maandag :D</strong>