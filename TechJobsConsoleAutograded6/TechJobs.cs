namespace TechJobsConsoleAutograded6
{
    public class TechJobs
	{
        public void RunProgram()
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new()
            {
                { "search", "Search" },
                { "list", "List" }
            };

            // Column options
            Dictionary<string, string> columnChoices = new()
            {
                { "core competency", "Skill" },
                { "employer", "Employer" },
                { "location", "Location" },
                { "position type", "Position Type" },
                { "all", "All" }
            };

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string? actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice is null)
                {
                    break;
                }
                else if (actionChoice.Equals("list"))
                {
                    string? columnChoice = GetUserSelection("List", columnChoices) ?? "all";

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);

                        Console.WriteLine(Environment.NewLine + "*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string? columnChoice = GetUserSelection("Search", columnChoices) ?? "all";

                    // What is their search term?
                    Console.WriteLine(Environment.NewLine + "Search term: ");
                    string? searchTerm = Console.ReadLine() ?? "x";

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        List<Dictionary<string, string>> searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                    }
                    else
                    {
                        List<Dictionary<string, string>> searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }

            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        public static string? GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                if (choiceHeader.Equals("View Jobs"))
                {
                    Console.WriteLine(Environment.NewLine + choiceHeader + " by (type 'x' to quit):");
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + choiceHeader + " by:");
                }

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                string? input = Console.ReadLine() ?? "x";
                if (input.Equals("x") || input.Equals("X"))
                {
                    return null;
                }
                else
                {
                    choiceIdx = int.Parse(input);
                }

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

        // TODO: complete the PrintJobs method.
        public static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            if (someJobs.Count == 0)
            {
                Console.WriteLine("No results");
            }
            foreach (Dictionary<string, string> job in someJobs)
            {
                Console.WriteLine($"{Environment.NewLine}*****");
                foreach (KeyValuePair<string, string> entry in job)
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
                }
                Console.WriteLine("*****");
            }
        }
    }
}

