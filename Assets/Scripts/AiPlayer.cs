using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AiPlayer : MonoBehaviour
{
    public int depth;
    public GameManager manager;
    Ai ai;
    Thread player;

    static int n;

    void Start()
    {
        
    }

    public void play()
    {
        StartCoroutine(_play());
    }

    public void InitializeAi()
    {
        ai = new Ai(depth);
    }

    public IEnumerator _play()
    {
        Node.nb = 0;
        if(ai == null)
        {
            InitializeAi();
        }
        n = ai.getNextPlay(manager.getCurrentBoard());
        Debug.Log($"Ai plays {n}");
        Debug.Log($"Number of nodes created is {Node.nb}");
        Debug.Log($"=> {depth}");
        manager.drop(n);
        yield return new WaitForSeconds(0.1f);
    }
}
