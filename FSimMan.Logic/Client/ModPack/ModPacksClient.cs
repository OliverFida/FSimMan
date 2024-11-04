using OliverFida.FSimMan.Config.ModPack;

namespace OliverFida.FSimMan.Client.ModPack
{
    internal static class ModPacksClient
    {
        public static ModPacks ReadModPacks(FsEdition fsEdition)
        {
            ModPacksData data = ConfigFileClient.DeserializeFile<ModPacksData>(GetFileName(fsEdition));
            ModPacks modPacks = data.FromData();
            StoreModPacks(fsEdition, modPacks);
            return modPacks;
        }

        public static void StoreModPacks(FsEdition fsEdition, ModPacks modPacks)
        {
            ModPacksData data = new ModPacksData();
            data.ToData(modPacks);
            ConfigFileClient.SerializeFile(GetFileName(fsEdition), data);
            CheckDirectoriesMatch(fsEdition, modPacks);
        }

        private static string GetFileName(FsEdition fsEdition)
        {
            switch (fsEdition)
            {
                case FsEdition.Fs22:
                    return "modPacksFs22.xml";
                case FsEdition.Fs25:
                    return "modPacksFs25.xml";
                default:
                    throw new NotImplementedException();
            }
        }

        private static void CheckDirectoriesMatch(FsEdition fsEdition, ModPacks modPacks)
        {
            // Create missing directories
            string gameDirectoryPath = Path.Combine(CurrentApplication.MODPACKS_PATH, fsEdition.ToString());
            if (!Directory.Exists(gameDirectoryPath)) Directory.CreateDirectory(gameDirectoryPath);

            foreach (Config.ModPack.ModPack modPack in modPacks.List)
            {
                string rootDirectoryPath = Path.Combine(gameDirectoryPath, modPack.Key.ToString());
                if (!Directory.Exists(rootDirectoryPath)) Directory.CreateDirectory(rootDirectoryPath);

                string modsDirectoryPath = Path.Combine(rootDirectoryPath, "mods");
                if (!Directory.Exists(modsDirectoryPath)) Directory.CreateDirectory(modsDirectoryPath);
            }

            // Delete deprecated directories
            string[] directories = Directory.GetDirectories(gameDirectoryPath);
            foreach (string directory in directories)
            {
                string key = directory.Split('\\').Last().ToLower();
                Config.ModPack.ModPack? matchingModPack = (from m in modPacks.List where m.Key.ToString().ToLower().Equals(key) select m).FirstOrDefault();
                if (matchingModPack == null) Directory.Delete(directory, true);
            }
        }
    }
}
