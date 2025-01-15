using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PriorityQueueTests
{
    public class PriorityQueue
    {
        private List<(string value, int priority)> queue = new List<(string, int)>();

        public void Enqueue(string value, int priority)
        {
            queue.Add((value, priority));
        }

        public string Dequeue()
        {
            if (queue.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            var highestPriority = int.MinValue;
            foreach (var item in queue)
            {
                if (item.priority > highestPriority)
                {
                    highestPriority = item.priority;
                }
            }

            var index = queue.FindIndex(item => item.priority == highestPriority);
            var itemToReturn = queue[index];
            queue.RemoveAt(index);
            return itemToReturn.value;
        }

        public int Count => queue.Count;
    }

    [TestClass]
    public class PriorityQueue_Tests
    {
        [TestMethod]
        // Scenario: Enqueue three players with different priorities and dequeue them.
        // Expected Result: Tim (priority 3), Sue (priority 2), Bob (priority 1)
        // Defect(s) Found: None
        public void TestPriorityQueue_1()
        {
            var pq = new PriorityQueue();
            pq.Enqueue("Bob", 1);
            pq.Enqueue("Sue", 2);
            pq.Enqueue("Tim", 3);

            Assert.AreEqual("Tim", pq.Dequeue());
            Assert.AreEqual("Sue", pq.Dequeue());
            Assert.AreEqual("Bob", pq.Dequeue());
        }

        [TestMethod]
        // Scenario: Enqueue three players with equal priority and dequeue them.
        // Expected Result: Tim should be dequeued first, followed by Bob and Sue.
        // Defect(s) Found: None
        public void TestPriorityQueue_2()
        {
            var pq = new PriorityQueue();
            pq.Enqueue("Bob", 1);
            pq.Enqueue("Sue", 1);
            pq.Enqueue("Tim", 2);

            Assert.AreEqual("Tim", pq.Dequeue());
            Assert.AreEqual("Bob", pq.Dequeue());
            Assert.AreEqual("Sue", pq.Dequeue());
        }

        [TestMethod]
        // Scenario: Try to dequeue from an empty queue.
        // Expected Result: Exception should be thrown with message "Queue is empty."
        // Defect(s) Found: None
        public void TestPriorityQueue_Empty()
        {
            var pq = new PriorityQueue();
            Assert.ThrowsException<InvalidOperationException>(() => pq.Dequeue());
        }
    }
}
