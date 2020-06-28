using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    //last move position
    public int[] coins;
    public int[] places;
    public int lastMove;    //column of the last play

    public State(Board board,int lastMove)
    {
        coins = (int [])board.pieces.Clone();
        places = (int [])board.places.Clone();
        this.lastMove = lastMove;
    }

    public Board GetBoard()
    {
        Board board = new Board();
        for(int i = 0; i < 42; i++)
        {
            board.pieces[i] = (Board.Coin)coins[i];
        }
        for(int i = 0; i < 7; i++)
        {
            board.places[i] = places[i];
        }
        board.places[lastMove] += 7;
        board.pieces[places[lastMove]] = Board.Coin.empty;
        return board;
    }
}
