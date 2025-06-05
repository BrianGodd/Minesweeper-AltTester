using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CellManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject Number, Frame, Flag, Bomb;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            RevealTile();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            ToggleFlag();
        }
    }

    public void RevealTile()
    {
        int ind = int.Parse(this.gameObject.name.Substring(5, this.gameObject.name.Length-5));
        GameObject bmaster = GameObject.Find("BoardManager");

        //if loss, cannot click
        if(bmaster.GetComponent<BoardManager>().GMaster.GameState != GameManager.State.gaming && 
            bmaster.GetComponent<BoardManager>().GMaster.GameState != GameManager.State.init) return;

        //start game, turn game state to "gaming"
        if(!bmaster.GetComponent<BoardManager>().GMaster.Timer.isRunning) 
            bmaster.GetComponent<BoardManager>().GMaster.StartGame();

        if(bmaster.GetComponent<BoardManager>().CellList[ind] == 1) return;

        if(bmaster.GetComponent<BoardManager>().BoardList[ind] == -1)
        {
            bmaster.GetComponent<BoardManager>().CellList[ind] = 3;
            GameObject gmaster = GameObject.Find("GameManager");
            gmaster.GetComponent<GameManager>().Bomb();
            Bomb.SetActive(true);
        }
        else
        {
            bmaster.GetComponent<BoardManager>().CellList[ind] = 1;
            Frame.SetActive(true);
            bmaster.GetComponent<BoardManager>().revelNum ++;
            if(bmaster.GetComponent<BoardManager>().BoardList[ind] != 0)
            {
                Number.SetActive(true);
                Number.GetComponent<TextMeshProUGUI>().text = bmaster.GetComponent<BoardManager>().BoardList[ind].ToString();
            }
            else
            {
                bmaster.GetComponent<BoardManager>().RecursiveActive(ind);
            }
            //check win
            bmaster.GetComponent<BoardManager>().CheckBoard();
        }
    }

    public void ToggleFlag()
    {
        int ind = int.Parse(this.gameObject.name.Substring(5, this.gameObject.name.Length-5));
        GameObject bmaster = GameObject.Find("BoardManager");
        if(bmaster.GetComponent<BoardManager>().CellList[ind] == 1) return;
        if(Flag.active)
        {
            bmaster.GetComponent<BoardManager>().CellList[ind] = 0;
            Flag.SetActive(false);
            bmaster.GetComponent<BoardManager>().flagNum --;
        }
        else
        {
            bmaster.GetComponent<BoardManager>().CellList[ind] = 2;
            Flag.SetActive(true);
            bmaster.GetComponent<BoardManager>().flagNum ++;
        }
    }
}
