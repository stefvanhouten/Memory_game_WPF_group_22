# Memory_game_group_22

# HBO ICT 1E Group 22.

# Participants:

- Stef van Houten
- Wietze Bronkema
- Daniël Safarizadeh
- Awather Alnashy

# Client:
- Harro Medema

# Version control:
- https://github.com/stefvanhouten/Memory_game_WPF_group_22

Each main feature was developed in a seperate branch. When the feature was completed this branch would be merged into the dev branch.
Each stable version of the memory game would then be merged into the main branch. 

# SCRUM
- https://trello.com/b/fEDNLW9j/memory-game

Tuesday and Friday we would meet and started the day with a SCRUM session.

# Summary

For most of the people in our group it was the first time programming in C# and for Awather C# was his very first programming language.
We chose to make memory game using the OOP principal. 
Although this would be harder for Awather, since he just started programming, we believe it would be in his best interest to start OOP as early as possible. 

We made sure that everybody in the group contributed an equal amount, not in quanitity of code but in hours invested in the project. 
Because we all come from different backgrounds there will be a difference in quanitity of code provided. 
However, this doesn't mean that less code equals less time spent. 

Everyone got assigned a class or multiple classes within the project for which they would be responsible. 

- Stef van Houten
- - Mainwindow
- - Memory and most subclasses found in Memory game folder.
- - Unittests

- Wietze Bronkema
- - Sound class 
- - Multiplayer
- - Data encryption 

- Daniël Safarizadeh
- - Data encryption
- - Data storage

- Awather Alnashy
- - Scoreboard
- - HighScores

Besides our assigned classes we would take turns in helping Awather, learning him the basics, learning him OOP and assigning him small 
homework tasks. The homewerk we assigned to him can be found on Trello.

# Game rules

2 player game. Players must submit a name, names can be the same. Game is played in turns, each turn consists out of choosing 2 cards. 
When both cards match yields x amount of points to the player that played the turn. When there is no match there is an additional check,
- Have both cards been visible?
- - If so remove x amount of points from player
- - If not no points will be taken

# Extra features
- Sound
- - Background music
- - On card match
- - On no card match
- - On game end
- - Mute game button
- - Audio slider

- Theme selection
- - Theme music
- - Card frontside and backside image according to theme
- - Background according to theme

- Pre-game
- - Dynamic game size options. The available game sizes are calculated based on the available cards per theme. The basic animal theme has more options than the starwars theme.

- Unittests
- - There are a few unittests for the main features of the memory game

- Encryption
- - All data stored in highscores and savegame files are encrypted. 
- - When data is changed the encryption is rendered useless and the contents in the file are thrown away


 