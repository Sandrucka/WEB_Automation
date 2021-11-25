using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System;


namespace HomeworkProject
{

    public class Tests
    {
        
        private IWebDriver Driver;

        private string username = "logname@logname.com";
        private string password = "password";
        private string item_name = "dress";

        [SetUp]
        public void Setup()
        {
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
        }

        [Test]
        public void TestSignIn()
        {
            DoLogin();

            IWebElement logOff = Driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div:nth-child(2) > a"));
            Assert.AreEqual("Sign out", logOff.Text, "Error message:button is not visible on the page");
            //1. Expected value, 2. Actual result

            IWebElement dresses_btn = Driver.FindElement(By.CssSelector("#block_top_menu > ul > li:nth-child(2) > a"));
            dresses_btn.Click();

            IWebElement summer_dresses_btn = Driver.FindElement(By.CssSelector("#categories_block_left > div > ul > li.last > a"));
            summer_dresses_btn.Click();

            IWebElement summer_dress_list = Driver.FindElement(By.CssSelector("#center_column > ul"));//<table>, <body>
            IList<IWebElement> summer_dress_list_grid = summer_dress_list.FindElements(By.TagName("li"));

            foreach (IWebElement data in summer_dress_list_grid)
            {
                Console.WriteLine(data.Text);
            }
        }

        [Test]
        public void TestItemSearch()
        {
            DoLogin();

            DoItemSearch();
        }

        //------------------------------------------------------------------------------------------
        public void DoLogin()
        {
            Driver.FindElement(By.ClassName("login")).Click();
            // IWebElement login_btn = Driver.FindElement(By.Id("loginLink"))
            // login_btn.Click();

            IWebElement email_txt = Driver.FindElement(By.CssSelector("#email"));
            email_txt.SendKeys(username);

            IWebElement password_txt = Driver.FindElement(By.CssSelector("#passwd"));
            password_txt.SendKeys(password);

            IWebElement SignIn_btn = Driver.FindElement(By.CssSelector("#SubmitLogin > span"));
            SignIn_btn.Click();
        }

        public void DoItemSearch()
        {
            
            IWebElement search_field = Driver.FindElement(By.Id("search_query_top"));
            search_field.SendKeys(item_name);

            IWebElement search_btn = Driver.FindElement(By.CssSelector("#searchbox > button"));
            search_btn.Click();

            IWebElement found_item = Driver.FindElement(By.CssSelector("#center_column > ul > li:nth-child(1) > div > div.right-block > h5 > a"));
            string item = found_item.Text;
            bool does_item_match = item.Contains(item_name);
            Console.WriteLine("Item we looked for: " + item_name);
            Console.WriteLine("Item found: " + item);

        }
    }
}