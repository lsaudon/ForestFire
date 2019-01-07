namespace ForestFire
{
    public class Position
    {
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }

        public int Column { get; }

        public bool IsNeighbor(Position position)
        {
            return ((Row == position.Row && Column != position.Column) || (Row != position.Row && Column == position.Column))
                   && (Row == position.Row + 1 || Row == position.Row - 1 || Column == position.Column + 1 || Column == position.Column - 1);
        }
    }
}