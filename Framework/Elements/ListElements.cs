using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Framework.Elements
{
    public class ListElements : BaseElement
    {
        public ListElements(By locator, string description) : base(locator, description)
        {
        }

        public IList<IWebElement> GetElements()
        {
            WaitListElementsIsVisible();
            return driver.FindElements(locator);
        }

        public List<string> GetTextFromListElements()
        {
            var lstText = new List<string>();
            foreach (var el in GetElements())
            {
                WaitElementIsVisible();
                lstText.Add(el.Text.Trim());
            }

            return lstText;
        }

        public IWebElement GetElementFromList(int index)
        {
            return GetElements()[index];
        }

        public int GetRandomIndexListElements()
        {
            WaitListElementsIsVisible();
            return new Random().Next(0, GetElements().Count);
        }

        private void WaitListElementsIsVisible()
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }
    }
}
