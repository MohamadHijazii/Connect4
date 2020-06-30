using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Coin = Board.Coin;
using Unity.Jobs;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI noPrun, prun;

    public GameObject []pieces;
    public int[] places = new int []{35, 36, 37, 38, 39, 40, 41};
    Board board;

    public Coin turn;

    public AiPlayer ai;

    public GameObject drops;
    public GameObject winPannel;
    public TextMeshProUGUI win,lose,draw;

    public Apple apple;

    public Stack<State> states;

    //the color of the ai is yellow and the player is red

    public static bool playerStartsFirst = false;
    public static int difficulty;

    private void Start()
    {
        states = new Stack<State>();
        turn = Coin.yellow;
        if (playerStartsFirst) switchTurns();
        board = new Board(turn);
        ai.depth = difficulty;
        ai.InitializeAi();
        Debug.Log($"d:{difficulty} ,ai:{ai.depth}");
        if (!playerStartsFirst)
        {
            ai.play();
            //switchTurns();
        }

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
            apple.startThinking();
            ai.play();
            apple.stopThinking();
        }
        noPrun.text = $"Nodes:\n{Node.sumOf7(ai.depth)}";
        prun.text = $"After Prun:\n{Node.nb}";
        states.Push(new State(board,col));

    }

    public void Undo()
    {
        states.Pop();
        Board board = states.Pop().GetBoard();
        this.board = board.Clone();
        for(int i = 0; i < 42; i++)
        {
            Image image = pieces[i].GetComponent<Image>();
            if(board.pieces[i] == Coin.empty)
            {
                image.color = Color.white;
            }
            if (board.pieces[i] == Coin.red)
            {
                image.color = Color.red;
            }
            if (board.pieces[i] == Coin.yellow)
            {
                image.color = Color.yellow;
            }

        }
        for(int i = 0; i < 7; i++)
        {
            places[i] = board.places[i];
        }
        //switchTurns();
    }

    public void EndTheGame()
    {
        Coin winner = board.winner;
        if(winner == Coin.empty)
        {
            draw.gameObject.SetActive(true);
            apple.draw();
        }
        else
        {
            if(winner == Coin.red)
            {
                win.gameObject.SetActive(true);
                apple.lose();

            }
            else
            {
                lose.gameObject.SetActive(true);
                apple.win();
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

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
