using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public Animator animator;

    public void startThinking()
    {
        animator.SetBool("think", true);
    }

    public void stopThinking()
    {
        animator.SetBool("think", false);

    }

    public void win()
    {
        animator.SetInteger("endgame", 1);
    }

    public void lose()
    {
        animator.SetInteger("endgame", -1);
    }

    public void draw()
    {
        animator.SetInteger("endgame", -1);
    }
}
