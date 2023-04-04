using Testovoe;
class Program
{
    static void Main(string[] args)
    {
        // Создание общего списка игроков и автомобилей
        List<Player> players = new List<Player>();
        List<Car> cars = new List<Car>();

        // Заполнение списков случайными данными(Основной поток)
        Random random = new Random();

        for (int i = 0; i < 1000; i++)
        {
            Coordinate position = new Coordinate() { X = random.Next(101), Y = random.Next(101) };
            players.Add(new Player($"Player{i + 1}", position));
        }

        for (int i = 0; i < 200; i++)
        {
            Coordinate position = new Coordinate() { X = random.Next(101), Y = random.Next(101) };
            cars.Add(new Car($"Car{i + 1}", position));
        }

        // Заполнение машин игроками(Доп. поток)
        Task.Run(() =>
        {
            foreach (Player player in players)
            {
                Car car = cars.OrderBy(c => c.Position.DistanceTo(player.Position)).First();
                try
                {
                    car.AddPlayer(player);
                }
                catch (Exception)
                {
                    // Машина уже заполнена, ничего не делаем
                }
            }
        });

        // Вывод информации о 5 случайных автомобилях
        Console.WriteLine("Randomly selected cars:\n");
        for (int i = 0; i < 5; i++)
        {
            Car car = cars[random.Next(cars.Count)];
            string driverInfo, passengersInfo;
            if (car.Driver != null)
            {
                driverInfo = car.Driver.Nickname;
            }
            else
            {
                driverInfo = "no driver";
            }
            if (car.Passengers != null && car.Passengers.Count > 0)
            {
                passengersInfo = string.Join(", ", car.Passengers.Select(p => p.Nickname));
            }
            else
            {
                passengersInfo = "no passengers";
            }
            Console.WriteLine($"{car.Name} - Driver: {driverInfo}, Passengers: {passengersInfo}");
            if (i.Equals(4))
            {
                Console.WriteLine("");
            }
        }


        // Выбор случайной машины и вывод всех игроков в радиусе 15 от нее
        Car selectedCar = cars[random.Next(cars.Count)];
        List<Tuple<Player, double>> nearbyPlayers = selectedCar.GetNearbyPlayers(players);

        Console.WriteLine($"Players near {selectedCar.Name}:\n");
        foreach (Tuple<Player, double> tuple in nearbyPlayers)
        {
            Console.WriteLine($"{tuple.Item1.Nickname}: {tuple.Item2}");
        }

        Console.ReadKey();
    }
}