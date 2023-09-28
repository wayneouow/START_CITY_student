namespace LO.Model.Server {

    public interface ILOCardServerModel {

        string Identity { get; }
        int Number { get; }
    }

    public class LOCardServerModel : ILOCardServerModel {

        public string Identity { get; private set; }
        public int Number { get; private set; }

        public static ILOCardServerModel Create(string identity, int number) {
            return new LOCardServerModel(identity, number);
        }

        private LOCardServerModel(string identity, int number) {

            this.Identity = identity;
            this.Number = number;
        }
    }
}