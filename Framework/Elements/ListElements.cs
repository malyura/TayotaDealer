using System;
using System.Collections.Generic;
using OpenQA.Selenium;

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

            return Driver.FindElements(Locator);
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
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(Locator));
        }
    }
}
