using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public Coin[] pieces;       //piece number 0 is at the top left
    public int[] places;

    public Coin winner = Coin.empty;
    public bool end = false;

    public Coin turn;       //whos turn

    public Board(Coin firstToStart)
    {
        turn = firstToStart;
        pieces = new Coin[42];
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i] = Coin.empty;
        }
        places = new int[] { 35, 36, 37, 38, 39, 40, 41 };

    }

    public Board()
    {

        pieces = new Coin[42];
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i] = Coin.empty;
        }
        places = new int[] { 35, 36, 37, 38, 39, 40, 41 };
    }

    public bool play(int col)
    {
        int place = places[col];
        if (place < 0)
        {
            return false;
        }
        places[col] -= 7;
        pieces[place] = turn;
        checkWin();
        if (!end)
            checkDraw();
        switchTurns();
        return end;
    }

    #region check win

    public void checkWin()
    {
        checkWinHorizontal();
        checkWinVertical();
        checkWinOblique1();
        checkWinOblique2();
    }

    public bool checkDraw()
    {
        for (int i = 0; i < 7; i++)
        {
            if (places[i] > 0)
            {
                return false;
            }
        }
        end = true;
        winner = Coin.empty;
        return true;
    }

    void checkWinHorizontal()
    {
        int i = 0;
        while (i < 42)
        {
            if (pieces[i] == Coin.empty)
            {
                i++;
                if (i % 7 == 4)
                {
                    i += 3;
                }
                continue;
            }

            if (pieces[i] == pieces[i + 1] && pieces[i + 1] == pieces[i + 2] && pieces[i + 2] == pieces[i + 3])
            {
                winner = pieces[i];
                end = true;
                return;
            }
            i++;
            if (i % 7 == 4)
            {
                i += 3;
            }
        }
    }

    void checkWinVertical()
    {
        int i = 41;
        while (i > 20)
        {
            if (pieces[i] == Coin.empty)
            {
                i--;
                continue;
            }
            if (pieces[i] == pieces[i - 7] && pieces[i - 7] == pieces[i - 14] && pieces[i - 14] == pieces[i - 21])
            {
                winner = pieces[i];
                end = true;
                return;
            }
            i--;
        }
    }

    void checkWinOblique1()
    {
        int i = 0;
        while (i < 42)
        {
            if (i % 7 != 6 && i <= 34 && (i + 8) % 7 != 6 && (i + 8) <= 34 && (i + 16) % 7 != 6 && (i + 16) <= 34)
            {
                if (pieces[i] == Coin.empty)
                {
                    i++;
                    continue;
                }
                if (pieces[i] == pieces[i + 8] && pieces[i + 8] == pieces[i + 16] && pieces[i + 16] == pieces[i + 24])
                {
                    winner = pieces[i];
                    end = true;
                    return;
                }
            }
            i++;
        }
    }

    void checkWinOblique2()
    {
        int i = 0;
        while (i < 42)
        {
            if (i % 7 != 0 && i <= 34 && (i + 6) % 7 != 0 && (i + 6) <= 34 && (i + 12) % 7 != 0 && (i + 12) <= 34)
            {
                if (pieces[i] == Coin.empty)
                {
                    i++;
                    continue;
                }
                if (pieces[i] == pieces[i + 6] && pieces[i + 6] == pieces[i + 12] && pieces[i + 12] == pieces[i + 18])
                {
                    winner = pieces[i];
                    end = true;
                    return;
                }
            }
            i++;
        }
    }

    #endregion


    #region Evaluation

    public int Evaluate()
    {
        return EvaluateHorizontaly() + EvaluateVertically() + EvaluateOblique1() + EvaluateOblique2();
    }

    public int EvaluateHorizontaly()
    {
        int score = 0;
        int i = 0;
        while (i < 42)
        {
            int temp = 0;

            if (pieces[i] == Coin.empty && pieces[i + 1] == pieces[i + 2] && pieces[i + 2] == pieces[i + 3])
            {
                temp = 7;
            }   //check for 3 when first coin is empty

            {
                if (pieces[i + 1] == pieces[i + 2] && pieces[i] == Coin.empty && pieces[i + 3] == Coin.empty)
                {
                    temp = 3;
                }

                if (pieces[i + 1] == pieces[i + 3] && pieces[i] == Coin.empty && pieces[i + 2] == Coin.empty)
                {
                    temp = 3;
                }
                if (pieces[i + 2] == pieces[i + 3] && pieces[i] == Coin.empty && pieces[i + 1] == Coin.empty)
                {
                    temp = 3;
                }
            }   //check for 2 when first is empty
            if (pieces[i + 1] == Coin.yellow || pieces[i + 2] == Coin.yellow)
            {
                score += temp;
            }

            if (pieces[i + 1] == Coin.red || pieces[i + 2] == Coin.red)
            {
                score -= temp;
            }

            if (pieces[i] == Coin.empty)
            {
                i++;
                if (i % 7 == 4)
                {
                    i += 3;
                }
                continue;
            }

            {
                if (pieces[i] == pieces[i + 1] && pieces[i + 1] == pieces[i + 2] && pieces[i + 3] == Coin.empty)
                {
                    temp = 7;
                }

                if (pieces[i] == pieces[i + 1] && pieces[i + 2] == Coin.empty && pieces[i + 1] == pieces[i + 3])
                {
                    temp = 7;
                }

                if (pieces[i] == pieces[i + 2] && pieces[i + 1] == Coin.empty && pieces[i + 2] == pieces[i + 3])
                {
                    temp = 7;
                }


            }   //check for 3 

            {
                if (pieces[i] == pieces[i + 1] && pieces[i + 2] == Coin.empty && pieces[i + 3] == Coin.empty)
                {
                    temp = 3;
                }

                if (pieces[i] == pieces[i + 2] && pieces[i + 1] == Coin.empty && pieces[i + 3] == Coin.empty)
                {
                    temp = 3;
                }

                if (pieces[i] == pieces[i + 3] && pieces[i + 1] == Coin.empty && pieces[i + 2] == Coin.empty)
                {
                    temp = 3;
                }
            }   //check for 2

            score += pieces[i] == Coin.yellow ? temp : -temp;

            i++;
            if (i % 7 == 4)
            {
                i += 3;
            }
        }
        return score;
    }

    public int EvaluateVertically()
    {
        int score = 0;
        int i = 41;
        while (i > 20)
        {
            if (pieces[i] == Coin.empty)
            {
                i--;
                continue;
            }
            if (pieces[i] == pieces[i - 7] && pieces[i - 7] == pieces[i - 14] && pieces[i - 21] == Coin.empty)
            {
                score += pieces[i] == Coin.yellow ? 7 : -7;
            }

            if (pieces[i] == pieces[i - 7] && pieces[i - 14] == Coin.empty && pieces[i - 21] == Coin.empty)
            {
                score += pieces[i] == Coin.yellow ? 3 : -3;
            }
            i--;
        }
        return score;
    }

    public int EvaluateMiddleColumn()
    {
        if (pieces[3] != Coin.empty)
            return 0;
        if (pieces[24] == Coin.empty)
            return 5;
        if (pieces[24] == Coin.red || pieces[17] == Coin.red || pieces[10] == Coin.red)
            return 0;
        return 5;
    }

    public int EvaluateOblique1()
    {
        int score = 0;
        int i = 0;
        while (i < 42)
        {
            int t = 0;
            if (i % 7 != 6 && i <= 34 && (i + 8) % 7 != 6 && (i + 8) <= 34 && (i + 16) % 7 != 6 && (i + 16) <= 34)
            {
                if (pieces[i+8] == pieces[i +16] && pieces[i + 16] == pieces[i + 24] && pieces[i] == Coin.empty)
            {
                t += 7;
            }

            if (pieces[i] == Coin.empty && pieces[i+8] == pieces[i+16] && pieces[i + 24] == Coin.empty)
            {
                t += 3;
            }

            if (pieces[i] == Coin.empty && pieces[i + 8] == pieces[i + 24] && pieces[i + 16] == Coin.empty)
            {
                t += 3;
            }

            if (pieces[i] == Coin.empty && pieces[i + 16] == pieces[i + 24] && pieces[i + 8] == Coin.empty)
            {
                t += 3;
            }

            if (pieces[i] == Coin.empty)
            {
                i++;
                continue;
            }
                //3 in a row
                {
                    if (pieces[i] == pieces[i + 8] && pieces[i + 8] == pieces[i + 16] && pieces[i + 24] == Coin.empty)
                    {
                        t += 7;
                    }
                    if (pieces[i] == pieces[i + 8] && pieces[i + 8] == pieces[i + 24] && pieces[i + 16] == Coin.empty)
                    {
                        t += 7;
                    }
                    if (pieces[i] == pieces[i + 16] && pieces[i + 16] == pieces[i + 24] && pieces[i + 8] == Coin.empty)
                    {
                        t += 7;
                    }
                }

                //2 in a row
                {
                    if (pieces[i] == pieces[i + 8] &&pieces[i + 16] == Coin.empty && pieces[i + 24] == Coin.empty)
                    {
                        t += 3;
                    }

                    if (pieces[i] == pieces[i + 16] && pieces[i + 8] == Coin.empty && pieces[i + 24] == Coin.empty)
                    {
                        t += 3;
                    }

                    if (pieces[i] == pieces[i + 24] && pieces[i + 16] == Coin.empty && pieces[i + 24] == Coin.empty)
                    {
                        t += 3;
                    }
                }
            }
            score += pieces[i] == Coin.yellow ? t : -t;

            i++;
        }
        return score;
    }

    public int EvaluateOblique2()
    {
        int score = 0;
        int i = 0;
        while (i < 42)
        {
            int t = 0;
            if (i % 7 != 0 && i <= 34 && (i + 6) % 7 !=0 && (i +6) <= 34 && (i + 12) % 7 != 0 && (i + 12) <= 34)
            {
                if (pieces[i + 6] == pieces[i + 12] && pieces[i + 12] == pieces[i + 18] && pieces[i] == Coin.empty)
                {
                    t += 7;
                }

                if (pieces[i] == Coin.empty && pieces[i + 6] == pieces[i + 12] && pieces[i + 18] == Coin.empty)
                {
                    t += 3;
                }

                if (pieces[i] == Coin.empty && pieces[i + 6] == pieces[i + 18] && pieces[i + 12] == Coin.empty)
                {
                    t += 3;
                }

                if (pieces[i] == Coin.empty && pieces[i + 12] == pieces[i + 18] && pieces[i + 6] == Coin.empty)
                {
                    t += 3;
                }

                if (pieces[i] == Coin.empty)
                {
                    i++;
                    continue;
                }
                //3 in a row
                {
                    if (pieces[i] == pieces[i + 6] && pieces[i + 6] == pieces[i + 12] && pieces[i + 18] == Coin.empty)
                    {
                        t += 7;
                    }
                    if (pieces[i] == pieces[i + 6] && pieces[i + 6] == pieces[i + 18] && pieces[i + 12] == Coin.empty)
                    {
                        t += 7;
                    }
                    if (pieces[i] == pieces[i + 12] && pieces[i + 12] == pieces[i + 18] && pieces[i + 6] == Coin.empty)
                    {
                        t += 7;
                    }
                }

                //2 in a row
                {
                    if (pieces[i] == pieces[i + 6] && pieces[i + 12] == Coin.empty && pieces[i + 18] == Coin.empty)
                    {
                        t += 3;
                    }

                    if (pieces[i] == pieces[i + 12] && pieces[i +6] == Coin.empty && pieces[i + 18] == Coin.empty)
                    {
                        t += 3;
                    }

                    if (pieces[i] == pieces[i + 18] && pieces[i + 12] == Coin.empty && pieces[i + 18] == Coin.empty)
                    {
                        t += 3;
                    }
                }
            }
            score += pieces[i] == Coin.yellow ? t : -t;
            i++;
        }
        return score;
    }

    #endregion

    public bool ColumnFullAt(int c)
    {
        return places[c] < 0;
    }

    public Board Clone()
    {
        Board board = new Board();
        board.pieces = new Coin[42];
        for (int i = 0; i < pieces.Length; i++)
        {
            board.pieces[i] = pieces[i];
        }

        for(int i = 0; i < 7; i++)
        {
            board.places[i] = places[i];
        }

        board.turn = turn;
        board.winner = winner;
        board.end = end;
        return board;
    }

    public void switchTurns()
    {
        if (turn == Coin.red)
        {
            turn = Coin.yellow;
        }
        else
        {
            turn = Coin.red;
        }
    }

    public enum Coin
    {
        empty = 0,
        red = 1,
        yellow = 2
    }
}
