namespace Testovoe
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        // Метод для определения расстояния между координатами
        public double DistanceTo(Coordinate c)
        {
            double dx = (double)(c.X - X);
            double dy = (double)(c.Y - Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
