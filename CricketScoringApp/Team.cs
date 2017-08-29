using System;
using System.Collections.Generic;

namespace CricketScoringApp
{
    public class Team
    {
        public string Name { get; set; }
        public int Runs { get; set; }
        public int Wickets { get; set; }
        public int Byes { get; set; }
        public int LegByes { get; set; }
        public int Wides { get; set; }
        public int NoBalls { get; set; }
        public int Extras { get; set; }
        public List<Player> Players; 

        public Team(string name, List<Player> players)
        {
            Name = name;
            Players = players;
        }

        public void CalcExtras()
        {
            Extras = Byes + LegByes + Wides + NoBalls;
        }

        public void CalcRuns()
        {
            CalcExtras();
            Runs = Extras;
            foreach (Player player in Players){
                Runs += player.RunsScored;
            }
        }

        public void AddRuns(int run)
        {
            Runs += run;
        }

		public void AddByes(int run)
		{
			Byes += run;
		}

		public void AddLegByes(int run)
		{
			LegByes += run;
		}

		public void AddWides(int run)
		{
            Wides += run;
		}

		public void AddNoBalls(int run)
		{
            NoBalls += run;
		}

        public void CalcWickets()
        {
            Wickets = 0;
            foreach (Player player in Players){
                if(player.IsOut){
                    Wickets += 1;
                }
            }
        }

        public int NumPlayers()
        {
            return Players.Count;
        }
    }
}
