using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecflowCodedUiTests
{
    [Binding]
    public class AddSteps
    {
        private readonly ApplicationUnderTest _aut;

        public AddSteps()
        {
            _aut = ApplicationUnderTest.Launch(@"C:\Windows\System32\calc.exe");
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            Keyboard.SendKeys(_aut, NumbersToSendKeysString(p0));
        }

        [Given(@"I press add")]
        public void GivenIPressAdd()
        {
            Keyboard.SendKeys(_aut, "{Add}");
        }

        [When(@"I press enter")]
        public void WhenIPressEnter()
        {
            Keyboard.SendKeys(_aut, "{Enter}");
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int result)
        {
            WinText resultTextBox = new WinText(_aut);
            resultTextBox.SearchProperties[UITestControl.PropertyNames.Name] = "Result";

            Assert.AreEqual(result.ToString(CultureInfo.InvariantCulture), resultTextBox.DisplayText);
        }

        protected string NumbersToSendKeysString(int number)
        {
            StringBuilder result = new StringBuilder();
            char[] numbers = number.ToString(CultureInfo.InvariantCulture).ToCharArray();

            foreach (char c in numbers)
            {
                result.AppendFormat("{{NumPad{0}}}", c);
            }

            return result.ToString();
        }
    }
}
