using NUnit.Framework;
using AltTester.AltTesterSDK.Driver;

public class Trd_SimpleTest
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
    public void ThrowTest()
    {
        //點擊螢幕
        AltVector2 originalPosition = new AltVector2(0.5f, 0.5f);
	    altDriver.Click(originalPosition);

        //取得UI並確認其存在
        var altElement = altDriver.FindObject(By.NAME, "Text (TMP)");
        Assert.NotNull(altElement);

        //取得UI的Text文字內容
        const string componentName = "TMPro.TextMeshProUGUI";
        const string propertyName = "text";
        var propertyValue = altElement.WaitForComponentProperty<string>
        (componentName, propertyName, "Total : 1", "Unity.TextMeshPro", timeout : 2);

        //Debug Message
        UnityEngine.Debug.Log("[Test] propertyValue: " + propertyValue.ToString());

        //測試是否丟出一個PaperBall時有增加數量
        Assert.AreEqual("Total : 1", propertyValue);
    }

}