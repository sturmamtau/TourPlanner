### 1. Tour-Seite (tour-page.component)
main page zeigt Tour-Liste, Detailansicht und Formular.

Linke Seite: Tour-Liste
Rechte Seite: Detailansicht oder Formular

Auswahl einer Tour aus der Liste zeigt Details in der rechten Spalte
Klick auf "+ Add Tour" zeigt das Formular zum Erstellen einer neuen Tour
Suchleiste oben für Filterung der Touren

### 2. Tour-Liste (tour-list.component)
Zeigt alle verfügbaren Touren in einer scrollbaren Liste.

Button "+ Add Tour" zum Hinzufügen neuer Touren
zeigt "Keine Touren vorhanden." an wenn keine Touren existieren
Tour einträge werde angezeigt mit:
  Tour-Name
  Route: "Startort → Zielort"
  Transporttyp
Bei click auf eine tour wird die tour detail ansicht geöffnet 

### 3. Tour-Detailansicht (tour-detail.component)
Zeigt details zur ausgewählten tour

Überschrift mit Tour-Name und Aktionsbuttons
Karten-Platzhalter (später bild der karte)
Attribute für Tour-Details


Buttons: "Bearbeiten" und "Löschen"
Attribute:
  Von (Startort)
  Nach (Zielort)
  Transportmittel
  Geschätzte Zeit


"Bearbeiten" öffnet das Formular für diese Tour
"Löschen" entfernt die Tour (mit Bestätigung erforderlich?)

### 4. Tour-Formular (tour-form.component)
Formular zum Erstellen oder Bearbeiten von Touren.

Felder:
Id, Name, Beschreibung, Von, Nach, Transport Typ

Speichern: Validiert und speichert die Tour
Abbrechen: Schließt das Formular ohne Speichern

### 5. Suchleiste (search-bar.component)
Suchfunktionalität für Touren.

- ESpäter Suche beim Tippen, aktuell nur UI
- Clear leert suchbar

## -Workflows

- User können alle gespeicherten touren ind er tour liste sehen
- User kann neue Touren hinzufügen
- User kann bestehende Touren editieren oder löschen
- user kann Tour Logs der ausgewählten Tour ansehen
- User kann Tour Logs zu bestehendnden Touren hinzufügen
- USer kann bestehnde Tour Logs bearbeiten oder löschen
