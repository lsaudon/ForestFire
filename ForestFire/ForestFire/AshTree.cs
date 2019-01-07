namespace ForestFire
{
    public class AshTree : ITree
    {

        public AshTree(Position position)
        {
            Position = position;
        }

        public Position Position { get; }

        public string Color => "#FF787878";
    }
}