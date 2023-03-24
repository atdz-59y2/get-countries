using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GetCountry
{
    public class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--incoginto");
            options.AddArguments("--start-maximized");
            ChromeDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://countrycode.org/");
            List<string> values = new List<string>();
            for(int i = 1; i < 241; i++)
            {
                string xpath = String.Format("/html[1]/body[1]/div[3]/div[1]/div[1]/div[2]/div[2]/table[1]/tbody[1]/tr[{0}]", i);
                IWebElement webElement = driver.FindElement(By.XPath(xpath));
                string result = webElement.GetAttribute("textContent");
                values.Add(result);
            }
            string DirectoryPath = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            string FileName = "country.txt";
            string FilePath = Path.Combine(DirectoryPath, FileName);
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }
            using(StreamWriter sw = new StreamWriter(FilePath))
            {
                foreach(var value in values)
                {
                    string name = "";
                    for(int i = 0; i < value.Length; i++)
                    {
                        if (!char.IsDigit(value[i]))
                        {
                            name += value[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    string cutString = value.Split("/")[1];
                    string shortName = "";
                    for(int i = 1; i < cutString.Length; i++)
                    {
                        if (!char.IsDigit(cutString[i]))
                        {
                            shortName += cutString[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    sw.WriteLine(String.Format("{0}, {1}", name, shortName));
                }
            }
            driver.Quit();
            Console.WriteLine("Done!");
        }
    }
}