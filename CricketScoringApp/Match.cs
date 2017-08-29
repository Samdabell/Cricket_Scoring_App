using System;
namespace CricketScoringApp
{
    public class Match
    {
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public Team Batting { get; set; }
        public Player OnStrike { get; set; }
        public Player OffStrike { get; set; }
        public Player Bowler { get; set; }
        public double Overs { get; set; }
        public int InLength { get;  set;}
        public int FirstInScore { get; set; }
        public Team Winners { get; set; }

        
        public Match(Team team1, Team team2, int length)
        {
            Team1 = team1;
            Team2 = team2;
            Batting = Team1;
            InLength = length;
            Overs = 0;
            SetBatsmen();
        }


		public void OversConverter()
		{
			if (Overs - Math.Truncate(Overs) > 0.5)
			{
				Overs = Math.Ceiling(Overs);
			}
		}

        public void InEndCheck(){
            if((Overs >= InLength || Batting.Wickets == Batting.NumPlayers() - 1) && Batting == Team1)
            {
                FirstInScore = Batting.Runs;
                Overs = 0;
                Batting = Team2;
            }
            else if(Overs >= InLength || Batting.Wickets == Batting.NumPlayers() - 1 || Batting.Runs > FirstInScore)
            {
                WinCheck();
            }
                
        }

        private void WinCheck()
        {
            if(Batting == Team2){
                if (Batting.Runs > FirstInScore){
                    Winners = Team2;
                }
                else if (Batting.Runs < FirstInScore){
                    Winners = Team1;
                }
                else{
                    Winners = null;
                }
            }
        }

        public void SetBatsmen()
        {
            if (OnStrike == null || OnStrike.IsOut == true)
            {
                OnStrike = Batting.Players.Find(x => x.IsOut == false && x.HasBatted == false);
                OnStrike.HasBatted = true;
            }
            if (OffStrike == null || OffStrike.IsOut == true)
            {
                OffStrike = Batting.Players.Find(x => x.IsOut == false && x.HasBatted == false);
                OffStrike.HasBatted = true;
            }
        }

        public void Wicket(string method)
        {
            OnStrike.IsOut = true;
            OnStrike.MethodOut = method;
            Bowler.GetWicket();
            SetBatsmen();
            InEndCheck();
        }

		public void Wicket(string method, Player player)
		{
            player.IsOut = true;
            player.MethodOut = method;
            if (method != "Run out")
                Bowler.GetWicket();
			SetBatsmen();
			//InEndCheck();
		}

        public void Runs(int runs)
        {
            OnStrike.ScoreRuns(runs);
            Bowler.ConcedeRuns(runs);
            if (runs % 2 != 0){
                BatsmenSwap();
            }
                
        }

        private void BatsmenSwap()
        {
            Player swap = OnStrike;
            OnStrike = OffStrike;
            OffStrike = swap;
        }

        public void BallBowled(string outcome, int runs)
        {
            if(outcome != "Wide" && outcome != "No Ball"){
                OnStrike.FaceBall();
                Bowler.BallBowled();
                Overs += 0.1;
                OversConverter();
            }
            if(outcome == "Runs"){
                Runs(runs);
            }
            if(outcome == "Byes"){
                Batting.AddByes(runs);
            }
            if(outcome == "Leg Byes"){
                Batting.AddLegByes(runs);
            }
            if(outcome == "Wide"){
                Batting.AddWides(runs);
                Bowler.ConcedeRuns(runs);
            }
            if(outcome == "No Ball"){
                if (runs > 1)
                {
                    Batting.AddNoBalls(1);
                    OnStrike.ScoreRuns(runs - 1);
                    if (runs % 2 != 0)
                        BatsmenSwap();
                }
                else
                {
                    Batting.AddNoBalls(runs);
                }
                Bowler.ConcedeRuns(runs);
            }
            Batting.CalcRuns();
            EndOfOverCheck();
            InEndCheck();

        }

        public void BallBowled(string outcome, int runs, string methodOut)
        {
			if (outcome != "No Ball" || outcome != "Wide")
			{
				OnStrike.FaceBall();
				Bowler.BallBowled();
				Overs += 0.1;
				OversConverter();
			}
			if (outcome == "Runs")
			{
				Runs(runs);
			}
			if (outcome == "Byes")
			{
				Batting.AddByes(runs);
			}
			if (outcome == "Leg Byes")
			{
				Batting.AddLegByes(runs);
			}
			if (outcome == "Wides")
			{
				Batting.AddWides(runs);
				Bowler.ConcedeRuns(runs);
			}
			if (outcome == "No Ball")
			{
				Batting.AddNoBalls(runs);
				Bowler.ConcedeRuns(runs);
			}
            Wicket(methodOut);
			Batting.CalcRuns();
            EndOfOverCheck();
			InEndCheck();

        }

        public void EndOfOverCheck()
        {
            if(Overs - Math.Truncate(Overs) <= 0)
            {
                BatsmenSwap();
                Bowler = null;
            }
        }
    }
}
