using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several items with different priorities, then Dequeue.
    // Expected Result: The item with the highest priority is removed first.
    // Defect(s) Found: Dequeue did not remove the highest priority correctly (ignored last element / wrong selection).
    public void TestPriorityQueue_HighestPriorityRemovedFirst()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("low", 1);
        pq.Enqueue("high", 5);
        pq.Enqueue("mid", 3);

        Assert.AreEqual("high", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Two items have the same highest priority.
    // Expected Result: FIFO tie-breaker: the first enqueued among the highest priority is removed first.
    // Defect(s) Found: Tie-breaking was incorrect because Dequeue used >= and removed the later item first.
    public void TestPriorityQueue_TieBreakerIsFIFO()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 10);
        pq.Enqueue("B", 10);
        pq.Enqueue("C", 1);

        Assert.AreEqual("A", pq.Dequeue());
        Assert.AreEqual("B", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: After Dequeue, the removed item should no longer be in the queue.
    // Expected Result: Two dequeues return two different items in correct priority order.
    // Defect(s) Found: Item was not removed from the internal list, causing repeated results.
    public void TestPriorityQueue_DequeueActuallyRemovesItem()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("X", 2);
        pq.Enqueue("Y", 9);

        Assert.AreEqual("Y", pq.Dequeue());
        Assert.AreEqual("X", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue on empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None (should throw correct exception/message).
    public void TestPriorityQueue_EmptyThrows()
    {
        var pq = new PriorityQueue();

        try
        {
            pq.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}
