using OF.FSimMan.Api.GitHub;

namespace OF.FSimMan
{
    public class NewsEntry
    {
        public string Title { get; } = string.Empty;
        public DateTime DateTime { get; }
        public string DateTimeString
        {
            get => DateTime.ToString("dd.MM.yyyy");
        }
        public List<string> Content { get; }

        public NewsEntry(GitHubContentData newsPost, List<string> content)
        {
            // DateTime
            {
                int year, month, day;
                string date = newsPost.Name.Split("_")[0];
                year = Convert.ToInt32("20" + date.Substring(0, 2));
                month = Convert.ToInt32(date.Substring(2, 2));
                day = Convert.ToInt32(date.Substring(4, 2));

                DateTime = new DateTime(year, month, day);
            }

            // Title & Content
            {
                int counter = 0;
                bool titleFound = false;
                while (true)
                {
                    string line = content[counter];

                    if (!titleFound)
                    {
                        // Searching Title
                        if (line.StartsWith("# "))
                        {
                            Title = line.Substring(2).Trim();
                            titleFound = true;
                        }
                    }
                    else
                    {
                        // Searching Start
                        if (!line.Trim().Equals(string.Empty))
                        {
                            break;
                        }
                    }

                    counter++;
                }

                Content = content.GetRange(counter, content.Count - counter);
            }
        }
    }
}
