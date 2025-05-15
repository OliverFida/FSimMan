using OF.FSimMan.Management.Games;
using OF.FSimMan.Management.Games.Fs;
using System.Collections.Concurrent;

namespace OF.FSimMan.Management
{
    public class AppSettingsData : AppSettingsDataBase<AppSettings>
    {
        public ApplicationMode ApplicationMode { get; set; } = ApplicationMode.None;
        public string LastSelectedView { get; set; } = string.Empty;
        //public string LastVersionChangelogDisplayed { get; set; } = string.Empty;
        public string ModificationKey { get; set; } = string.Empty;

        public List<GameSettingsDataBase> GameSettings { get; set; } = new List<GameSettingsDataBase>();

        public override AppSettings FromData()
        {
            AppSettings temp = new AppSettings
            {
                Id = Id,
                _applicationMode = ApplicationMode,
                _lastSelectedView = LastSelectedView,
                //_lastVersionChangelogDisplayed = LastVersionChangelogDisplayed,
                _modificationKey = ModificationKey
            };

            if (temp._applicationMode.Equals(ApplicationMode.None)) temp._applicationMode = ApplicationMode.User;

            ConcurrentBag<GameSettingsBase> tempGames = new ConcurrentBag<GameSettingsBase>();
            Parallel.ForEach(GameSettings, game =>
            {
                switch (game)
                {
                    case GameSettingsFs22Data data:
                        tempGames.Add(data.FromData());
                        break;
                    case GameSettingsFs25Data data:
                        tempGames.Add(data.FromData());
                        break;
                    default:
                        throw new NotImplementedException();
                }
            });
            temp._games.AddRange(tempGames);

            return temp;
        }

        public override void ToData(AppSettings value)
        {
            Id = value.Id;
            ApplicationMode = value._applicationMode;
            LastSelectedView = value.LastSelectedView;
            //LastVersionChangelogDisplayed = value.LastVersionChangelogDisplayed;
            ModificationKey = value.ModificationKey;

            ConcurrentBag<GameSettingsDataBase> temp = new ConcurrentBag<GameSettingsDataBase>();
            Parallel.ForEach(value.Games, game =>
            {
                switch (game)
                {
                    case GameSettingsFs22 gameVal:
                        {
                            GameSettingsFs22Data data = new GameSettingsFs22Data();
                            data.ToData(gameVal);
                            temp.Add(data);
                        }
                        break;
                    case GameSettingsFs25 gameVal:
                        {
                            GameSettingsFs25Data data = new GameSettingsFs25Data();
                            data.ToData(gameVal);
                            temp.Add(data);
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            });
            GameSettings = temp.ToList();
        }
    }
}
