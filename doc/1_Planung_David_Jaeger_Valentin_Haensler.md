# POS-Projekt – Team \& Projektidee

**David \& Valentin**

## Die Idee

Die Idee für unser POS-Projekt ist ein Roguelike-Spiel, inspiriert von The Binding of Isaac, jedoch mit eigenen Mechaniken im HTL-Setting.
Der Spieler spielt einen HTL-Schüler, der sich durch verschiedene Räume kämpft, Prüfungen besteht und versucht, alle 5 Schuljahre erfolgreich abzuschließen.

### Hauptfeatures:

1. Notensystem statt Leben
Spieler hat Noten (1–5) statt HP
    - Gute Noten → Buffs 
        - Buffs sind schnelleres Movement oder schnellere Schüsse
    - Schlechte Noten → Debuffs
        - Debuffs sind beispielsweise langsameres Movement oder langsamere Schüsse

2. Energie-System
    - Energie sinkt durch:
        - Kämpfen
        - Zeit
    - Energie wird durch Items (Essen) wiederhergestellt, kann im Shop gekauft werden
    - Bei 0 Energie stirbt man

3. Level-System
    - 5 Levels = 5 HTL-Klassen
    - Schwierigkeit steigt pro Level
    - Ein Level besteht aus mehreren Räumen

4. Boss + Prüfung am Ende jedes Levels:
    - Bossfight (Lehrer)
    - Während dem Bossfight kann man nicht schießen. 
    - Ab und zu werden multiple choice Fragen angezeigt und das Spiel wird pausiert, Richtig beantwortet = Schaden wird verursacht. 
    - Man muss den Attacken des Bosses ausweichen


## Must-Haves

- Zufällig generierte Levels, mit mehr Räumen, je höhere Klasse man sich befindet

- flüssiges Wechseln zwischen den Räumen 

- Gegner, die einen Attackieren 

- Speichern + Laden von Spielständen

- Shop in dem 3 Items gekauft werden können
    - zB.: Energy Drink

- Räume mit Fragen wo die Note verbessert

- Objekte in den Räumen mit Hitboxen

- Verschiedene Items (für die Energiewiederherstellung)

## Nice-To-Haves

- Achievments

- Fortgeschrittenere Notenverbesserungsräume
    - Räume bei denen man seine Note durch Aufgaben verbessern kann

- Sound & Musik

- Verschiedene Powerups

- Animationen im Menü

- Verschiedene Schwerigkeitsstufen

## Grobe Umsetzungsidee

- Als Framework wird WPF verwendet

- Zusammenarbeit über GIT