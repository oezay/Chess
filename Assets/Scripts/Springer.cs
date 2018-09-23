using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springer : Figur {

    public override bool[,] AttackMove()
    {
        return Move(true);
    }

    public override bool[,] PossibleMove()
    {
        return Move(false);
    }
    private bool[,] Move(bool atk)
    {
        bool[,] arr = new bool[8,8];

        //ObenLinks
        Sprung(x - 1, y + 2, ref arr, atk);

        //ObenRechts
        Sprung(x + 1, y + 2, ref arr, atk);

        //UntenLinks
        Sprung(x - 1, y - 2, ref arr, atk);

        //UntenLinks
        Sprung(x + 1, y - 2, ref arr, atk);


        //LinksOben
        Sprung(x + 2, y + 1, ref arr, atk);

        //RechtsOben
        Sprung(x + 2, y - 1, ref arr, atk);

        //LinksUnten
        Sprung(x - 2, y + 1, ref arr, atk);

        //RechtsUnten
        Sprung(x - 2, y - 1, ref arr, atk);

        return arr;
    }

    public void Sprung(int x, int y, ref bool[,] r, bool atk)
    {
        Figur fig;

        if(x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            fig = BoardController.Instance.figures[x,y];
            if(fig == null)
            {
                r[x,y] = true;
            }
            else if(isWhite != fig.isWhite || atk)
            {
                r[x,y] = true; 
            }
        }
    }
}
