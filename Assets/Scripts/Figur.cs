using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Figur : MonoBehaviour {

	public int x;
	public int y;
	public bool isWhite;

	public void SetPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public virtual bool[,] PossibleMove()
	{
		return new bool[8,8];
	}
}
