namespace Testovoe
{
    // Класс игрока
    public class Player
    {
        public string Nickname { get; set; }
        public Coordinate Position { get; set; }

        public Player(string nickname, Coordinate position)
        {
            Nickname = nickname;
            Position = position;
        }
    }
}
