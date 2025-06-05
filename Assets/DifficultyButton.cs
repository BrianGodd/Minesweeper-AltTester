using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonEasyClicked()
    {
        SceneManager.LoadScene("2D_Minesweeper_Game1");
    }

    public void OnButtonNormalClicked()
    {
        SceneManager.LoadScene("2D_Minesweeper_Game2");
    }

    public void OnButtonHardClicked()
    {
        SceneManager.LoadScene("2D_Minesweeper_Game3");
    }
}
