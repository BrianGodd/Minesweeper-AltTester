using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectManager : MonoBehaviour
{
    public TextMeshProUGUI total, now, prob;
    public List<GameObject> Inside_OBJ = new List<GameObject>();
    public int Inside_Num = 0;
    public int totalNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Inside_Num = Inside_OBJ.Count;

        total.text = "Total : " + totalNum.ToString();
        now.text = "Gocha! : " + Inside_Num.ToString();
        prob.text = (((float)Inside_Num/(float)totalNum)*100).ToString() + "%";
    }

    public void AddOBJ(GameObject obj)
    {
        if(!Inside_OBJ.Contains(obj))
            Inside_OBJ.Add(obj);
    }

    public void RemoveOBJ(GameObject obj)
    {
        if(Inside_OBJ.Contains(obj))
            Inside_OBJ.Remove(obj);
    }
}
