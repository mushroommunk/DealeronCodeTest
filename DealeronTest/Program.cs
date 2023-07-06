using DealeronTest.Common;
using DealeronTest.Localizations;
using System.Globalization;

namespace DealeronTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SetLanguageChanges();
            var keepRunning = true;

            do
            {
                if (!Mars.GetGridAssigned())
                {
                    Console.Write(Prompts.Grid);
                }

                var input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    input = input.Trim().ToLower();
                    if (input.Equals(Prompts.Reset))
                    {
                        Mars.Reset();
                    }
                    else if (input.Equals(Prompts.Run))
                    {
                        Mars.Run();
                    }
                    else if (input.Equals(Prompts.Language))
                    {
                        ChangeLanguage();
                    }
                    else if (input.Equals(Prompts.End))
                    {
                        keepRunning = false;
                    }
                    else
                    {
                        Mars.ProcessInput(input);
                    }
                }
            } while (keepRunning);
        }

        private static void ChangeLanguage()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write(Prompts.LanguagePrompt);

            var newLanguageCode = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newLanguageCode))
            {
                Console.WriteLine(Prompts.LanguageCodeError);
            }
            else
            {

                try
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(newLanguageCode);
                    SetLanguageChanges();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(Prompts.PressEnter);
                    Console.ReadLine();
                }
            }
            Mars.Reset();
        }

        private static void SetLanguageChanges()
        {
            Console.Title = Prompts.Title;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(Prompts.Menu);
        }
    }
}