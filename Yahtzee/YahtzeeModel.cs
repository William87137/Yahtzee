﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
	public class YahtzeeModel
	{
		private int aantalTeerlingen = 5; //, aantalWorpen = 0;

		public int AantalTeerlingen {
			get { return aantalTeerlingen; }
			set { aantalTeerlingen = value; }
		}

		//public int AantalWorpen
		//{
		//  get { return aantalWorpen; }
		//  set { aantalWorpen = value; }
		//}  

	}
}
