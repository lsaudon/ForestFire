using System.Collections.Generic;

namespace ForestFire
{
    public class FireTree : ITree
    {
        public FireTree(Position position)
        {
            Position = position;
        }

        public Position Position { get; }

        public string Color => "#FFFF0000";

        public List<UntouchedTree> Neighbors { get; } = new List<UntouchedTree>();

        public void GetNeighnorsInUntouchedTrees(IEnumerable<UntouchedTree> untouchedTrees)
        {
            foreach (var untouchedTree in untouchedTrees)
            {
                if (untouchedTree.IsNeighbor(this))
                {
                    AddNeighbor(untouchedTree);
                }
            }
        }

        private void AddNeighbor(UntouchedTree tree)
        {
            Neighbors.Add(tree);
        }

    }
}