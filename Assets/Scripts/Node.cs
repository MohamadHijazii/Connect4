using System;
using System.Collections;
using System.Collections.Generic;

public class Node
{
    public static int nb = 0;

    public Board board;
    public List<Node> childs;
    public bool isLeaf;
    public int score;
    public Node parent;
    int alpha, beta;

    public Node(Node parent,Board board, bool isLeaf)
    {
        this.board = board;
        this.isLeaf = isLeaf;
        childs = new List<Node>();
        this.parent = parent;
        alpha = int.MinValue;
        beta = int.MaxValue;
        score = 0;
        nb++;
    }

    #region Evaluation

    public void Evaluate()
    {
        score += board.Evaluate();
        if (board.end)
        {
            if (board.winner == Board.Coin.red)
            {
                score -= 50;
            }
            if (board.winner == Board.Coin.yellow)
            {
                score += 75;
            }
        }
    }

    public void CreateFinalLevel(bool ismax)
    {
        for (int i = 0; i < 7; i++)
        {
            score = ismax ? int.MinValue : int.MaxValue;
            Node child = new Node(this,board.Clone(), true);
            childs.Add(child);
            if (i == 3)
            {
                child.score += ismax ? child.board.EvaluateMiddleColumn():0;
            }
            child.board.play(i);
            child.Evaluate();
            if (ismax) //max
            {
                if (child.score > score)
                {
                    score = child.score;
                    alpha = score;
                }
            }
            else
            {       //min

                if (child.score < score)
                {
                    score = child.score;
                    beta = score;
                }
            }
            if(alpha > beta)
            {
                return;
            }
        }

        if (ismax)
        {
            if(parent != null)
                parent.beta = alpha;
        }
        else
        {
            if (parent != null)
                parent.alpha = beta;
        }
    }

    public int getMaxChild()
    {
        int index = 0;
        int max;
        int i = 0;
        while (board.ColumnFullAt(index))
        {
            index++;
        }
        try
        {
            max = childs[index].score;
            for (i = index + 1; i < 7; i++)
            {
                //GameManager.log($"Score at {i} is {childs[i].score}");
                if (!board.ColumnFullAt(i) && childs[i].score > max)
                {
                    index = i;
                    max = childs[i].score;
                }


            }
        }catch(Exception ex)
        {
            GameManager.log($"error at i = {i}, while max is {childs.Count}");
        }
        return index;
    }

    public int Min()
    {
        int m = int.MaxValue;
        foreach (var c in childs)
        {
            if (c.score < m)
            {
                m = c.score;
            }
        }
        return m;
    }

    public int Max()
    {
        int m = int.MinValue;
        foreach (var c in childs)
        {
            if (c.score > m)
            {
                m = c.score;
            }
        }
        return m;
    }

    #endregion

    public void BuildTree(int depth)
    {
        BuildTree(depth, depth);
    }

    private void BuildTree(int depth, int maxDepth)       //call it with same values for both
    {
        if (isLeaf)
        {
            Evaluate();
            return;
        }

        if (depth == 1)     //i am at the last level
        {
            CreateFinalLevel((maxDepth - depth) % 2 == 0);
            //score = (maxDepth - depth) % 2 == 0 ? Max() : Min();
            return;
        }
        else
        {
            score = (maxDepth - depth) % 2 == 0? int.MinValue : int.MaxValue;
            for (int i = 0; i < 7; i++)
            {
                Node child = new Node(this,board.Clone(), false);
                childs.Add(child);
                child.alpha = alpha;
                child.beta = beta;
                if (child.board.ColumnFullAt(i))
                {
                    child.isLeaf = true;
                }
                if (!child.isLeaf)
                {
                    child.board.play(i);
                }
                child.BuildTree(depth - 1, maxDepth);
                if ((maxDepth - depth) % 2 == 0) //max
                {
                    if(child.score > score)
                    {
                        score = child.score;
                        alpha = score;

                    }
                }
                else{       //min

                    if (child.score < score)
                    {
                        score = child.score;
                        beta = score;
                    }
                }
                if (alpha > beta)
                {
                    return;
                }
            }
            if ((maxDepth - depth) % 2 == 0)
            {
                if(parent != null)
                    parent.beta = alpha;
            }
            else
            {
                if (parent != null)
                    parent.alpha = beta;
            }
            //score = (maxDepth - depth) % 2 == 0 ? Max() : Min();
            //GameManager.log($"Score: {score}");
        }

    }


    public static int sumOf7(int depth)
    {
        int s = 0;
        for(int i = 1; i <= depth; i++)
        {
            s += (int)Math.Pow(7,i);
        }

        return s;
    }
}