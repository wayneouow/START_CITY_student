namespace LO.Model {

    public interface ILOUserServerModel {

        string UserId { get; }
        int Level { get; }
        int Experience { get; }
        int Coin { get; }
        int TechPoint { get; }
    }

    public class LOUserServerModel : ILOUserServerModel {

        public static ILOUserServerModel Create(string userId, int level, int experience, int coin, int techPoint) {

            return new LOUserServerModel(userId, level, experience, coin, techPoint);
        }

        public string UserId { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int Coin { get; private set; }
        public int TechPoint { get; private set; }

        private LOUserServerModel(string userId, int level, int experience, int coin, int techPoint) {

            UserId = userId;
            Level = level;
            Experience = experience;
            Coin = coin;
            TechPoint = techPoint;
        }
    }
}