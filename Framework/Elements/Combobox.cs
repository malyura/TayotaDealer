using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Framework.Elements
{
    public class ComboBox : BaseElement
    {
        public ComboBox(By locator, string description) : base(locator, description)
        {
        }

        public void SelectItemByIndex(int index)
        {
            var indXpath = index + 1;
            logger.Info("Select " + description + " index " + indXpath);
            var select = new SelectElement(GetElement());
            select.SelectByIndex(indXpath);
        }

        public void SelectItemByText(string text)
        {
            logger.Info("Select " + description + " item " + text);
            var select = new SelectElement(GetElement());
            select.SelectByText(text);
        }

        public string GetTextFromSelectedItem()
        {
            var select = new SelectElement(GetElement());
            return select.SelectedOption.Text;
        }
    }
}
