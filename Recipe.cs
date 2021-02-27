using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recipe : MonoBehaviour
{
    // [System.Serializable]
    // public struct RecipePiecePrefab
    // {
    //     // public PieceType type; // normal bubble.. 
        
    // };

    public GameObject ReciPiecePrefab;
    public GameObject ReciBGPrefab;

    [System.Serializable]
    public struct PiecePosition
    {
        // public PieceType type; // normal bubble.. 
        public int x;
        public int y;
    };

    public int xDim;
    public int yDim;
    public float fillTime;

    // TODO Get automatically or at least make an assertion
    public Level level;

    // public GameObject backgroundPrefab; // 每個格子的背景 hierarchy/grid

    public PiecePosition[] initialPieces;

    public int numPerRecipe;

    // private Dictionary<PieceType, GameObject> _piecePrefabDict;

    private ReciPiece[,] _pieces;

    private bool _inverse = false;

    // private GamePiece _pressedPiece;
    // private GamePiece _enteredPiece;

    // private bool _gameOver;

    private bool _isFilling;

    public bool IsFilling => _isFilling;

    private void Awake()
    {
        // // populating dictionary with piece prefabs types
        // _piecePrefabDict = new Dictionary<PieceType, GameObject>();
        // for (int i = 0; i < recipePiecePrefabs.Length; i++)
        // {
        //     if (!_piecePrefabDict.ContainsKey(recipePiecePrefabs[i].type))
        //     {
        //         _piecePrefabDict.Add(recipePiecePrefabs[i].type, recipePiecePrefabs[i].prefab);
        //     }
        // }

        for (int x = 0; x < xDim; x++)
        {
            // instantiate backgrounds
            GameObject recipeGround = Instantiate(ReciBGPrefab, GetWorldPosition(x, yDim), Quaternion.identity);
            recipeGround.transform.parent = transform;

            // instantiate veggies
            // for (int y = 0; y < yMax; y++)
            // {
            //     GameObject recipeVeggie = Instantiate(recipePiecePrefab, GetWorldPosition(x, y), Quaternion.identity);
            //     recipeVeggie.transform.parent = transform;
            // } 
        }

        // instantiating pieces
        _pieces = new ReciPiece[xDim, numPerRecipe];

        // for (int i = 0; i < initialPieces.Length; i++)
        // {
        //     if (initialPieces[i].x >= 0 && initialPieces[i].y < xDim
        //         && initialPieces[i].y >=0 && initialPieces[i].y <yDim)
        //     {
        //         SpawnNewPiece(initialPieces[i].x, initialPieces[i].y, initialPieces[i].type);
        //     }
        // }

        StartCoroutine(Fill());
    }

    private IEnumerator Fill()
    {       
        yield return new WaitForSeconds(fillTime);
         for (int x = 0; x < xDim; x++)
            {
                // 每列生成数量随机
                int yMax = Random.Range(1, numPerRecipe + 1);
                for (int y = 0; y < yMax; y++)
                {
                    if (_pieces[x, y] == null)
                    {
                        SpawnNewPiece(x, y);
                        Debug.Log(_pieces[x, y]);
                        _pieces[x, y].ColorComponent.SetColor((ColorType)Random.Range(0, _pieces[x, y].ColorComponent.NumColors));
                    }                
                }
            } 
         
        // bool needsRefil = true;
        // _isFilling = true;

        // while (needsRefil)
        // {
            // yield return new WaitForSeconds(fillTime);
            // while (FillStep())
            // {
            //     _inverse = !_inverse;
            //     yield return new WaitForSeconds(fillTime);
            // }

            // needsRefil = ClearAllValidMatches();
        // }

        // _isFilling = false;
    }

    /// <summary>
    /// One pass through all grid cells, moving them down one grid, if possible.
    /// </summary>
    /// <returns> returns true if at least one piece is moved down</returns>
    // private bool FillStep()
    // {
    //     bool movedPiece = false;
    //     // y = 0 is at the top, we ignore the last row, since it can't be moved down.
    //     for (int y = yDim + numPerRecipe - 2; y >= 0; y--)
    //     {
    //         for (int loopX = 0; loopX < xDim; loopX++)
    //         {
    //             int x = loopX;
    //             if (_inverse) { x = xDim - 1 - loopX; }
    //             GamePiece piece = _pieces[x, y];

    //             if (!piece.IsMovable()) continue;
                
    //             GamePiece pieceBelow = _pieces[x, y + 1];

    //             if (pieceBelow.Type == PieceType.EMPTY)
    //             {
    //                 Destroy(pieceBelow.gameObject);
    //                 piece.MovableComponent.Move(x, y + 1, fillTime);
    //                 _pieces[x, y + 1] = piece;
    //                 SpawnNewPiece(x, y, PieceType.EMPTY);
    //                 movedPiece = true;
    //             }
    //             else
    //             {
    //                 for (int diag = -1; diag <= 1; diag++)
    //                 {
    //                     if (diag == 0) continue;
                        
    //                     int diagX = x + diag;

    //                     if (_inverse)
    //                     {
    //                         diagX = x - diag;
    //                     }

    //                     if (diagX < 0 || diagX >= xDim) continue;
                        
    //                     GamePiece diagonalPiece = _pieces[diagX, y + 1];

    //                     if (diagonalPiece.Type != PieceType.EMPTY) continue;
                        
    //                     bool hasPieceAbove = true;

    //                     for (int aboveY = y; aboveY >= 0; aboveY--)
    //                     {
    //                         GamePiece pieceAbove = _pieces[diagX, aboveY];

    //                         if (pieceAbove.IsMovable())
    //                         {
    //                             break;
    //                         }
    //                         else if (/*!pieceAbove.IsMovable() && */pieceAbove.Type != PieceType.EMPTY)
    //                         {
    //                             hasPieceAbove = false;
    //                             break;
    //                         }
    //                     }

    //                     if (hasPieceAbove) continue;
                        
    //                     Destroy(diagonalPiece.gameObject);
    //                     piece.MovableComponent.Move(diagX, y + 1, fillTime);
    //                     _pieces[diagX, y + 1] = piece;
    //                     SpawnNewPiece(x, y, PieceType.EMPTY);
    //                     movedPiece = true;
    //                     break;
    //                 }
    //             }
    //         }
    //     }

    //     // the highest row (0) is a special case, we must fill it with new pieces if empty
    //     for (int x = 0; x < xDim; x++)
    //     {
    //         GamePiece pieceBelow = _pieces[x, 0];

    //         if (pieceBelow.Type != PieceType.EMPTY) continue;
            
    //         Destroy(pieceBelow.gameObject);
    //         GameObject newPiece = (GameObject)Instantiate(_piecePrefabDict[PieceType.NORMAL], GetWorldPosition(x, -1), Quaternion.identity, this.transform);

    //         _pieces[x, 0] = newPiece.GetComponent<GamePiece>();
    //         _pieces[x, 0].Init(x, -1, this, PieceType.NORMAL);
    //         _pieces[x, 0].MovableComponent.Move(x, 0, fillTime);
    //         _pieces[x, 0].ColorComponent.SetColor((ColorType)Random.Range(0, _pieces[x, 0].ColorComponent.NumColors));
    //         movedPiece = true;
    //     }

    //     return movedPiece;
    // }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(transform.position.x - xDim / 2.0f + x,
                           transform.position.y + yDim / 2.0f - y);
    }

    private ReciPiece SpawnNewPiece(int x, int y)
    {
        GameObject newPiece = (GameObject)Instantiate(ReciPiecePrefab, GetWorldPosition(x, y), Quaternion.identity, this.transform);
        // _pieces[x, y] = newPiece.GetComponent<ReciPiece>();
        // ReciPiece piece = _pieces[x, y];
        ReciPiece piece = newPiece.GetComponent<ReciPiece>();
        _pieces[x, y] = piece;
        _pieces[x, y].Init(x, y, this);

        return _pieces[x, y];
    }

    // private static bool IsAdjacent(GamePiece piece1, GamePiece piece2)
    // {
    //     return (piece1.X==piece2.X && (int)Mathf.Abs(piece1.Y - piece2.Y) == 1)
    //         || (piece1.Y == piece2.Y && (int)Mathf.Abs(piece1.X - piece2.X) == 1);
    // }

    // private bool ClearPiece(int x, int y)
    // {
    //     if (!_pieces[x, y].IsClearable() || _pieces[x, y].ClearableComponent.IsBeingCleared) return false;
        
    //     _pieces[x, y].ClearableComponent.Clear();
    //     SpawnNewPiece(x, y, PieceType.EMPTY);

    //     ClearObstacles(x, y);

    //     return true;

    // }

    // public void ClearColumn(int column)
    // {
    //     for (int y = 0; y < yDim; y++)
    //     {
    //         ClearPiece(column, y);
    //     }
    // }
    
    // public List<GamePiece> GetPiecesOfType(PieceType type)
    // {
    //     var piecesOfType = new List<GamePiece>();

    //     for (int x = 0; x < xDim; x++)
    //     {
    //         for (int y = 0; y < yDim; y++)
    //         {
    //             if (_pieces[x, y].Type == type)
    //             {
    //                 piecesOfType.Add(_pieces[x, y]);
    //             }
    //         }
    //     }

    //     return piecesOfType;
    // }

}
