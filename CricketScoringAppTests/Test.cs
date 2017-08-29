using NUnit.Framework;
using System;
using CricketScoringApp;
using System.Collections.Generic;

namespace CricketScoringAppTests
{
    [TestFixture()]
    public class PlayerTest
    {
        public Player testPlayer;
        [SetUp]
        public void BeforeTest()
        {
            testPlayer = new Player("Sam");
        }

        [Test()]
        public void TestRuns()
        {
            testPlayer.ScoreRuns(2);
            Assert.AreEqual(2, testPlayer.RunsScored);
        }

		[Test()]
		public void TestBallsBowled()
		{
			testPlayer.BallBowled();
			Assert.AreEqual(0.1, testPlayer.OversBowled);
		}

		[Test()]
		public void TestOversConverter()
		{
			testPlayer.BallBowled();
            testPlayer.BallBowled();
            testPlayer.BallBowled();
            testPlayer.BallBowled();
            testPlayer.BallBowled();
            testPlayer.BallBowled();
			Assert.AreEqual(1, testPlayer.OversBowled);
		}

		[Test()]
		public void TestOversConverter2()
		{
			testPlayer.BallBowled();
			testPlayer.BallBowled();
			testPlayer.BallBowled();
			testPlayer.BallBowled();
			testPlayer.BallBowled();
			testPlayer.BallBowled();
            testPlayer.BallBowled();
			Assert.AreEqual(1.1, testPlayer.OversBowled);
		}

		[Test()]
		public void TestOut()
		{
            testPlayer.IsOut = true;
            testPlayer.MethodOut = "Bowled";
			Assert.IsTrue(testPlayer.IsOut);
            Assert.AreEqual("Bowled", testPlayer.MethodOut);
		}


	}

    [TestFixture()]
    public class TeamTest
    {
        public Player player1;
        public Player player2;
        public Player player3;
        public Player player4;
        public Player player5;
        public Team testTeam;
        public List<Player> players;

        [SetUp]
        public void BeforeTest()
        {
            player1 = new Player("Sam");
            player2 = new Player("Craw");
            player3 = new Player("Tasha");
            player4 = new Player("Colin");
            player5 = new Player("Stu");
            players = new List<Player>
            {
                player1, player2, player3, player4, player5
            };
            testTeam = new Team(players);
        }

		[Test()]
		public void TestNumPlayers()
		{
			Assert.AreEqual(5, testTeam.NumPlayers());
		}

		[Test()]
		public void TestCalcExtras()
		{
            testTeam.AddByes(2);
            testTeam.AddWides(4);
            testTeam.AddLegByes(1);
            testTeam.AddNoBalls(3);
            testTeam.CalcExtras();
            Assert.AreEqual(10, testTeam.Extras);
		}

		[Test()]
		public void TestCalcRuns()
		{
			testTeam.AddByes(2);
			testTeam.AddWides(4);
			testTeam.AddLegByes(1);
			testTeam.AddNoBalls(3);
            player1.ScoreRuns(3);
            player2.ScoreRuns(10);
            player3.ScoreRuns(30);
            player4.ScoreRuns(76);
            testTeam.CalcRuns();
			Assert.AreEqual(129, testTeam.Runs);
		}

		[Test()]
		public void TestCalcWickets()
		{
            player1.IsOut = true;
			player2.IsOut = true;
			player3.IsOut = true;
			testTeam.CalcWickets();
            Assert.AreEqual(3, testTeam.Wickets);
		}

	}
}
