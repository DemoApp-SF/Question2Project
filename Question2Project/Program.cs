using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace Question2Project
{
    class Program
    {
        static void Main(string[] args)
        {         
            ChromeOptions options = new ChromeOptions();

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;

            IWebDriver driver = new ChromeDriver(service, options);
            
            options.AddArgument("--log-level=3");
            options.AddArgument("--silent");   
            
            //Load main page
            driver.Url = "https://www.weightwatchers.com/us/";

            IWebElement FindWorkshop = driver.FindElement(By.XPath("(//a[@da-label='Find a Workshop'])[1]"));
            FindWorkshop.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            IWebElement locationZip = driver.FindElement(By.XPath("//input[contains(@id,'location-search')]"));

            locationZip.SendKeys("10011");
            IWebElement locationsearch = driver.FindElement(By.XPath("(//button[contains(@id,'location-search-cta')])[1]"));
            locationsearch.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement FirstsearchResultTitle = driver.FindElement(By.XPath("(//a[contains(@class,'linkUnderline-1_h4g')])[1]"));
            IWebElement FirstsearchResultDistance = driver.FindElement(By.XPath("(//span[contains(@class,'distance-')])[1]"));

            String ResultTitle = FirstsearchResultTitle.Text;
            String ResultDistance = FirstsearchResultDistance.Text;       


            FirstsearchResultTitle.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement PageTitleElement = driver.FindElement(By.XPath("//h1[contains(@class,'locationName')]"));
            IWebElement PageLocationElement = driver.FindElement(By.XPath("(//div[contains(@class,'address-')])[1]//div"));

            if(ResultTitle != PageTitleElement.Text)
            {
                Console.Write("First search result not selected. Check here!");
                driver.Quit();
            }
            else 
            {
                Console.WriteLine("\n");
                Console.WriteLine("First search Result Title    : " + ResultTitle);
                Console.WriteLine("First search Result Distance : " + ResultDistance +"\n");
               
                IList<IWebElement> AllMettingSchedules = driver.FindElements(By.XPath("//div[contains(@class,'meeting-')]"));
                for(int i=1; i < AllMettingSchedules.Count; i++)
                {                   
                    Console.WriteLine(AllMettingSchedules[i].FindElement(By.XPath("./span[2]")).Text + "\t" + AllMettingSchedules[i].FindElement(By.XPath("./span[contains(@class,'meetingTime-')]")).Text);                  
                }               
            }

            driver.Quit();
        }
    }

    
}
