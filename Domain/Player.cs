namespace Domain;

public abstract class Player : Entity
{
    public string Name { get; init; }
    public PlayerType PlayerType { get; init; }

    public Player(string name, PlayerType playerType)
    {
        Name = name;
        PlayerType = playerType;
    }

}