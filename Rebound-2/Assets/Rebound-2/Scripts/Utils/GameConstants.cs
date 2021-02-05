namespace BumblePux.Rebound2.Utils
{
    public static class GameConstants
    {
        public static class Scenes
        {
            public const string SCENE_MAIN_MENU = "MainMenu";
            public const string SCENE_GAMEMODE_TIMED = "GameMode_Timed";
        }

        public static class SaveLoad
        {
            public const string DEBUG_FILE_PATH = "E:\\Files\\Projects\\Unity\\Rebound-2\\";
            public const string FILE_NAME = "Rebound-2";

            public const string EXTENSION_JSON = ".json";
            public const string EXTENSION_JSON_ENCRYPTED = ".jsav";

            public const string FILE_PATH_JSON_DEBUG = DEBUG_FILE_PATH + FILE_NAME + EXTENSION_JSON;
            public const string FILE_PATH_JSON_DEBUG_ENCRYPTED = DEBUG_FILE_PATH + FILE_NAME + EXTENSION_JSON_ENCRYPTED;

            public const string XOR_KEY = "TeamBumblePux1!";
        }

        public static class Text
        {
            public const string TEXT_EQUIP = "Equip";
            public const string TEXT_EQUIPPED = "Equipped";
            public const string TEXT_BUY = "Buy";
        }

        public static class Links
        {
            public const string LINK_PRIVACY_POLICY = "https://sites.google.com/view/bumblepux/games/rebound-2-privacy-policy?authuser=1";
        }
    }
}