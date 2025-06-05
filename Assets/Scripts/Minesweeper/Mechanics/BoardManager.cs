using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public List<int> BoardList = new List<int>();
    public List<int> CellList = new List<int>(); //0: Unopen, 1: Open, 2: Flag, 3: Explosion
    public List<GameObject> CellObjList = new List<GameObject>();
    public int width;
    public int height;
    public int mineCount;
    public int placedMines;

    public GameObject CellPrefab;
    public Transform Board;
    public float cellWidth;
    public float cellHeight;

    public GameManager GMaster;
    public int revelNum = 0;
    public int flagNum = 0;
    public int nowMode = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBoard(nowMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBoard(int mode = 1) //1:9*9, 10, 2:16*16, 40, 3:30*16, 99, 4:for testing
    {
        revelNum = 0;
        flagNum = 0;
        CellObjList.Clear();

        Debug.Log($"spawn {mode}");
        switch (mode)
        {
            case 1:
                width = 9;
                height = 9;
                mineCount = 10;
                cellWidth = 36.36f;
                cellHeight = -33.3f;
                break;
            case 2:
                width = 16;
                height = 16;
                mineCount = 40;
                cellWidth = 36.36f* 0.7f;
                cellHeight = -33.3f*0.7f;
                break;
            case 3:
                width = 30;
                height = 16;
                mineCount = 99;
                cellWidth = 36.36f* 0.5f;
                cellHeight = -33.3f* 0.5f;
                break;
            case 4:
                width = 16;
                height = 16;
                mineCount = 40;
                cellWidth *= 0.7f;
                cellHeight *= 0.7f;
                break;
            default:
                width = 9;
                height = 9;
                mineCount = 10;
                cellWidth *= 1;
                cellHeight *= 1;
                break;
        }

        // initial board
        int totalCells = width * height;
        BoardList = new List<int>(new int[totalCells]);

        // 放置地雷：-1 為地雷
        placedMines = 0;
        System.Random rand = new System.Random();
        while (placedMines < mineCount)
        {
            int index = rand.Next(0, totalCells);
            if (BoardList[index] != -1)
            {
                BoardList[index] = -1;
                placedMines++;
            }
        }

        // 計算非地雷格周圍地雷數量
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = GetIndex(x, y);
                if (BoardList[index] == -1)
                    continue;

                int count = GetCount(x, y);
                BoardList[index] = count;
            }
        }

        // initial cell with unopen state
        CellList = new List<int>(new int[totalCells]);
        for(int i = 0;i<totalCells;i++) CellList[i] = 0;

        // spawn cells UI
        foreach(Transform obj in Board) Destroy(obj.gameObject);
        for(int y = 0;y<height;y++)
        {
            for(int x = 0;x<width;x++)
            {
                GameObject cell = Instantiate(CellPrefab);
                cell.transform.SetParent(Board, false);
                cell.name = "Cell_" + GetIndex(x, y).ToString();
                cell.transform.localPosition = new Vector3(x*cellWidth, y*cellHeight, 0);
                if(mode == 1) cell.transform.localScale *= 1;
                else if(mode == 2 || mode == 4) cell.transform.localScale *= 0.7f;
                else if(mode == 3) cell.transform.localScale *= 0.5f;
                CellObjList.Add(cell);
            }
        }

        // start game, turn game state to "gaming"
        // GMaster.ChangeState(GameManager.State.gaming);
    }

    int GetIndex(int x, int y)
    {
        return y * width + x;
    }

    bool IsInside(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    public int GetCount(int x, int y)
    {
        int count = 0;
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                    continue;
                int nx = x + dx;
                int ny = y + dy;
                if (IsInside(nx, ny) && BoardList[GetIndex(nx, ny)] == -1)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public void RecursiveActive(int ind)
    {
        int x = ind%width;
        int y = ind/height;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                    continue;
                int nx = x + dx;
                int ny = y + dy;
                if (IsInside(nx, ny) && BoardList[GetIndex(nx, ny)] != -1)
                {
                    CellObjList[GetIndex(nx, ny)].GetComponent<CellManager>().RevealTile();
                }
            }
        }
    }

    public int GetState(int x, int y)
    {
        if(x >= width && y >= height) return 0;
        return CellList[GetIndex(x, y)];
    }

    public void CheckBoard()
    {
        if(revelNum + mineCount == width * height) GMaster.Win();
    }
}
