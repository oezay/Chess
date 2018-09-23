using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

	public static BoardController Instance{set;get;}
	private bool [,] allowedMoves{set;get;}
	private bool[,] atkPath{set;get;}

	private Figur selectedFigure;
	private Koenig koenig;
	private Figur checker;
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
    public List<Camera> cams;
	private List<Figur> activeFigures = new List<Figur>();

	
	private void Start()
	{
		Instance = this;
	//	this.isWhiteTurn = true;
	//	this.check = false;
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

		if(Check())
		{
			if(figures[x,y] == (Figur)koenig)
			{
				allowedMoves = KingMoves();
				selectedFigure = figures[x,y];
			}
			else
			{
				bool[,] moves = new bool[8,8];
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						if (atkPath[i,j] == true)
							if (figures[x,y].PossibleMove()[i,j] == atkPath[i,j])
							{
								moves[i,j] = true;
							}
					}
				}
			allowedMoves = moves;
			selectedFigure = figures[x,y];
			}
		}
		else
		{
			if(figures[x,y] == (Figur)koenig)
			{
				allowedMoves = KingMoves();
				selectedFigure = figures[x,y];
			}
			else
			{
				allowedMoves = figures [x,y].PossibleMove();
				selectedFigure = figures[x,y];
			}
		}
		
		/* 
		previousMat = selectedFigure.GetComponent<MeshRenderer>().material;
		selectedMat.mainTexture = previousMat.mainTexture;
		selectedFigure.GetComponent<MeshRenderer>().material = selectedMat;
		*/

		Highlights.Instance.HighlightAllowedMoves(allowedMoves);
	}

    // Oezay Oeztuerk
	private void MoveFigure(int x, int y)
	{
		if(allowedMoves[x, y])
		{
			Figur fig = figures [x, y];


			
			if(fig != null && fig.isWhite != isWhiteTurn)
			{
				activeFigures.Remove(fig);
				Destroy (fig.gameObject);
			}
			
			figures[selectedFigure.x, selectedFigure.y] = null;
			selectedFigure.transform.position = GetTileCenter(x,y);
			selectedFigure.SetPosition(x,y);
			figures[x, y] = selectedFigure;
			isWhiteTurn = !isWhiteTurn;
            //Schach Gewinn Bedingungen
			if(Check())
				if(Checkmate())
				{
				    EndGame();
				}
			
					

		}
		Highlights.Instance.Hide();
		selectedFigure = null;
	}


	private bool Check()
	{
		foreach (Figur fig in figures)
		{
			if(fig && fig.isWhite == isWhiteTurn && fig.GetType() == typeof(Koenig))
			{
				koenig = (Koenig)fig;
				break;
			}
		}

		foreach (Figur fig in figures)
		{
			if(fig && fig.isWhite != isWhiteTurn && fig.PossibleMove()[koenig.x,koenig.y] == true)
			{
				checker = fig;
				Debug.Log("Check");
				return true;
			}
		}

		return false;
	}


	private bool IsKillable()
	{
		bool b = false;
		foreach(Figur fig in activeFigures)
		{
			if(fig.isWhite == isWhiteTurn)
			{
				if(fig.GetType() == koenig.GetType())
				{
					if(KingMoves()[checker.x,checker.y] == true)
					{
						b = true;
					}
				}
				else
				{
					if(fig.PossibleMove()[checker.x,checker.y] == true)
					{
						b = true;
					}
				}
			}
											
		}
		Debug.Log("Is it Killable " + b);
		return b;
	}

	private bool IsNullable(int dir)
	{	
		Debug.Log(dir);
		
		if(dir == 0)
			{
				atkPath = new bool[8,8];
				atkPath[checker.x,checker.y] = true;
				if(KingMovable())
				{
					return true;
				}
				else
				{
					return IsKillable();
				}
			}
		
		atkPath = koenig.AttackPath(dir);

		foreach(Figur fig in activeFigures)
		{
			if(fig.isWhite == isWhiteTurn)
			{
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						if (atkPath[i,j] == true)
							if (fig.PossibleMove()[i,j] == true)
							{
								return true;
							}
					}
				}
			}
		}
		return false;
	}

	private bool Checkmate()
	{
		Debug.Log(checker.x + " , " + checker.y + "  " + checker.GetType());

		if(checker.GetType() == typeof(Springer) || checker.GetType() == typeof(Bauer))
		{
			return !IsNullable(0);
		}

		if(checker.x == koenig.x)
		{
			//Oben
			if(checker.y > koenig.y)
			{
				if(checker.y == koenig.y + 1)
				{
					return !IsNullable(0);
				}
				else
				{	
					return !IsNullable(2);
				}
			}
			//Unten
			else
			{
				if(checker.y == koenig.y - 1)
				{
					return !IsNullable(0);
				}
				else
				{
					return !IsNullable(6);
				}
			}
		}

		if(checker.y == koenig.y)
		{
			//Rechts
			if(checker.x > koenig.x)
			{
				if(checker.x == koenig.x + 1)
				{
					return !IsNullable(0);
				}
				else
				{	
					return !IsNullable(4);
				}
			}
			//Links
			else
			{
				if(checker.x == koenig.x - 1)
				{
					return !IsNullable(0);
				}
				else
				{
					return !IsNullable(8);
				}
			}
		}

		if(checker.x > koenig.x)
		{
			//Rechtsoben
			if(checker.y > koenig.y)
			{
				if(checker.y == koenig.y + 1)
				{
					return !IsNullable(0);
				}
				else
				{	
					return !IsNullable(3);
				}
			}
			//Rechtsunten
			else
			{
				if(checker.y == koenig.y - 1)
				{
					return !IsNullable(0);
				}
				else
				{
					return !IsNullable(5);
				}
			}
		}

		if(checker.x < koenig.x)
		{
			//Linksoben
			if(checker.y > koenig.y)
			{
				if(checker.y == koenig.y + 1)
				{
					return !IsNullable(0);
				}
				else
				{	
					return !IsNullable(1);
				}
			}
			//Linkssunten
			else
			{
				if(checker.y == koenig.y - 1)
				{
					return !IsNullable(0);
				}
				else
				{
					return !IsNullable(7);
				}
			}
		}

		Debug.Log("Checkmat Ackcomplished");
		return !IsKillable();
	}

	private bool[,] KingMoves()
	{
		bool[,] moves = koenig.PossibleMove();
		foreach (Figur fig in activeFigures)
			{
				if(fig.isWhite != isWhiteTurn)
				{
					for (int i = 0; i < 8; i++)
					{
						for (int j = 0; j < 8; j++)
						{
							if (moves[i,j] == true)
								if (fig.AttackMove()[i,j])
								{
									moves[i,j] = false;
								}
						}
					}
				}
			}
		//if(Check())

		return moves;
	}

	private bool KingMovable()
	{
		bool b = false;
		bool [,] moves = KingMoves();
		
		for(int i = 0; i < 8; i++)
		{
			for(int j = 0; j < 8; j++)
			{
				if(moves[i,j] == true)
					b = true;
			}
		}
		return b;
	}

	private void SpawnFigure(int index, int x, int y, Quaternion orientation)
	{
		GameObject gmeObj = Instantiate(figurePrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
		gmeObj.transform.SetParent(transform);
		figures[x,y] = gmeObj.GetComponent<Figur>();
		figures[x,y].SetPosition(x,y);
		activeFigures.Add(gmeObj.GetComponent<Figur>());		
	}

	private void SpawnMatch()
	{
		figures = new Figur[8, 8];

		Quaternion white = Quaternion.Euler(0,0,0);
		Quaternion black = Quaternion.Euler(0,180,0);

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
		if(!getCurrentCamera()) return;

		RaycastHit hit;
		if(Physics.Raycast(getCurrentCamera().ScreenPointToRay(Input.mousePosition),out hit,25.0f,LayerMask.GetMask("ChessPlane")))
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


    public Camera getCurrentCamera()
    {
        if(isWhiteTurn)
        {
            cams[1].enabled = false;
            cams[0].enabled = true;
            return cams[0];
        } else
        {
            cams[1].enabled = true;
            cams[0].enabled = false;
            return cams[1];
        }
    }



	private Vector3 GetTileCenter(int x, int y)
	{
		Vector3 center = Vector3.zero;
		center.x += (TILESIZE * x) + OFFSET;
		center.z += (TILESIZE * y) + OFFSET;

		return center;
	}
	
	private void drawChessboard()
	{

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

	public void EndGame()
	{
		if(isWhiteTurn)
		{
			Debug.Log("Black wins");
		}
		else
		{
			Debug.Log("Black wins");
		}
		foreach(Figur fig in activeFigures)
		{
			Destroy(fig.gameObject);
		}
		foreach(Figur fig in figures)
		{
			activeFigures.Remove(fig);
		}
		
		isWhiteTurn = true;
		SpawnMatch();
	}
}