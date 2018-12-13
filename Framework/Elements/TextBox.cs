using OpenQA.Selenium;

namespace Framework.Elements
{
    public class TextBox : BaseElement
    {
        public TextBox(By locator, string description) : base(locator, description)
        {
        }

        public void Type(string text)
        {
            WaitElementIsVisible();
            logger.Info(string.Format("Typing '{0}'", text));
            GetElement().SendKeys(text);
        }
    }
}
