using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turm : Figur {

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

        Figur fig;
        int i;

        //Links
        i = x;
        while(true)
        {
            i--;
            if(i < 0)
                break;

            fig = BoardController.Instance.figures[i,y];
            if(fig == null)
            {
                arr[i,y] = true;
            }
            else
            {
                if(atk)
                {
                    arr[i,y] = true;
                    break;
                }
                else
                {
                    if(isWhite != fig.isWhite)
                        arr[i,y] = true;

                    break;
                }
            }  
        }

        //Rechts
        i = x;
        while(true)
        {
            i++;
            if(i >= 8)
                break;

            fig = BoardController.Instance.figures[i,y];
            if(fig == null)
            {
                arr[i,y] = true;
            }
            else
            {
                if(atk)
                {
                    arr[i,y] = true;
                    break;
                }
                else
                {
                    if(isWhite != fig.isWhite)
                        arr[i,y] = true;

                    break;
                }
            }  
        }

        //Oben
        i = y;
        while(true)
        {
            i++;
            if(i >= 8)
                break;

            fig = BoardController.Instance.figures[x,i];
            if(fig == null)
            {
                arr[x,i] = true;
            }
            else
            {
                if(atk)
                {
                    arr[x,i] = true;
                    break;
                }
                else
                {
                    if(isWhite != fig.isWhite)
                        arr[x,i] = true;

                    break;
                }
            }  
        }

        //Unten
        i = y;
        while(true)
        {
            i--;
            if(i < 0)
                break;

            fig = BoardController.Instance.figures[x,i];
            if(fig == null)
            {
                arr[x,i] = true;
            }
            else
            {
                if(atk)
                {
                    arr[x,i] = true;
                    break;
                }
                else
                {
                    if(isWhite != fig.isWhite)
                        arr[x,i] = true;

                    break;
                }
            }  
        }

        return arr;
    }

}
