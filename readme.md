# Player logs in to see Start button

# StartButtonPressed()
* hide Start button
* subscribe player to SignalR hub
* see message "Waiting for partner..."
* if 2 players are ready, trigger StartGame()

# StartGame()
* creates game id + board state
* randomly assign X and O
* game id, player names and styles gets displayed

# PieceClicked()
* validate input
* affect board state
* display new board state
* if HasWinner || IsDraw, trigger GameOver(bool draw, string winner)

# GameOver()
* disable input
* show winner
* show Start button