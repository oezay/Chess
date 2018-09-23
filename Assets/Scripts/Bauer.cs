using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bauer : Figur {

    public override bool[,] PossibleMove()
    {
        bool[,] arr = new bool[8,8];

        Figur fig1,fig2;

        if (isWhite)
        {
            //Angriff Links
            if (x != 0 && y != 7)
            {
                fig1 = BoardController.Instance.figures [x - 1, y + 1];
                if(fig1 != null && !fig1.isWhite)
                {
                    arr[x - 1,y + 1] = true;
                }
            }

            //Angriff Rechts
            if (x != 7 && y != 7)
            {
                fig1 = BoardController.Instance.figures [x + 1, y + 1];
                if(fig1 != null && !fig1.isWhite)
                {
                    arr[x + 1,y + 1] = true;
                }
            }

            //Ein Schritt
            if(y != 7)
            {
                fig1 = BoardController.Instance.figures [x, y + 1];
                if(fig1 == null)
                {
                    arr[x,y + 1] = true;
                }
            }

            //Zwei Schritt
            if(y == 1)
            {
                fig1 = BoardController.Instance.figures[x, y + 1];
                fig2 = BoardController.Instance.figures[x, y + 2];
                if(fig1 == null && fig2 == null)
                {
                    arr[x,y + 2] = true;
                }
            }
        }
        else
        {
            //Angriff Links
            if (x != 0 && y != 0)
            {
                fig1 = BoardController.Instance.figures [x - 1, y - 1];
                if(fig1 != null && fig1.isWhite)
                {
                    arr[x - 1,y - 1] = true;
                }
            }

            //Angriff Rechts
            if (x != 7 && y != 0)
            {
                fig1 = BoardController.Instance.figures [x + 1, y - 1];
                if(fig1 != null && fig1.isWhite)
                {
                    arr[x + 1,y - 1] = true;
                }
            }

            //Ein Schritt
            if(y != 7)
            {
                fig1 = BoardController.Instance.figures [x, y - 1];
                if(fig1 == null)
                {
                    arr[x,y - 1] = true;
                }
            }

            //Zwei Schritt
            if(y == 6)
            {
                fig1 = BoardController.Instance.figures[x, y - 1];
                fig2 = BoardController.Instance.figures[x, y - 2];
                if(fig1 == null && fig2 == null)
                {
                    arr[x,y - 2] = true;
                }
            }
        }
        return arr;
    }
    //Vitalij Becker
    public override bool[,] AttackMove()
    {
        bool[,] arr = new bool[8,8];
 

        if (isWhite)
        {
            
            //Angriff Links
            if (x != 0 && y != 7)
            {
                arr[x - 1,y + 1] = true;
            }

            //Angriff Rechts
            if (x != 7 && y != 7)
            {
                arr[x + 1,y + 1] = true;
            }
        }
        else
        {
            //Angriff Links
            if (x != 0 && y != 0)
            {
                arr[x - 1,y - 1] = true;
            }

            //Angriff Rechts
            if (x != 7 && y != 0)
            {
                arr[x + 1,y - 1] = true;
            }
        }
        return arr;
    }



}
