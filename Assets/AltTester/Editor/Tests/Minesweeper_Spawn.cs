using NUnit.Framework;
using AltTester.AltTesterSDK.Driver;
using System;

public class Minesweeper_Spawn
{   //Important! If your test file is inside a folder that contains an .asmdef file, please make sure that the assembly definition references NUnit.
    public AltDriver altDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver =new AltDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void SpawnTest(int mode)
    {

        //test object
        AltObject boardManager = altDriver.FindObject(By.NAME, "BoardManager");
        Assert.NotNull(boardManager, "BoardManager should be present in the scene");

        //test board size
        string componentName = "BoardManager";
        string methodName = "SpawnBoard";
        string propertyName = "BoardList.Count";
        string assemblyName = "Assembly-CSharp";
        object[] parameters = new object[] {mode};
        int AnsExpected = 0;
        if(mode == 1) AnsExpected = 81;
        else if(mode == 2) AnsExpected = 256;
        else if(mode == 3) AnsExpected = 480;
        var data = boardManager.CallComponentMethod<int>(componentName, methodName, assemblyName, parameters);
        var propertyValue = boardManager.GetComponentProperty<int>(componentName, propertyName, assemblyName);

        Assert.AreEqual(AnsExpected, propertyValue);

        //test bomb number
        propertyName = "placedMines";
        if(mode == 1) AnsExpected = 10;
        else if(mode == 2) AnsExpected = 40;
        else if(mode == 3) AnsExpected = 99;
        propertyValue = boardManager.GetComponentProperty<int>(componentName, propertyName, assemblyName);

        Assert.AreEqual(AnsExpected, propertyValue);
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void SpawnUITest(int mode)
    {
        //test object
        AltObject boardManager = altDriver.FindObject(By.NAME, "BoardManager");

        //spawn board
        string componentName = "BoardManager";
        string methodName = "SpawnBoard";
        string assemblyName = "Assembly-CSharp";
        object[] parameters = new object[] {mode};
        int AnsExpected = 0;
        if(mode == 1) AnsExpected = 81;
        else if(mode == 2) AnsExpected = 256;
        else if(mode == 3) AnsExpected = 480;
        var data = boardManager.CallComponentMethod<int>(componentName, methodName, assemblyName, parameters);
        
        //test spawn UI cells number
        var cells = altDriver.FindObjectsWhichContain(By.NAME, "Cell");
        Assert.AreEqual(AnsExpected, cells.Count);
        
        //test UI cells position
        string propertyName = "width";
        var propertyValue = boardManager.GetComponentProperty<int>(componentName, propertyName, assemblyName);
        var cellPos = cells[0].GetComponentProperty<dynamic>("UnityEngine.RectTransform", "position", "UnityEngine.CoreModule");
        var cellPosW = cells[1].GetComponentProperty<dynamic>("UnityEngine.RectTransform", "position", "UnityEngine.CoreModule");
        var cellPosH = cells[propertyValue].GetComponentProperty<dynamic>("UnityEngine.RectTransform", "position", "UnityEngine.CoreModule");
        
        //test cellWidth
        propertyName = "cellWidth";
        var cellWidth = boardManager.GetComponentProperty<float>(componentName, propertyName, assemblyName);
        Assert.AreEqual(Math.Round((double)cellWidth, 2), Math.Round((double)(cellPosW["x"] - cellPos["x"]), 2));

        //test cellHeight
        propertyName = "cellHeight";
        var cellHeight = boardManager.GetComponentProperty<float>(componentName, propertyName, assemblyName);
        Assert.AreEqual(Math.Round((double)cellHeight, 2), Math.Round((double)(cellPosH["y"] - cellPos["y"]), 2));
    }

    [TestCase(2, 2)]
    [TestCase(0, 0)] //edge case
    [TestCase(999, 999)] //edge case
    public void CellNumTest(int x, int y)
    {
        //test object
        AltObject boardManager = altDriver.FindObject(By.NAME, "BoardManager");

        //spawn board
        string componentName = "BoardManager";
        string methodName = "SpawnBoard";
        string assemblyName = "Assembly-CSharp";
        object[] parameters = new object[] {1};
        var data = boardManager.CallComponentMethod<int>(componentName, methodName, assemblyName, parameters);

        //test cell represent number
        string methodToVerifyName = "GetCount";
        object[] pos = new object[] {x, y};
        var cellCount =  boardManager.CallComponentMethod<int>(componentName, methodToVerifyName, assemblyName, pos);
        int A = 0, B = 8;
        Assert.IsTrue(cellCount >= A && cellCount <= B);
        UnityEngine.Debug.Log("[TestCellNum] cellCount: " + cellCount.ToString());
    }

    [TestCase(2, 2)]
    [TestCase(999, 999)] //edge case
    public void InitialCellStateTest(int x, int y)
    {
        //test object
        AltObject boardManager = altDriver.FindObject(By.NAME, "BoardManager");

        //spawn board
        string componentName = "BoardManager";
        string methodName = "SpawnBoard";
        string assemblyName = "Assembly-CSharp";
        object[] parameters = new object[] {1};
        var data = boardManager.CallComponentMethod<int>(componentName, methodName, assemblyName, parameters);

        //cell record count
        string propertyName = "CellList.Count";
        var propertyValue = boardManager.GetComponentProperty<int>(componentName, propertyName, assemblyName);
        Assert.AreEqual(81, propertyValue);

        //check target cell state
        string methodToVerifyName = "GetState";
        object[] pos = new object[] {x, y};
        var cellCount =  boardManager.CallComponentMethod<int>(componentName, methodToVerifyName, assemblyName, pos);
        Assert.AreEqual(0, cellCount);
    }

}