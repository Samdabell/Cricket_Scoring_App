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
            testTeam = new Team("CodeClan Cricket Club", players);
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

    [TestFixture()]
    public class MatchTest
    {
        public Player player1;
        public Player player2;
        public Player player3;
        public Player player4;
        public Player player5;
        public Player player6;
        public Player player7;
        public Player player8;
        public Player player9;
        public Player player10;
        public Team team1;
        public Team team2;
        public List<Player> players1;
        public List<Player> players2;
        public Match testMatch;
        [SetUp]
        public void BeforeTest()
        {
            player1 = new Player("Sam");
            player2 = new Player("Craw");
            player3 = new Player("Tasha");
            player4 = new Player("Colin");
            player5 = new Player("Stu");
            players1 = new List<Player>
            {
                player1, player2, player3, player4, player5
            };
            team1 = new Team("CodeClan Cricket Club", players1);
            player6 = new Player("Alan");
            player7 = new Player("Jia");
            player8 = new Player("Graham");
            player9 = new Player("Mick");
            player10 = new Player("Chris");
            players2 = new List<Player>
            {
                player6, player7, player8, player9, player10
            };
            team2 = new Team("Tontine Cricket Club", players2);
            testMatch = new Match(team1, team2, 20);
        }

        [Test()]
        public void TestInitialSetBatsmen()
        {
            Assert.AreEqual(testMatch.OnStrike, player1);
            Assert.AreEqual(testMatch.OffStrike, player2);
            Assert.IsTrue(player1.HasBatted);
        }

		[Test()]
		public void TestSetBatsmenAfterWicket()
		{
            player1.IsOut = true;
            testMatch.SetBatsmen();
			Assert.AreEqual(testMatch.OnStrike, player3, "onstrike wrong");
			Assert.AreEqual(testMatch.OffStrike, player2, "offstrike wrong");
		}

        [Test()]
        public void TestInEndDueToOvers()
        {
            testMatch.Batting.Runs = 140;
            testMatch.Overs = 20;
            testMatch.InEndCheck();
            Assert.AreEqual(team2, testMatch.Batting);
            Assert.AreEqual(0, testMatch.Overs);
            Assert.AreEqual(140, testMatch.FirstInScore);
        }

		[Test()]
		public void TestInEndDueToWickets()
		{
			testMatch.Batting.Runs = 140;
			testMatch.Overs = 10;
            player1.IsOut = true;
            player2.IsOut = true;
            player3.IsOut = true;
            player4.IsOut = true;
            testMatch.Batting.CalcWickets();
			testMatch.InEndCheck();
			Assert.AreEqual(team2, testMatch.Batting);
			Assert.AreEqual(0, testMatch.Overs);
			Assert.AreEqual(140, testMatch.FirstInScore);
		}

        [Test()]
        public void Test2ndInEndTeam1Win()
        {
            team1.Runs = 140;
            testMatch.Overs = 20;
            testMatch.InEndCheck();
            testMatch.Batting.Runs = 120;
            testMatch.Overs = 20;
            testMatch.InEndCheck();
            Assert.AreEqual(team1, testMatch.Winners);
            Assert.AreEqual(120, team2.Runs);
        }

        [Test()]
        public void Test2ndInEndTeam2Win()
        {
			team1.Runs = 140;
			testMatch.Overs = 20;
			testMatch.InEndCheck();
			testMatch.Batting.Runs = 141;
			testMatch.Overs = 20;
			testMatch.InEndCheck();
			Assert.AreEqual(team2, testMatch.Winners);
            Assert.AreEqual(140, testMatch.FirstInScore);
			Assert.AreEqual(141, team2.Runs);
        }

        [Test()]
        public void TestStandardWicket()
        {
            testMatch.Bowler = player6;
            testMatch.Wicket("Bowled");
            Assert.IsTrue(player1.IsOut);
            Assert.AreEqual(player3, testMatch.OnStrike);
            Assert.AreEqual(1, testMatch.Bowler.Wickets);
        }

        [Test()]
        public void TestRunOut()
        {
            testMatch.Bowler = player6;
            testMatch.Wicket("Run out", testMatch.OffStrike);
            Assert.IsTrue(player2.IsOut);
            Assert.AreEqual(player1, testMatch.OnStrike);
            Assert.AreEqual(player3, testMatch.OffStrike);
            Assert.AreEqual(0, testMatch.Bowler.Wickets);
        }

        [Test()]
        public void TestRun()
        {
            testMatch.Bowler = player6;
            testMatch.Runs(2);
            Assert.AreEqual(2, testMatch.OnStrike.RunsScored);
            Assert.AreEqual(2, testMatch.Bowler.RunsConceded);
        }

        [Test()]
        public void TestRunSwitch()
        {
			testMatch.Bowler = player6;
			testMatch.Runs(1);
			Assert.AreEqual(player2, testMatch.OnStrike);
            Assert.AreEqual(player1, testMatch.OffStrike);
            Assert.AreEqual(0, player2.RunsScored);
            Assert.AreEqual(1, player1.RunsScored);
			Assert.AreEqual(1, testMatch.Bowler.RunsConceded);
        }

        [Test()]
        public void TestDotBall()
        {
            testMatch.Bowler = player6;
            testMatch.BallBowled("Dot", 0);
            Assert.AreEqual(0, team1.Runs);
            Assert.AreEqual(0.1, testMatch.Overs);
            Assert.AreEqual(0, player1.RunsScored);
            Assert.AreEqual(1, player1.BallsFaced);
            Assert.AreEqual(0.1, player6.OversBowled);
            Assert.AreEqual(0, player6.RunsConceded);
        }

        [Test()]
        public void Test1Run()
        {
            testMatch.Bowler = player6;
            testMatch.BallBowled("Runs", 1);
			Assert.AreEqual(1, team1.Runs);
			Assert.AreEqual(0.1, testMatch.Overs);
            Assert.AreEqual(1, player1.RunsScored);
			Assert.AreEqual(1, player1.BallsFaced);
			Assert.AreEqual(0.1, player6.OversBowled);
            Assert.AreEqual(1, player6.RunsConceded);
        }

        [Test()]
        public void TestWide()
        {
			testMatch.Bowler = player6;
			testMatch.BallBowled("Wide", 1);
			Assert.AreEqual(1, team1.Runs);
			Assert.AreEqual(0, testMatch.Overs, "overs wrong");
			Assert.AreEqual(0, player1.RunsScored);
			Assert.AreEqual(0, player1.BallsFaced);
			Assert.AreEqual(0, player6.OversBowled);
			Assert.AreEqual(1, player6.RunsConceded);
        }

        [Test()]
        public void TestNoBall()
        {
			testMatch.Bowler = player6;
			testMatch.BallBowled("No Ball", 2);
			Assert.AreEqual(2, team1.Runs);
			Assert.AreEqual(0, testMatch.Overs, "overs wrong");
			Assert.AreEqual(1, player1.RunsScored);
			Assert.AreEqual(0, player1.BallsFaced);
            Assert.AreEqual(player2, testMatch.OnStrike);
			Assert.AreEqual(0, player6.OversBowled);
			Assert.AreEqual(2, player6.RunsConceded);
        }

        [Test()]
        public void TestWicketBall()
        {
            testMatch.Bowler = player6;
            testMatch.BallBowled("Wicket", 0, "Bowled");
			Assert.AreEqual(0, team1.Runs);
			Assert.AreEqual(0.1, testMatch.Overs, "overs wrong");
			Assert.IsTrue(player1.IsOut);
            Assert.AreEqual("Bowled", player1.MethodOut);
			Assert.AreEqual(1, player1.BallsFaced);
			Assert.AreEqual(0.1, player6.OversBowled);
			Assert.AreEqual(0, player6.RunsConceded);
            Assert.AreEqual(1, player6.Wickets);
            Assert.AreEqual(player3, testMatch.OnStrike);
            Assert.AreEqual(1, team1.Wickets);
        }

        [Test()]
        public void TestFullOver()
        {
            testMatch.Bowler = player6;
            testMatch.BallBowled("Dot", 0);
            testMatch.BallBowled("Runs", 1);
            testMatch.BallBowled("Runs", 4);
            testMatch.BallBowled("Wicket", 0, "LBW");
            testMatch.BallBowled("Wide", 1);
            testMatch.BallBowled("Runs", 2);
            testMatch.BallBowled("Runs", 6);
            Assert.AreEqual(14, team1.Runs);
            Assert.AreEqual(1, team1.Wickets);
            Assert.AreEqual(1.0, testMatch.Overs);
            Assert.AreEqual(1, player1.RunsScored);
            Assert.AreEqual(2, player1.BallsFaced);
            Assert.AreEqual(4, player2.RunsScored);
            Assert.AreEqual(2, player2.BallsFaced);
            Assert.IsTrue(player2.IsOut);
            Assert.AreEqual("LBW", player2.MethodOut);
            Assert.AreEqual(8, player3.RunsScored);
            Assert.AreEqual(2, player3.BallsFaced);
            Assert.AreEqual(1, team1.Extras);
            Assert.AreEqual(1.0, player6.OversBowled);
            Assert.AreEqual(14, player6.RunsConceded);
            Assert.AreEqual(1, player6.Wickets);
            Assert.Null(testMatch.Bowler);
            Assert.AreEqual(player1, testMatch.OnStrike);
            Assert.AreEqual(player3, testMatch.OffStrike);
        }

    }
}
