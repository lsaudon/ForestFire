using System;
using System.Collections.Generic;
using System.Linq;

namespace ForestFire
{
    public class Forest
    {
        public List<UntouchedTree> UntouchedTrees { get; private set; } = new List<UntouchedTree>();

        public List<FireTree> FireTrees { get; } = new List<FireTree>();

        public List<AshTree> AshTrees { get; } = new List<AshTree>();

        public Forest()
        {
        }

        public Forest(int fireRow, int fireColumn, int forestSize, int treeRatio)
        {
            var random = new Random();
            for (int i = 0; i < forestSize; i++)
            {
                for (int j = 0; j < forestSize; j++)
                {
                    if (i != fireRow || j != fireColumn)
                    {
                        if (random.Next(100) < treeRatio)
                        {
                            UntouchedTrees.Add(new UntouchedTree(new Position(i, j)));
                        }
                    }
                    else
                    {
                        FireTrees.Add(new FireTree(new Position(i, j)));
                    }
                }
            }
        }

        public void Iterate()
        {
            var newFireTrees = new List<FireTree>();
            var newUntouchedTrees = Copy(UntouchedTrees);
            foreach (var fireTree in FireTrees)
            {
                fireTree.GetNeighnorsInUntouchedTrees(newUntouchedTrees.ToList());
                newUntouchedTrees.ExceptWith(fireTree.Neighbors);
                newFireTrees.AddRange(fireTree.Neighbors.Select(item => new FireTree(item.Position)).ToList());
            }
            AshTrees.Clear();
            AshTrees.AddRange(FireTrees.Select(item => new AshTree(new Position(item.Position.Row, item.Position.Column))));
            FireTrees.Clear();
            FireTrees.AddRange(newFireTrees);
            UntouchedTrees = newUntouchedTrees.ToList();
        }

        private static HashSet<UntouchedTree> Copy(List<UntouchedTree> untouchedTrees)
        {
            UntouchedTree[] tempUntouchedTrees = new UntouchedTree[untouchedTrees.Count];
            untouchedTrees.CopyTo(tempUntouchedTrees);
            return new HashSet<UntouchedTree>(tempUntouchedTrees);
        }
    }
}
