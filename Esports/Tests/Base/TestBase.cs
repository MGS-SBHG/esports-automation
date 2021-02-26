using Framework;
using Framework.Selenium;
using League.Com;
using NUnit.Framework;

// TestBase and Configuration

// We're calling our DriverFactory.Build() method whenever we initialize a new Driver, and that's done in our TestBase class. 
// This class has a Setup method, which is run before every test, and a Cleanup method, which is run after every test. 
// Notice how we pass in a type and browser into our Driver.Init() method? 
// Those are the values that determine the driver configuration and are located in an XML settings file called .runsettings 
// that NUnit uses in its TestContext class.

// Be default, we are using a remote chrome driver, 
// but we can easily change these values and have them affect all of our test suites.


namespace Tests.Base
{
    public abstract class TestBase
    {
        protected TestBase()
        {
            FW.Init();
        }

        [SetUp]
        public virtual void Setup()
        {
            Driver.Init(
                type: FW.Config.Driver.Type,
                browser: FW.Config.Driver.Browser,
                setWait: FW.Config.Driver.Wait
            );

            Esports.Init();
        }

        [TearDown]
        public virtual void Cleanup()
        {
            Driver.Quit();
        }
    }
}
