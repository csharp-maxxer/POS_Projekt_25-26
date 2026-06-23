# 2. Planungsphase

## GUI Skizzen:

Auf folgendem Bild soll man einen der Räume sehen und die Stats des Spielers.
<img src="Medien (3).jpg">

<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>

Auf diesem Bild sieht man die Menüs:
1) Das Startmenü
2) Stats
3) Achievment-Menü (nice-2-have)
<img src="Medien (2)a.jpg">

Im folgenden Bild das Pause Menü:
<img src="Medien (22).jpg">

Im folgenden Menue einen Shop:
<img src="Medien (2).jpg">

<br>
<br>
<br>
<br>

## Klassendiagramme:

@startuml
skinparam classAttributeIconSize 0

class Player
{
    +bullets:List<bullet>
    +damage:double {+get; +set;}
    +speed:double {+get; +set;}
    +range:double {+get; +set;}
    +grade:int {+get; +set;}
    +energy:double {+get; +set;}
    +firerate:double {+get; +set;}
    +x_pos:double {+get; +set;}
    +y_pos:double {+get; +set;}
--
    +Player(damage:double,speed:double,range:double,firerate:double,grade:int,energy:double)
--
    +attack():void
}

class Enemy
{ 
    +bullets:List<bullet>
    +damage:double {+get; +set;}
    +speed:double {+get; +set;}
    +range:double {+get; +set;}
    +firerate:double {+get; +set;}
    +x_pos:double {+get; +set;}
    +y_pos:double {+get; +set;}
--
    +Enemy(damage:double,speed:double,range:double)
--
    +move():void
    +attack():void
}

class Bullet
{
    +direction:Vector {+get; -set;}
    +damage:double {+get; -set;}
    +speed:double {+get; -set;}
    +range:double {+get; -set;}
    +x_pos:double {+get; -set;}
    +y_pos:double {+get; -set;}
--
    +Bullet(damage:double,speed:double,range:double)
--
+move():void
+hit():void
}

class GameLogger
{
+logger:Logger{static} {+get; -set;}
--
+init():void{static} 
}

class Room
{
--
+bool DoorTop { +get; +set; }
+bool DoorRight { +get; +set; }
+bool DoorBottom { +get; +set; }
+bool DoorLeft { +get; +set; }
+RoomCanvas: Canvas { +get; -set; }
+Xpos: int { +get; -set; }
+Ypos: int { +get; -set; }
-room_matrix:List<List<char>> { -get; -set;}
+enemies:List<Enemy> { +get; -set;}
+isCleared:bool { +get; +set;}
+Type:char { +get; -set; }
--
+Room(ypos:int, xpos:int, type:char)
--
+DrawRoom():void
+SpawnEnemies():void
}

class Boss
{
--
+bullets:List<bullet>
+questionCount:int {+get; -set;}
+x_pos:int {+get; +set;}
+y_pos:int {+get; +set;}
--
+Boss(damage:int,speed:int,range:int,firerate:int)
--
+CheckAnswer():bool
+move():void
}

class Statistics
{
--
+Attemps:int {static} {+get; +set;}
+Wins:int {static} {+get; +set;}
+Achievment_numer:int {static} {+get; +set;}
}


' Beziehungen
Player --> Bullet:shoots
Enemy --> Bullet:shoots
Boss --> Bullet:shoots

@enduml

<br>
<br>
<br>
<br>
<br>


## Aufgabenaufteilung

| Valentin | David |
|---|---|
| Raumlogik | Designs |
| Gegner | UI |
| Bossfights | Player |
| Random-Map-Generierung | Bullets |
| Raumwechsel | Kollisionen |
| Prüfungs-/Fragensystem | Notensystem |
| Speichern & Laden | Energie-System |
| Balancing | Startmenü / Pausemenü / Statistics Menü |
| Testing / Bugfixing | Logger |

<br>
<br><br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>
<br>


## Roadmap

https://github.com/users/csharp-maxxer/projects/3/views/4
<img src="Screenshot (565).png">
