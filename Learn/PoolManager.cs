using M133.Models;

namespace M133.Services.Learn;

public class PoolManager
{
    private readonly Lernset _lernset;
    private LinkedList<Card> _activeCards = new();

    public PoolManager(Lernset lernset)
    {
        _lernset = lernset;
    }


}