using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai
{
    public int depth;
    public Node root;

    public Ai(int d)
    {
        depth = d;
    }


    public int getNextPlay(Board board)
    {
        root = new Node(null,board, false);
        root.BuildTree(depth);
        GameManager.log("--> " + depth);
        return root.getMaxChild();
    }
}
