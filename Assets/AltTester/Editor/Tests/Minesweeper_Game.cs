using NUnit.Framework;
using AltTester.AltTesterSDK.Driver;

public class Minesweeper_Game
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

    [Test]
    public void GameStateTest()
    {
        //get init game state
        AltObject gameManager = altDriver.FindObject(By.NAME, "GameManager");
        string componentName = "GameManager";
        string propertyName = "GameState";
        string assemblyName = "Assembly-CSharp";
        var propertyValue = gameManager.GetComponentProperty<string>(componentName, propertyName, assemblyName);

        Assert.AreEqual("0", propertyValue);

        //spawn board
        AltObject boardManager = altDriver.FindObject(By.NAME, "BoardManager");
        string methodName = "SpawnBoard";
        componentName = "BoardManager";
        assemblyName = "Assembly-CSharp";
        object[] parameters = new object[] {1};
        var data = boardManager.CallComponentMethod<int>(componentName, methodName, assemblyName, parameters);
        
        //get gaming state
        AltObject gameManagerAfter = altDriver.FindObject(By.NAME, "GameManager");
        componentName = "GameManager";
        propertyName = "GameState";
        var propertyAfter = gameManagerAfter.GetComponentProperty<string>(componentName, propertyName, assemblyName);

        Assert.AreEqual("1", propertyAfter);

    }
}