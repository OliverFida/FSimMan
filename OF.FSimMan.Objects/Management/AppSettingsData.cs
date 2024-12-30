using OF.FSimMan.Management.Games;
using OF.FSimMan.Management.Games.Fs;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace OF.FSimMan.Management
{
    //[XmlRoot("AppSettings")]
    //[XmlInclude(typeof(AppSettingsGameFs22Data))]
    //[XmlInclude(typeof(AppSettingsGameFs25Data))]
    //public class AppSettingsData : AppSettingsDataBase<AppSettings>
    //{
    //    [XmlElement(IsNullable = false)]
    //    public ApplicationMode ApplicationMode = ApplicationMode.None;

    //    [XmlElement(IsNullable = false)]
    //    public string LastSelectedView = string.Empty;

    //    [XmlElement(IsNullable = false)]
    //    public string LastVersionChangelogDisplayed = string.Empty;

    //    [XmlArray(nameof(Games), IsNullable = true)]
    //    [XmlArrayItem("GameSettings")]
    //    public AppSettingsGameDataBase[] Games = [];

    //    public override AppSettings FromData()
    //    {
    //        AppSettings temp = new AppSettings
    //        {
    //            _applicationMode = ApplicationMode,
    //            _lastSelectedView = LastSelectedView,
    //            _lastVersionChangelogDisplayed = LastVersionChangelogDisplayed
    //        };

    //        if (temp._applicationMode == ApplicationMode.None) temp._applicationMode = ApplicationMode.User;

    //        ConcurrentBag<AppSettingsGameBase> tempGames = new ConcurrentBag<AppSettingsGameBase>();
    //        Parallel.ForEach(Games, game =>
    //        {
    //            switch (game)
    //            {
    //                case AppSettingsGameFs22Data data:
    //                    tempGames.Add(data.FromData());
    //                    break;
    //                case AppSettingsGameFs25Data data:
    //                    tempGames.Add(data.FromData());
    //                    break;
    //                default:
    //                    throw new NotImplementedException();
    //            }
    //        });
    //        temp._games.AddRange(tempGames);

    //        return temp;
    //    }

    //    public override void ToData(AppSettings value)
    //    {
    //        ApplicationMode = value._applicationMode;
    //        LastSelectedView = value.LastSelectedView;
    //        LastVersionChangelogDisplayed = value.LastVersionChangelogDisplayed;

    //        ConcurrentBag<AppSettingsGameDataBase> temp = new ConcurrentBag<AppSettingsGameDataBase>();
    //        Parallel.ForEach(value.Games, game =>
    //        {
    //            switch (game)
    //            {
    //                case AppSettingsGameFs22 gameVal:
    //                    {
    //                        AppSettingsGameFs22Data data = new AppSettingsGameFs22Data();
    //                        data.ToData(gameVal);
    //                        temp.Add(data);
    //                    }
    //                    break;
    //                case AppSettingsGameFs25 gameVal:
    //                    {
    //                        AppSettingsGameFs25Data data = new AppSettingsGameFs25Data();
    //                        data.ToData(gameVal);
    //                        temp.Add(data);
    //                    }
    //                    break;
    //                default:
    //                    throw new NotImplementedException();
    //            }
    //        });
    //        Games = temp.ToArray();
    //    }
    //}

    public class AppSettingsData : AppSettingsDataBase<AppSettings>
    {
        public ApplicationMode ApplicationMode { get; set; } = ApplicationMode.None;
        public string LastSelectedView { get; set; } = string.Empty;
        //public string LastVersionChangelogDisplayed { get; set; } = string.Empty;

        public override AppSettings FromData()
        {
            AppSettings temp = new AppSettings
            {
                _id = Id,
                _applicationMode = ApplicationMode,
                _lastSelectedView = LastSelectedView,
                //_lastVersionChangelogDisplayed = LastVersionChangelogDisplayed
            };

            if(temp._applicationMode.Equals(ApplicationMode.None)) temp._applicationMode = ApplicationMode.User;

            return temp;
        }

        public override void ToData(AppSettings value)
        {
            Id = value._id;
            ApplicationMode = value._applicationMode;
            LastSelectedView = value.LastSelectedView;
            //LastVersionChangelogDisplayed = value.LastVersionChangelogDisplayed;
        }
    }
}
