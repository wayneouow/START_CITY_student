namespace LO
{
    public enum LOAppSceneState
    {
        Preload = 0,
        Main,
        Game
    }

    public class LOConst
    {
        #region scene

        public static string SCENE_PRELOAD = "preload-scene";
        public static string SCENE_MAIN = "main-scene";
        public static string SCENE_GAME = "game-scene";
        public static string SCENE_DEMO = "demo-scene";

        public static string GetSceneNameByState(LOAppSceneState state)
        {
            switch (state)
            {
                case LOAppSceneState.Preload:
                    return SCENE_PRELOAD;
                case LOAppSceneState.Main:
                    return SCENE_MAIN;
                case LOAppSceneState.Game:
                    return SCENE_DEMO; // TODO : refactor or just replace
            }
            return "";
        }

        #endregion

        #region locale

        public static string[] LocaleList = new string[2] { "tw", "en" };

        #endregion

        #region asset key

        public static string CardBookKey = "card-book";
        public static string UserCardKey = "user-card";

        #endregion
    }
}
