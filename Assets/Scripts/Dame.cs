using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dame : Figur {

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

    int j;

    //LinksOben
    i = x;
    j = y;
    while(true)
    {
        i--;
        j++;
        if(i < 0 || j >= 8)
            break;

        fig = BoardController.Instance.figures[i,j];
        if(fig == null)
        {
            arr[i,j] = true;
        }
        else
        {
            if(atk)
                {
                    arr[i,j] = true;
                    break;
                }
                else
                {
                    if(isWhite != fig.isWhite)
                        arr[i,j] = true;

                    break;
                }
        } 
    }

    //LinksUnten
    i = x;
    j = y;
    while(true)
    {
        i--;
        j--;
        if(i < 0 || j < 0)
            break;

        fig = BoardController.Instance.figures[i,j];
        if(fig == null)
        {
            arr[i,j] = true;
        }
        else
        {
            if(atk)
                {
                    arr[i,j] = true;
                    break;
                }
                else
                {
                    if(isWhite != fig.isWhite)
                        arr[i,j] = true;

                    break;
                }
        } 
    }

    //RechtsUnten
    i = x;
    j = y;
    while(true)
    {
        i++;
        j--;
        if(i >= 8 || j < 0)
            break;

        fig = BoardController.Instance.figures[i,j];
        if(fig == null)
        {
            arr[i,j] = true;
        }
        else
        {
            if(atk)
                {
                    arr[i,j] = true;
                    break;
                }
                else
                {
                    if(isWhite != fig.isWhite)
                        arr[i,j] = true;

                    break;
                }
        } 
    }

    //RechtsOben
    i = x;
    j = y;
    while(true)
    {
        i++;
        j++;
        if(i >= 8 || j >= 8)
            break;

        fig = BoardController.Instance.figures[i,j];
        if(fig == null)
        {
            arr[i,j] = true;
        }
        else
        {
            if(atk)
                {
                    arr[i,j] = true;
                    break;
                }
                else
                {
                    if(isWhite != fig.isWhite)
                        arr[i,j] = true;

                    break;
                }
        } 
    }

    return arr;
}

}
