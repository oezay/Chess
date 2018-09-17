using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springer : Figur {

    public override bool[,] PossibleMove()
    {
        bool[,] arr = new bool[8,8];

        //ObenLinks
        Sprung(x - 1, y + 2, ref arr);

        //ObenRechts
        Sprung(x + 1, y + 2, ref arr);

        //UntenLinks
        Sprung(x - 1, y - 2, ref arr);

        //UntenLinks
        Sprung(x + 1, y - 2, ref arr);


        //LinksOben
        Sprung(x + 2, y + 1, ref arr);

        //RechtsOben
        Sprung(x + 2, y - 1, ref arr);

        //LinksUnten
        Sprung(x - 2, y + 1, ref arr);

        //RechtsUnten
        Sprung(x - 2, y - 1, ref arr);

        return arr;
    }

    public void Sprung(int x, int y, ref bool[,] r)
    {
        Figur fig;

        if(x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            fig = BoardController.Instance.figures[x,y];
            if(fig == null)
            {
                r[x,y] = true;
            }
            else if(isWhite != fig.isWhite)
            {
                r[x,y] = true; 
            }
        }
    }
}
