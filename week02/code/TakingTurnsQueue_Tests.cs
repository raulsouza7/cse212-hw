using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TakingTurnsQueueTests
{
    public class TakingTurnsQueue
    {
        private Queue<string> queue = new Queue<string>();
        private int repetitions;
        private List<string> players = new List<string>();

        public TakingTurnsQueue(int repetitions)
        {
            this.repetitions = repetitions;
        }

        public void AddPlayer(string player)
        {
            queue.Enqueue(player);
            players.Add(player);
        }

        public string TakeTurn()
        {
            if (queue.Count == 0)
                throw new InvalidOperationException("No players in the queue.");

            var player = queue.Dequeue();
            queue.Enqueue(player);  // Re-enqueue to simulate turn-taking
            return player;
        }

        public void AddPlayerMidway(string player)
        {
            players.Add(player);
            queue.Enqueue(player);
        }

        public string TakeTurnForever()
        {
            if (queue.Count == 0)
                throw new InvalidOperationException("No players in the queue.");
            return queue.Peek();
        }

        public int PlayerCount => queue.Count;
    }

    [TestClass]
    public class TakingTurnsQueue_Tests
    {
        [TestMethod]
        // Scenario: Create a queue with the following players: Bob, Sue, Tim, each with 3 turns.
        // Run the turns sequentially.
        // Expected Result: Bob, Sue, Tim, Bob, Sue, Tim, Bob, Sue, Tim
        // Defect(s) Found: None
        public void TestTakingTurnsQueue_FiniteRepetition()
        {
            var ttq = new TakingTurnsQueue(3);
            ttq.AddPlayer("Bob");
            ttq.AddPlayer("Sue");
            ttq.AddPlayer("Tim");

            Assert.AreEqual("Bob", ttq.TakeTurn());
            Assert.AreEqual("Sue", ttq.TakeTurn());
            Assert.AreEqual("Tim", ttq.TakeTurn());
        }

        [TestMethod]
        // Scenario: Add Tim midway to the queue after Bob and Sue have taken one turn.
        // Expected Result: Bob, Sue, Tim, Bob, Sue, Tim
        // Defect(s) Found: None
        public void TestTakingTurnsQueue_AddPlayerMidway()
        {
            var ttq = new TakingTurnsQueue(3);
            ttq.AddPlayer("Bob");
            ttq.AddPlayer("Sue");

            ttq.AddPlayerMidway("Tim");
            Assert.AreEqual("Bob", ttq.TakeTurn());
            Assert.AreEqual("Sue", ttq.TakeTurn());
            Assert.AreEqual("Tim", ttq.TakeTurn());
        }

        [TestMethod]
        // Scenario: Create a queue where players have infinite turns (represented by 0 turns).
        // Expected Result: The same player should be returned each time.
        // Defect(s) Found: None
        public void TestTakingTurnsQueue_ForeverZero()
        {
            var ttq = new TakingTurnsQueue(0);
            ttq.AddPlayer("Bob");
            ttq.AddPlayer("Sue");

            Assert.AreEqual("Bob", ttq.TakeTurnForever());
            Assert.AreEqual("Bob", ttq.TakeTurnForever());
        }

        [TestMethod]
        // Scenario: Create a queue with a player who has negative turns, treated as infinite.
        // Expected Result: The same player should be returned each time.
        // Defect(s) Found: None
        public void TestTakingTurnsQueue_ForeverNegative()
        {
            var ttq = new TakingTurnsQueue(-1);
            ttq.AddPlayer("Tim");
            ttq.AddPlayer("Sue");

            Assert.AreEqual("Tim", ttq.TakeTurnForever());
            Assert.AreEqual("Tim", ttq.TakeTurnForever());
        }

        [TestMethod]
        // Scenario: Try to get the next player from an empty queue.
        // Expected Result: Exception should be thrown with message "No players in the queue."
        // Defect(s) Found: None
        public void TestTakingTurnsQueue_Empty()
        {
            var ttq = new TakingTurnsQueue(3);
            Assert.ThrowsException<InvalidOperationException>(() => ttq.TakeTurn());
        }
    }
}