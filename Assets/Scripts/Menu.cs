using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public int difficulty;
    public bool player_go_first = true;

    public Button easy, medium, hard, goFirst;
    public Sprite empty, check;

    private void Start()
    {
        Image im;
        im = easy.GetComponent<Image>();
        im.sprite = check;
        im = medium.GetComponent<Image>();
        im.sprite = empty;
        im = hard.GetComponent<Image>();
        im.sprite = empty;
        im = goFirst.GetComponent<Image>();
        im.sprite = check;

        player_go_first = true;
        difficulty = 1;
    }

    public void checkEz()
    {
        Image im;
        im = easy.GetComponent<Image>();
        im.sprite = check;
        im = medium.GetComponent<Image>();
        im.sprite = empty;
        im = hard.GetComponent<Image>();
        im.sprite = empty;
        difficulty = 1;
    }

    public void checkMedium()
    {
        Image im;
        im = easy.GetComponent<Image>();
        im.sprite = empty;
        im = medium.GetComponent<Image>();
        im.sprite = check;
        im = hard.GetComponent<Image>();
        im.sprite = empty;
        difficulty = 3;

    }

    public void checkHard()
    {
        Image im;
        im = easy.GetComponent<Image>();
        im.sprite = empty;
        im = medium.GetComponent<Image>();
        im.sprite = empty;
        im = hard.GetComponent<Image>();
        im.sprite = check;
        difficulty = 6;

    }

    public void ToggleFirst()
    {
        player_go_first = !player_go_first;
        Image im;
        im = goFirst.GetComponent<Image>();
        im.sprite = player_go_first ? check : empty;
    }

    public void play()
    {
        GameManager.playerStartsFirst = player_go_first;
        GameManager.difficulty = difficulty;
        SceneManager.LoadScene(1);
    }
}
