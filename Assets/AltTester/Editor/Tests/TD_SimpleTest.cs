using NUnit.Framework;
using AltTester.AltTesterSDK.Driver;

public class TD_SimpleTest
{   //Important! If your test file is inside a folder that contains an .asmdef file, please make sure that the assembly definition references NUnit.
    public AltDriver altDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver =new AltDriver("127.0.0.1", 13000, "__default__");
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [TestCase(0.5f)]
    [TestCase(1.0f)]
    [TestCase(3.0f)]
    public void MoveTest(float moveTime)
    {
        //載入場景並等待完成
        altDriver.LoadScene("2D_Template");
        altDriver.WaitForCurrentSceneToBe("2D_Template");

        //取得名為"Player"的物件與其世界座標位置
        AltObject player = altDriver.WaitForObject(By.NAME, "Player");
        AltVector3 originalPosition = player.GetWorldPosition();

        //Debug Message
        UnityEngine.Debug.Log("[TestMove] Original Position: " + originalPosition.ToString());
        
        //按下D鍵->等待->放開D鍵
        altDriver.KeyDown(AltKeyCode.D);
        System.Threading.Thread.Sleep((int)(1000* moveTime)); // 等待角色移動
        altDriver.KeyUp(AltKeyCode.D);

        //再次取得一次世界座標位置
        player = altDriver.FindObject(By.NAME, "Player");
        AltVector3 newPosition = player.GetWorldPosition();

        //Debug Message
        UnityEngine.Debug.Log("[TestMove] New Position: " + newPosition.ToString());
        
        //測試其有向右移動
        Assert.Greater(newPosition.x, originalPosition.x, "角色沒有向右移動");

    }

}