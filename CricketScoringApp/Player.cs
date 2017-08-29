using System;
namespace CricketScoringApp
{
    public class Player
    {
        public string Name { get; }
        public int RunsScored { get; set; }
        public int BallsFaced { get; set; }
        public Boolean IsOut { get; set; }
        public Boolean HasBatted { get; set; }
        public string MethodOut { get; set; }
        public int RunsConceded { get; set; }
        public int Wickets { get; set; }
        public double OversBowled { get; set; }

        public Player(string name)
        {
            Name = name;
            RunsScored = 0;
            BallsFaced = 0;
            IsOut = false;
            HasBatted = false;
            MethodOut = null;
            RunsConceded = 0;
            Wickets = 0;
            OversBowled = 0;
        }

        public void OversConverter()
        {
            if(OversBowled - Math.Truncate(OversBowled) > 0.5){
                OversBowled = Math.Ceiling(OversBowled);
            }
        }

        public void ScoreRuns(int runs)
        {
            RunsScored += runs;
        }

        public void ConcedeRuns(int runs)
        {
            RunsConceded += runs;
        }

        public void FaceBall()
        {
            BallsFaced += 1;
        }

        public void GetWicket()
        {
            Wickets += 1;
        }

        public void BallBowled()
        {
            OversBowled += 0.1;
            OversConverter();
        }


    }
}
