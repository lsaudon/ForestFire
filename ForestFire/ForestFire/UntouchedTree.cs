namespace ForestFire
{
    public class UntouchedTree : ITree
    {
        public UntouchedTree(Position position)
        {
            Position = position;
        }

        public Position Position { get; }

        public string Color => "#FF00FF00";

        public bool IsNeighbor(FireTree fireTree)
        {
            return Position.IsNeighbor(fireTree.Position);
        }
    }
}
