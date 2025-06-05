using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum State {init, gaming, win, loss};
    public State GameState = State.init;
    public Timer Timer;
    public TextMeshProUGUI FlagText;
    public BoardManager bmaster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FlagText.text = "Bombs: " + (bmaster.mineCount - bmaster.flagNum).ToString();
    }

    public void ChangeState(State st)
    {
        GameState = st;
    }

    public void Bomb()
    {
        Timer.isRunning = false;
        ChangeState(State.loss);
    }

    public void Win()
    {
        Timer.isRunning = false;
        ChangeState(State.win);
    }

    public void Respawn()
    {
        bmaster.SpawnBoard(bmaster.nowMode);
        Timer.ResetTimer();
        ChangeState(State.init);
    }

    public void StartGame()
    {
        Timer.isRunning = true;
        ChangeState(State.gaming);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("2D_Minesweeper_Menu");
    }
}
