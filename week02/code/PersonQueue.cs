/// <summary>
/// A basic implementation of a Queue
/// </summary>
public class PersonQueue
{
    private readonly List<Person> _queue = new();

    public int Length => _queue.Count;

    /// <summary>
    /// Add a person to the queue (FIFO: add to back)
    /// </summary>
    public void Enqueue(Person person)
    {
        _queue.Add(person); // add to the end (back of queue)
    }

    /// <summary>
    /// Remove a person from the queue (FIFO: remove from front)
    /// </summary>
    public Person Dequeue()
    {
        var person = _queue[0];
        _queue.RemoveAt(0);
        return person;
    }

    public bool IsEmpty()
    {
        return Length == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}
