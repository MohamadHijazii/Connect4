using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPlayer : MonoBehaviour
{
    public int depth;
    public GameManager manager;
    Ai ai;

    void Start()
    {
        ai = new Ai(depth);
    }

    public void play()
    {
        int n = ai.getNextPlay(manager.getCurrentBoard());
        Debug.Log($"Ai plays {n}");
        manager.drop(n);
    }
}
