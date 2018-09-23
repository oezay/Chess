using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koenig : Figur {

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

        //links
        if(x-1 >= 0)
        {
            fig = BoardController.Instance.figures[x-1,y];
            if(atk)
            {
                arr[x-1,y] = true;
            }
            else
            {
                if(fig == null || fig.isWhite != isWhite)
                {
                    arr[x-1,y] = true;
                }
            }
        }

        //rechts
        if(x+1 < 8)
        {
            fig = BoardController.Instance.figures[x+1,y];
            if(atk)
            {
                arr[x+1,y] = true;
            }
            else
            {
                if(fig == null || fig.isWhite != isWhite)
                {
                    arr[x+1,y] = true;
                }
            }
        }

        //oben
        if(y+1 < 8)
        {
            fig = BoardController.Instance.figures[x,y+1];
            if(atk)
            {
                arr[x,y+1] = true;
            }
            else
            {
                if(fig == null || fig.isWhite != isWhite)
                {
                    arr[x,y+1] = true;
                }
            }

            //oben links
            if(x-1 >= 0)
            {
                fig = BoardController.Instance.figures[x-1,y+1];
                if(atk)
                {
                    arr[x-1,y+1] = true;
                }
                else
                {
                    if(fig == null || fig.isWhite != isWhite)
                    {
                        arr[x-1,y+1] = true;
                    }
                }
            }

            //oben rechts
            if(x+1 < 8)
            {
                fig = BoardController.Instance.figures[x+1,y+1];
                if(atk)
                {
                    arr[x+1,y+1] = true;
                }
                else
                {
                    if(fig == null || fig.isWhite != isWhite)
                    {
                        arr[x+1,y+1] = true;
                    }
                }
            }
        }

        //unten
        if(y-1 >= 0)
        {
            
            fig = BoardController.Instance.figures[x,y-1];
            if(atk)
            {
                arr[x,y-1] = true;
            }
            else
            {
                if(fig == null || fig.isWhite != isWhite)
                {
                    arr[x,y-1] = true;
                }
            }

            //unten links
            if(x-1 >= 0)
            {
                fig = BoardController.Instance.figures[x-1,y-1];
                if(atk)
                {
                    arr[x-1,y-1] = true;
                }
                else
                {
                    if(fig == null || fig.isWhite != isWhite)
                    {
                        arr[x-1,y-1] = true;
                    }
                }
            }

            //unten rechts
            if(x+1 < 8)
            {
                fig = BoardController.Instance.figures[x+1,y-1];
                if(atk)
                {
                    arr[x+1,y-1] = true;
                }
                else
                {
                    if(fig == null || fig.isWhite != isWhite)
                    {
                        arr[x+1,y-1] = true;
                    }
                }
            }
        }
        return arr;
    }

    

    public bool[,] AttackPath(int dir)
    {
        bool[,] arr = new bool[8,8];
        Figur fig;
        int i;
        
        if(dir == 2)
        {
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
                    arr[x,i] = true;
                    break;
                }  
            }
        }

        if(dir == 6)
        {
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
                    arr[x,i] = true;
                    break;
                }  
            }
        }

        if(dir == 4)
        {
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
                    arr[i,y] = true;
                    break;
                }  
            }
        }

        if(dir == 8)
        {
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
                    arr[i,y] = true;
                    break;

                }  
            }
        }

        int j;

        if(dir == 1)
        {
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
                    arr[i,j] = true;
                    break;
                } 
            }
        }

        if(dir == 3)
        {
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
                    arr[i,j] = true;
                    break;
                } 
            }
        }

        if(dir == 5)
        {
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
                    arr[i,j] = true;
                    break;
                } 
            }
        }

        if(dir == 7)
        {
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
                    arr[i,j] = true;
                    break;
                } 
            }
        }
        Debug.Log(arr.ToString());
        return arr;
    }
}