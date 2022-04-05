using M133.Models;

namespace M133.Learn;

public class PoolManager
{
    private readonly StudySet _studySet;
    private LinkedList<Card> _activeCards = new();

    public PoolManager(Models.Learn learn)
    {
        _studySet = learn.StudySet;
    }
}