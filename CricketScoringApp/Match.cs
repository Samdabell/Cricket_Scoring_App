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
        }


		public void OversConverter()
		{
			if (Overs - Math.Truncate(Overs) > 0.5)
			{
				Overs = Math.Ceiling(Overs);
			}
		}

        public void InEndCheck(){
            if(Overs >= InLength || Batting.Wickets == Batting.NumPlayers()-1){
                if (FirstInScore == 0){
                    FirstInScore = Batting.Runs;
                    Overs = 0;
                    Batting = Team2;
                }
                else {
                    WinCheck();
                }
            }
        }

        private void WinCheck()
        {
            if(Batting == Team2){
                if (Batting.Runs > FirstInScore ){
                    Winners = Team2;
                }
                if (Batting.Runs < FirstInScore){
                    Winners = Team1;
                }
                else{
                    Winners = null;
                }
            }
        }

        public void SetBatsmen(){
            OnStrike = Batting.Players.Find(x => x.IsOut == false && x.HasBatted == false);
            OnStrike.HasBatted = true;
            OffStrike = Batting.Players.Find(x => x.IsOut == false && x.HasBatted == false);
            OffStrike.HasBatted = true;
        }
    }
}
