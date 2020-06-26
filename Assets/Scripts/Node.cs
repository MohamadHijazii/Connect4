using System.Collections;
using System.Collections.Generic;

public class Node
{
    public Board board;
    public List<Node> childs;
    public bool isLeaf;
    public int score;

    public Node(Board board,bool isLeaf)
    {
        this.board = board;
        this.isLeaf = isLeaf;
        childs = new List<Node>();
        score = 0;
    }

    #region Evaluation

    public void Evaluate()
    {
        score += board.Evaluate();
        if (board.end)
        {
            if(board.winner == Board.Coin.red)
            {
                score -= 50;
            }
            if(board.winner == Board.Coin.yellow)
            {
                score += 75;
            }
        }
    }

    public void CreateFinalLevel()
    {
        for(int i = 0; i < 7; i++)
        {
            Node child = new Node(board.Clone(), true);
            childs.Add(child);
            if(i == 3)
            {
                child.score += child.board.EvaluateMiddleColumn();
            }
            child.board.play(i);
            child.Evaluate();
        }
    }

    public int getMaxChild()
    {
        int index = 0;
        int max;
        while (board.ColumnFullAt(index))
        {
            index++;
        }
        max = childs[index].score;
        for (int i = index+1; i < 7; i++)
        {
            GameManager.log($"Score at {i} is {childs[i].score}");
            if(childs[i].score > max && !board.ColumnFullAt(i))
            {
                index = i;
                max = childs[i].score;
            }
        }
        return index;
    }
    
    public int Min()
    {
        int m = int.MaxValue;
        foreach(var c in childs)
        {
            if(c.score < m)
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

    private void BuildTree(int depth,int maxDepth)       //call it with same values for both
    {
        if(depth == 1)     //i am at the last level
        {
            CreateFinalLevel();

            return;
        }
        else
        {
            for(int i = 0; i < 7; i++)
            {
                Node child = new Node(board.Clone(), false);
                if (child.board.ColumnFullAt(i))
                {
                    child.isLeaf = true;
                }
                if (!child.isLeaf)
                {
                    child.board.play(i);
                }
                child.BuildTree(depth - 1, maxDepth);
                child.Evaluate();
                childs.Add(child);
            }

            score = (maxDepth - depth) % 2 == 0 ? Max() : Min();
        }

    }

}
