﻿namespace Yahtzee
{
	public class ScoreboardController
	{
		private ScoreboardView view;
		public ScoreboardModel model;
		private YahtzeeController yahtzeeController;

		public ScoreboardController(YahtzeeController y)
		{
			view = new ScoreboardView(this);
			model = new ScoreboardModel();
			yahtzeeController = y;
		}

		public ScoreboardView getView()
		{
			return view;
		}

		public void ChangeScore(int teerling, int ogen)
		{
			model.Numbers[teerling] = ogen;
		}

		public void ClickCategory(string name)
		{
			int points = 0;
			CountDice();
			switch (name)
			{
				case "acesPointsLbl":
					model.Ace = Sum(1);
					model.SubTotal1 += model.Ace;
					points = model.Ace;
					CheckForBonus();
					break;

				case "twosPointsLbl":
					model.Two = Sum(2);
					model.SubTotal1 += model.Two;
					points = model.Two;
					CheckForBonus();
					break;

				case "threesPointsLbl":
					model.Three = Sum(3);
					model.SubTotal1 += model.Three;
					points = model.Three;
					CheckForBonus();
					break;

				case "foursPointsLbl":
					model.Four = Sum(4);
					model.SubTotal1 += model.Four;
					points = model.Four;
					CheckForBonus();
					break;

				case "fivesPointsLbl":
					model.Five = Sum(5);
					model.SubTotal1 += model.Five;
					points = model.Five;
					CheckForBonus();
					break;

				case "sixesPointsLbl":
					model.Six = Sum(6);
					model.SubTotal1 += model.Six;
					points = model.Six;
					CheckForBonus();
					break;

				case "fullHousePointsLbl":
					if (model.DiceCount[model.DiceCount.Length - 1] >= 3 && model.DiceCount[model.DiceCount.Length - 2] >= 2)
					{
						model.FullHouse = model.PtFullHouse;
						model.SubTotal2 += model.FullHouse;
						points = model.FullHouse;
					}
					break;

				case "fourOKPointsLbl":
					if (model.DiceCount[model.DiceCount.Length - 1] >= 4)
					{
						model.FourOK = CountDiceTotal();
						model.SubTotal2 += model.FourOK;
						points = model.FourOK;
					}
					break;

				case "threeOKPointsLbl":
					if (model.DiceCount[model.DiceCount.Length - 1] >= 3)
					{
						model.ThreeOK = CountDiceTotal();
						model.SubTotal2 += model.ThreeOK;
						points = model.ThreeOK;
					}
					break;

				case "lStraightPointsLbl":
					break;

				case "sStraightPointsLbl":
					break;

				case "yahtzeePointsLbl":
					break;

				case "chancePointsLbl":
					break;

				default:
					break;
			}
			UpdateTotalScores();
			view.SetText(name, points);
			model.AmntOfRounds++;   //Even vlug erbij gezet.
			EndingGame();
		}

		public int Sum(int eye)
		{
			int sum = 0;
			for (int i = 0; i < model.Numbers.Length; i++)
			{
				if (model.Numbers[i] == eye)
				{
					sum += eye;
				}
			}
			return sum;
		}

		public void UpdateTotalScores()
		{
			view.SetText("totalPointsLbl_Upper", model.SubTotal1);
			view.SetText("totalPointsLbl_Lower", model.SubTotal2);
			model.Score = model.SubTotal1 + model.SubTotal2;
			view.SetText("totalPointsLbl", model.Score);

			yahtzeeController.startController.scoreboardControl[yahtzeeController.model.PlayerNumber].KeepingScore(); //Gaat via de YahtzeeController en YahtzeeStart naar de globale ScoreboardController om daar de Methode om de Score te Updaten in het YahtzeeStart scherm aan te halen. Elke YahtzeeController heeft een player number en zo weet de YahtzeeStart welke speler de score wilt updaten.
		}

		public void CheckForBonus()
		{
			if (model.SubTotal1 >= 63)
			{
				model.Bonus = model.PtBonus;
				view.SetText("bonusPointsLbl", model.Bonus);
				model.SubTotal1 += model.Bonus;
			}
		}

		public void CountDice()
		{
			System.Array.Clear(model.DiceCount,0,model.DiceCount.Length);
			for (int i = 0; i < model.Numbers.Length; i++)
			{
				model.DiceCount[model.Numbers[i]-1]++;
			}
			System.Array.Sort(model.DiceCount);
		}

		public int CountDiceTotal()
		{
			int diceTotal = 0;
			for (int i = 0; i < model.Numbers.Length; i++)
			{
				diceTotal += model.Numbers[i];
			}
			return diceTotal;
		}


		public void EndingGame()  //Checkt of de spel ten einde is. Even vlug erbij gezet...
		{
			if (model.AmntOfRounds == 13) {
				yahtzeeController.model.Playing = false;
				yahtzeeController.startController.CheckEndGame();
			}
		}

    // score reseten
    public void ResetScore()
    {
      model.Score = 0;
      model.Ace = 0;
      model.Two = 0;
      model.Three = 0;
      model.Four = 0;
      model.Five = 0;
      model.Six = 0;
        model.Bonus = 0;
			model.SubTotal1 = 0;
      model.ThreeOK = 0;
      model.FourOK = 0;
      model.FullHouse = 0;
      model.SStraight = 0;
      model.LStraight = 0;
			model.YahtzeeSc = 0; 
      model.Chance = 0;
      model.SubTotal2= 0;
      view.ChangeText();
    }

	}
}
