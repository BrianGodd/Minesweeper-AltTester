using NUnit.Framework;
using AltTester.AltTesterSDK.Driver;
using System.Threading;

public class Minesweeper_UI
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
    public void CellInteractionTest(int mode)
    {
        //click Easy UI 
        if(mode == 1)
        {
            altDriver.LoadScene("2D_Minesweeper_Menu");
            var cells = altDriver.FindObjectsWhichContain(By.NAME, "ButtonEasy");
            cells[0].Click();
            Thread.Sleep(1000);
            Assert.AreEqual("2D_Minesweeper_Game1", altDriver.GetCurrentScene());
        }
        //click Medium UI 
        if(mode == 2)
        {
            altDriver.LoadScene("2D_Minesweeper_Menu");
            var cells = altDriver.FindObjectsWhichContain(By.NAME, "ButtonNormal");
            cells[0].Click();
            Thread.Sleep(1000);
            Assert.AreEqual("2D_Minesweeper_Game2", altDriver.GetCurrentScene());
        }
        //click Hard UI 
        if(mode == 3)
        {
            altDriver.LoadScene("2D_Minesweeper_Menu");
            var cells = altDriver.FindObjectsWhichContain(By.NAME, "ButtonHard");
            cells[0].Click();
            Thread.Sleep(1000);
            Assert.AreEqual("2D_Minesweeper_Game3", altDriver.GetCurrentScene());
        }
    }

    // [TestCase(1)]
    // [TestCase(2)]
    // [TestCase(3)]
    // public void TimerTest(int time)
    // {
    //     //test object
    //     AltObject boardManager = altDriver.FindObject(By.NAME, "GameManager");

    //     //spawn board
    //     string componentName = "Timer";
    //     string methodName = "RunTime";
    //     string assemblyName = "Assembly-CSharp";
    //     object[] parameters = new object[] {1};
    //     var data = boardManager.CallComponentMethod<int>(componentName, methodName, assemblyName, parameters);

    // }
}