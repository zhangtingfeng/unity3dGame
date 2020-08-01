using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GamePlayer
{

    struct ScoreS
    {
        public int AllNum;
        public int FinishedNum;

    }

    public static class Score
    {
        static ScoreS mythisGame;
        public static void iniSucessPercent(int intAllNum)
        {
            mythisGame.AllNum = intAllNum;
            mythisGame.FinishedNum = 0;

        }

        public static void setSucessPercent(int intAddSucessNum)
        {
            mythisGame.FinishedNum += intAddSucessNum;
        }

        public static String getSucessPercent()
        {
            decimal mygetSucessPercent = 0;
            if ((mythisGame.AllNum == 0) || (mythisGame.FinishedNum == 0))
            {
                mygetSucessPercent = 0;
            }
            else
            {
                mygetSucessPercent = (mythisGame.FinishedNum * 100.0).toDecimal() / mythisGame.AllNum;
                //2.保留N位,四舍五入.
                mygetSucessPercent = decimal.Round(decimal.Parse(mygetSucessPercent.toString()), 2);
            }

            return mygetSucessPercent.toString() + "%" + "(" + mythisGame.FinishedNum.toString() + "/" + mythisGame.AllNum.toString() + ")";
        }

    }
}
