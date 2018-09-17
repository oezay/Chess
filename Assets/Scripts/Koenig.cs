using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koenig : Figur {

    public override bool[,] PossibleMove()
    {
        bool[,] arr = new bool[8,8];
        Figur fig;

        //links
        if(x-1 >= 0)
        {
            fig = BoardController.Instance.figures[x-1,y];
            if(fig == null || fig.isWhite != isWhite)
            {
                arr[x-1,y] = true;
            }
        }

        //rechts
        if(x+1 < 8)
        {
            fig = BoardController.Instance.figures[x+1,y];
            if(fig == null || fig.isWhite != isWhite)
            {
                arr[x+1,y] = true;
            }
        }

        //oben
        if(y+1 < 8)
        {
            fig = BoardController.Instance.figures[x,y+1];
            if(fig == null || fig.isWhite != isWhite)
            {
                arr[x,y+1] = true;
            }

            //oben links
            if(x-1 >= 0)
            {
                fig = BoardController.Instance.figures[x-1,y+1];
                if(fig == null || fig.isWhite != isWhite)
                {
                    arr[x-1,y+1] = true;
                }
            }

            //oben rechts
            if(x+1 < 8)
            {
                fig = BoardController.Instance.figures[x+1,y+1];
                if(fig == null || fig.isWhite != isWhite)
                {
                    arr[x+1,y+1] = true;
                }
            }
        }

        //unten
        if(y-1 >= 0)
        {
            
            fig = BoardController.Instance.figures[x,y-1];
            if(fig == null || fig.isWhite != isWhite)
            {
                arr[x,y-1] = true;
            }

            //unten links
            if(x-1 >= 0)
            {
                fig = BoardController.Instance.figures[x-1,y-1];
                if(fig == null || fig.isWhite != isWhite)
                {
                    arr[x-1,y-1] = true;
                }
            }

            //unten rechts
            if(x+1 < 8)
            {
                fig = BoardController.Instance.figures[x+1,y-1];
                if(fig == null || fig.isWhite != isWhite)
                {
                    arr[x+1,y-1] = true;
                }
            }
        }
        return arr;
    }
}