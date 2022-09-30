using RabotUpdateManager.Abstractions;
using SSD1306;
using SSD1306.Fonts;
using SSD1306.I2CPI;

namespace RabotUpdateManager.Managers
{
    public class DisplayManager : IDisplayManager
    {
        private Display display;

        public void test()
        {
        //TODO not working yet
        //    using (var i2cBus = new I2CBusPI("/dev/i2c-1"))
        //    {
        //        var i2cDevice = new I2CDevicePI(i2cBus, Display.DefaultI2CAddress);

        //        display = new SSD1306.Display(i2cDevice, 128, 32);
        //        display.Init();

        //        var dfont = new Tahmona12();

        //        display.WriteLineBuff(dfont, "Hello World 123456", "Line 2 xxxx");
        //        display.DisplayUpdate();
        //    }
        }
    }
}
