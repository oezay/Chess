using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

	public static BoardController Instance{set;get;}
	private bool [,] allowedMoves{set;get;}

	private Figur selectedFigure;
	public Figur[,] figures;

	public bool isWhiteTurn = true;

	/*Functionality for highlight selected figure.
	private Material previousMat;
	public Material selectedMat;*/

	public static float TILESIZE = 1.0f;
	public static float OFFSET = 0.5f;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> figurePrefabs;
	private List<GameObject> activeFigures = new List<GameObject>();

	
	private void Start()
	{
		Instance = this;
		Debug.Log("Spawned");
		SpawnMatch();
	}
	private void Update()
	{
		UpdateSelection();
		drawChessboard();

		if(Input.GetMouseButtonDown(0)){
			Click();
		}
	}

	private void Click()
	{
		if(selectionX >= 0 && selectionY >= 0){
				if(selectedFigure == null)
				{
					SelectFigure(selectionX, selectionY);
				}
				else
				{
					MoveFigure(selectionX, selectionY);
				}
			}
	}

	private void SelectFigure(int x, int y)
	{
		if(figures[x,y] == null)
			return;

		if(figures[x,y].isWhite != isWhiteTurn)
			return;

		allowedMoves = figures [x,y].PossibleMove();
		selectedFigure = figures[x,y];

		/* 
		previousMat = selectedFigure.GetComponent<MeshRenderer>().material;
		selectedMat.mainTexture = previousMat.mainTexture;
		selectedFigure.GetComponent<MeshRenderer>().material = selectedMat;
		*/

		Highlights.Instance.HighlightAllowedMoves(allowedMoves);
	}

	private void MoveFigure(int x, int y)
	{
		if(allowedMoves[x, y])
		{
			Figur fig = figures [x, y];


			//Schach Gewinn Bedingungen
			if(fig != null && fig.isWhite != isWhiteTurn)
			{

				if(fig.GetType() == typeof(Koenig))
				{
					return;
				}


				activeFigures.Remove(fig.gameObject);
				Destroy (fig.gameObject);
			}

			figures[selectedFigure.x, selectedFigure.y] = null;
			selectedFigure.transform.position = GetTileCenter(x,y);
			selectedFigure.SetPosition(x,y);
			figures[x, y] = selectedFigure;
			isWhiteTurn = !isWhiteTurn;
		}

		Highlights.Instance.Hide();
		selectedFigure = null;
	}

	private void SpawnFigure(int index, int x, int y, Quaternion orientation)
	{
		GameObject gmeObj = Instantiate(figurePrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
		gmeObj.transform.SetParent(transform);
		figures[x,y] = gmeObj.GetComponent<Figur> ();Debug.Log("Spawned");
		figures[x,y].SetPosition(x,y);
		activeFigures.Add(gmeObj);
		
	}

	private void SpawnMatch()
	{
		figures = new Figur[8, 8];

		Quaternion white = Quaternion.Euler(-90,0,0);
		Quaternion black = Quaternion.Euler(-90,180,0);

		//Bauern
		for(int i = 0; i < 8; i++)
		{
			SpawnFigure(5, i, 1, white);

			SpawnFigure(11, i, 6, black);
		}

		//Tuerme
		SpawnFigure(2, 0, 0, white);
		SpawnFigure(2, 7, 0, white);

		SpawnFigure(8, 0, 7, black);
		SpawnFigure(8, 7, 7, black);


		//Damen
		SpawnFigure(1, 3, 0, white);

		SpawnFigure(7, 3, 7, black);


		//Springer
		SpawnFigure(4, 1, 0, white);
		SpawnFigure(4, 6, 0, white);

		SpawnFigure(10, 1, 7, black);
		SpawnFigure(10, 6, 7, black);


		//Laeufer
		SpawnFigure(3, 2, 0, white);
		SpawnFigure(3, 5, 0, white);

		SpawnFigure(9, 2, 7, black);
		SpawnFigure(9, 5, 7, black);


		//Könige
		SpawnFigure(0, 4, 0, white);

		SpawnFigure(6, 4, 7, black);
		
	}

	private void UpdateSelection()
	{
		if(!Camera.main) return;

		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,25.0f,LayerMask.GetMask("ChessPlane")))
		{
			//Debug.Log(hit.point);
			selectionX = (int) hit.point.x;
			selectionY = (int) hit.point.z;
		}
		else 
		{
			selectionX = -1;
			selectionY = -1;
		}
	}	

	private Vector3 GetTileCenter(int x, int y)
	{
		Vector3 center = Vector3.zero;
		center.x += (TILESIZE * x) + OFFSET;
		center.z += (TILESIZE * y) + OFFSET;

		return center;
	}
	
	private void drawChessboard(){

		Vector3 widthLine = Vector3.right * 8;
		Vector3 heightLine = Vector3.forward * 8;

		for(int i = 0; i <=8; i++)
		{
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine, Color.green, Time.deltaTime, false); 
			for(int j = 0; j <=8; j++)
			{
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine); 
			}
		}

		//Draw Tile Selection
		if(selectionX >= 0 && selectionY >= 0)
		{
			Debug.DrawLine(	Vector3.forward * selectionY + Vector3.right * selectionX, 
							Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

			Debug.DrawLine(	Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
							Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
		}
	}
}

