namespace Domain;

public class Player : Entity
{
    public string Name { get; init; }
    public PlayerType PlayerType { get; init; }

    public Player(string name, PlayerType playerType)
    {
        Name = name;
        PlayerType = playerType;
    }

}