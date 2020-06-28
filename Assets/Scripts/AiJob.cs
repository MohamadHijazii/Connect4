using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class AiJob :MonoBehaviour, IJob
{
    public int depth;
    public GameManager manager;
    Ai ai;

    void Start()
    {
        ai = new Ai(depth);

    }

    public void Execute()
    {
        int n = ai.getNextPlay(manager.getCurrentBoard());
        Debug.Log($"Ai plays {n}");
        manager.drop(n);
    }
}
