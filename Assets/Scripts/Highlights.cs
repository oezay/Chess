using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlights : MonoBehaviour {

	public static Highlights Instance{get;set;}

	public GameObject highlightPrefab;
	private List<GameObject> highlights;
	private void Start()
	{
		Instance = this;
		highlights = new List<GameObject> ();

	}

	private GameObject GetHighlightObject()
	{
		GameObject gmeObj = highlights.Find(g => !g.activeSelf);

			if(gmeObj == null)
			{
				gmeObj = Instantiate (highlightPrefab);
				highlights.Add(gmeObj);
			}
			return gmeObj;
	}

	public void HighlightAllowedMoves(bool[,] moves)
	{
		for (int i=0; i<8; i++)
		{
			for (int j=0; j<8; j++)
			{
				if(moves[i,j] == true)
				{
					GameObject gmeObj = GetHighlightObject();
					gmeObj.SetActive(true);
					gmeObj.transform.position = new Vector3(i+0.5f,0,j+0.5f);
				}
			}
		}
	}

	public void Hide()
	{
		foreach (GameObject gmeObj in highlights)
		{
			gmeObj.SetActive(false);
		}
	}
}
