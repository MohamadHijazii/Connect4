# Connect 4

Connect 4 is a university project in INFO 407 - Introduction to Artificial Intelligence.

Connect 4 is a two-player connection board game in which the players first choose a whenver to start first or second, and then take turns dropping one colored disc from the top into a seven-column, six-row vertically suspended grid. The pieces fall straight down, occupying the lowest available space within the column. The objective of the game is to be the first to form a horizontal, vertical, or diagonal line of four of one's own discs.

In our version of Connect 4, you can play vs an AI, the AI has 3 levels (Easy - Medium - Hard). 
We used `MinMax` algorithm with `Alpha Beta pruning` to build our tree, and on each move we create the tree and set evaluation points for our ai to play the best move in each turn.

Please check the `class node` to find the code of minimax tree.
