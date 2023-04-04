namespace Testovoe
{
    // Класс автомобиля
    public class Car
    {
        public string Name { get; set; }
        public Coordinate Position { get; set; }
        public Player Driver { get; set; }
        public List<Player> Passengers { get; set; }

        public Car(string name, Coordinate position)
        {
            Name = name;
            Position = position;
            Passengers = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            if (Driver == null)
            {
                Driver = player;
            }
            else if (Passengers.Count < 3)
            {
                Passengers.Add(player);
            }
            else
            {
                throw new Exception("Car is full");
            }
        }

        // Метод для определения всех игроков в радиусе 15 от машины
        public List<Tuple<Player, double>> GetNearbyPlayers(List<Player> players)
        {
            List<Tuple<Player, double>> nearbyPlayers = new List<Tuple<Player, double>>();

            foreach (Player player in players)
            {
                double distance = Position.DistanceTo(player.Position);
                if (distance <= 15)
                {
                    nearbyPlayers.Add(new Tuple<Player, double>(player, distance));
                }
            }

            return nearbyPlayers;
        }
    }
}
