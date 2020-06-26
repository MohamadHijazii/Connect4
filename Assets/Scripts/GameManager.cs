using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Coin = Board.Coin;

public class GameManager : MonoBehaviour
{
    public GameObject []pieces;
    public int[] places = new int []{35, 36, 37, 38, 39, 40, 41};
    Board board;

    public Coin turn;

    public AiPlayer ai;

    public GameObject drops;
    public GameObject winPannel;
    public TextMeshProUGUI win,lose,draw;

    //the color of the ai is yellow and the player is red

    public static bool playerStartsFirst = true;

    private void Start()
    {
        turn = Coin.yellow;
        if (playerStartsFirst) switchTurns();
        board = new Board(turn);

    }

    public void drop(int col)
    {
        int place = places[col];
        if(place < 0)
        {
            Debug.Log($"Column {col} is full");
            return;
        }
        places[col] -= 7;
        Image image = pieces[place].GetComponent<Image>();
        image.color = GetColor();
        bool done = board.play(col);
        if (done)
        {
            EndTheGame();
        }
        switchTurns();
        if(turn == Coin.yellow && !board.end)
        {
            ai.play();
        }

    }

    public void EndTheGame()
    {
        Coin winner = board.winner;
        if(winner == Coin.empty)
        {
            draw.gameObject.SetActive(true);
        }
        else
        {
            if(winner == Coin.red)
            {
                win.gameObject.SetActive(true);

            }
            else
            {
                lose.gameObject.SetActive(true);

            }
        }
        winPannel.SetActive(true);
    }

    public Board getCurrentBoard()
    {
        return board.Clone();
    }

    public static void log(string s)
    {
        Debug.Log(s);
    }

    public void switchTurns()
    {
        if(turn == Coin.red)
        {
            turn = Coin.yellow;
            drops.SetActive(false);

        }
        else
        {
            turn = Coin.red;
            drops.SetActive(true);
        }
    }

    public Color GetColor()
    {
        return turn == Coin.red ? Color.red : Color.yellow;
    }
}
