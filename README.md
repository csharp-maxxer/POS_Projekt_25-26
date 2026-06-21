# POS_Projekt_25-26

# 🎓 HTL Roguelike - 5 Jahre Hölle

Ein Roguelike-Spiel im HTL-Setting, entwickelt als POS-Projekt.

## 📖 Projektbeschreibung

In diesem Spiel schlüpfst du in die Rolle eines HTL-Schülers und kämpfst dich durch fünf Schuljahre (bzw Levels). Jedes Schuljahr besteht aus mehreren zufällig generierten Räumen voller Gegner, Herausforderungen und Prüfungsfragen.

Am Ende jedes Levels wartet ein Bosskampf gegen einen Lehrer. Statt klassischer Kämpfe müssen dabei Multiple-Choice-Fragen beantwortet werden, während man den Angriffen des Bosses ausweicht.

Das Ziel ist es, alle fünf Klassen erfolgreich abzuschließen.

---

## ✨ Features

### 📚 Notensystem

Anstelle von Lebenspunkten besitzt der Spieler eine Schulnote (1–5).

* Gute Noten verbessern die Spielfigur nach abschließen eines Levels durch Buffs.
* Schlechte Noten verursachen Debuffs wie geringere Geschwindigkeit oder langsamere Schüsse nach dem Abschließen eines Levels.

### ⚡ Energie-System

Der Spieler besitzt zusätzlich einen Energiebalken.

Energie sinkt durch verstreichen der Zeit.

Energie kann durch Essen oder Getränke wiederhergestellt werden, die im Shop gekauft werden können.

Erreicht die Energie 0, ist das Spiel verloren.

### 🗺️ Zufällig generierte Level

* 5 Levels (1.–5. Klasse)
* Mit jedem Schuljahr steigt der Schwierigkeitsgrad, dadurch dass es mehr Räume gibt.
* Höhere Klassen besitzen größere Karten mit mehr Räumen.

### 👾 Gegner

* Gegner verfolgen und attackieren den Spieler.
* Kollisionen und Projektile sorgen für abwechslungsreiche Kämpfe.

### 👨‍🏫 Bosskämpfe

Am Ende jedes Schuljahres wartet ein Lehrer als Boss.

Besonderheiten:

* Während des Bosskampfes kann nicht geschossen werden.
* Das Spiel pausiert regelmäßig für Multiple-Choice-Fragen.
* 5 Richtige Antworten werden benötigt, um den Boss zu besiegen.
* Gleichzeitig müssen dessen Angriffen ausgewichen werden.

### 🛒 Shop

Im Shop können verschiedene Items gekauft werden, beispielsweise:

* Energy Drinks
* Essen zur Energiewiederherstellung

### 💾 Speichern & Laden

Der Spielstand kann jederzeit gespeichert und später wieder geladen werden.

---

## ✅ Must-Haves

* Zufällig generierte Maps
* Raumwechsel
* Gegner
* Bosskämpfe
* Prüfungs-/Fragensystem
* Shop
* Energie-System
* Notensystem
* Speichern und Laden
* Kollisionssystem
* Räume zur Notenverbesserung

---

## ⭐ Nice-to-Haves

* Achievements
* Schwierigkeitseinstellungen
* Power-Ups
* Sound und Musik
* Menüanimationen
* Erweiterte Prüfungsräume

---

## 🛠️ Technologien

* C#
* WPF
* Git
* GitHub

---

## 👥 Projektteam

| Name                 | Aufgaben                                                                                                                 |
| -------------------- | ------------------------------------------------------------------------------------------------------------------------ |
| **Valentin Hänsler** | Raumlogik, Gegner, Bosskämpfe, Random-Map-Generierung, Raumwechsel, Prüfungs-/Fragensystem, Speichern & Laden, Balancing |
| **David Jäger**      | UI, Designs, Player, Bullets, Kollisionen, Notensystem, Energie-System, Startmenü, Pausemenü, Statistikmenü, Logger, Achievements      |


---

Dieses Projekt wurde im Rahmen des POS-Unterrichts an der HTL entwickelt und dient ausschließlich zu Lern- und Ausbildungszwecken.
