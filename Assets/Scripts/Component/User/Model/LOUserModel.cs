namespace LO.Model {

    public interface ILOUserModel {
        // server data
        string UserId { get; } // TODO : check id format
        int Level { get; }
        int Experience { get; }
        int Coin { get; }
        int TechPoint { get; }
        // frontend data
        float Progress { get; }
        ILOUserServerModel GetServerModel();
    }

    public class LOUserModel : ILOUserModel {

        // NOTE : for test
        public static ILOUserModel CreateFake() {

            return LOUserModel.Create("fake-user-9999", 100, 100, 999999, 999999);
        }

        public static ILOUserModel Create(string userId, int level, int experience, int coin, int techPoint) {

            return new LOUserModel(userId, level, experience, coin, techPoint);
        }

        public string UserId { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int Coin { get; private set; }
        public int TechPoint { get; private set; }
        public float Progress { get => ExperienceToProgress(); }

        private LOUserModel(string userId, int level, int experience, int coin, int techPoint) {

            UserId = userId;
            Experience = experience;
            Coin = coin;
            TechPoint = techPoint;

            Level = ExperienceToLevel(Experience);
        }

        public ILOUserServerModel GetServerModel() {

            return LOUserServerModel.Create(UserId, Level, Experience, Coin, TechPoint);
        }

        // NOTE : although we save the level to server, but we recaluculate the level from frontend just for save
        int ExperienceToLevel(int experience) {

            // TODO : update the experience to level list
            return 100;
        }

        float ExperienceToProgress() {
            // TODO : update the progress by level list
            return 0.15f;
        }
    }
}