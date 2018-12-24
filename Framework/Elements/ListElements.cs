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
            var textFromElements = new List<string>();

            foreach (var element in GetElements())
            {
                WaitElementIsVisible();
                textFromElements.Add(element.Text.Trim());
            }

            return textFromElements;
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
