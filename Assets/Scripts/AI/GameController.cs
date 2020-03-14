using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region 2D variables
    [SerializeField]
    GameObject CasePefab2D;
    GameObject grid2D;
    [SerializeField]
    Camera camera2D;
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    Sprite board2D;
    float w;
    float offsetcase;
    #endregion

    #region 3D variables
    [SerializeField]
    GameObject CasePefab3D;
    GameObject grid3D;
    [SerializeField]
    Camera camera3D;
    [SerializeField]
    GameObject[] prefabs;

    [SerializeField]
    GameObject board3D;
    #endregion
    [SerializeField]
    Button[] btns;
    Data.GameManager gm;

    [SerializeField]
    GameObject chessPiece;


    [SerializeField]
    GameObject possibleCase;

    GameObject[,] possibleCases2D;
    GameObject[,] possibleCases3D;
    GameObject[] kings;
    GameObject player1, player2;

    bool playerTurn, ai, is3D, gameStarted;
    public bool mouvement;

    Data.ChessPiece selectChessPieceData;

    public bool GameStarted
    {
        get
        {
            return gameStarted;
        }

        set
        {
            gameStarted = value;
        }
    }

    //player one has black pieces
    void Start()
    {
        gm = new Data.GameManager();
        gm.Tree = new Data.Tree(gm.Board);
        playerTurn = false;
        mouvement = false;
        is3D = false;
        gameStarted = false;
        ai = true;

        selectChessPieceData = null;

        grid2D = new GameObject();
        grid2D.name = "Display 2D Grid possibilities";

        grid3D = new GameObject();
        grid3D.name = "Display 3D Grid possibilities";

        player1 = new GameObject();
        player1.name = "Player1";

        player2 = new GameObject();
        player2.name = "Player2";

        possibleCases2D = new GameObject[8, 8];
        possibleCases3D = new GameObject[8, 8];

        kings = new GameObject[2];

        Initialize2DGameObjects();
        Initialize3DGameObjects();
    }

    void Initialize2DGameObjects()
    {

        w = (board2D.bounds.size.x / 8.0f);
        offsetcase = w * 0.5f;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Data.ChessPiece ChessPieceData = gm.Board.GetChessPiece(i, j);
                GameObject gObjt;
                if (ChessPieceData != null)
                {

                    gObjt = Instantiate(chessPiece);
                    gObjt.transform.position = Find2DWorldPosition(i, j);
                    int offset = (ChessPieceData.Color == Data.Color.BLACK) ? 0 : 6;

                    gObjt.GetComponent<SpriteRenderer>().sprite = sprites[(int)ChessPieceData.Type - 1 + offset];
                    gObjt.name = gObjt.GetComponent<SpriteRenderer>().sprite.name;
                    gObjt.transform.parent = (offset == 6) ? player2.transform : player1.transform;
                    ChessPieceData.GameObject2D = gObjt;
                }

                gObjt = Instantiate(CasePefab2D);
                gObjt.transform.position = Find2DWorldPosition(i, j);
                gObjt.transform.position = new Vector2(gObjt.transform.position.x, gObjt.transform.position.y);
                gObjt.name = "[" + i + "]" + "[" + j + "]";
                possibleCases2D[i, j] = gObjt;
                possibleCases2D[i, j].SetActive(false);
                possibleCases2D[i, j].transform.parent = grid2D.transform;
            }
        }
    }

    void Initialize3DGameObjects()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Data.ChessPiece ChessPieceData = gm.Board.GetChessPiece(i, j);
                GameObject gObjt;
                if (ChessPieceData != null)
                {
                    int offset = (ChessPieceData.Color == Data.Color.BLACK) ? 0 : 6;
                    gObjt = Instantiate(prefabs[(int)ChessPieceData.Type - 1 + offset]);
                    gObjt.transform.position = Find3DWorldPosition(i, j);
                    gObjt.transform.parent = (offset == 6) ? player2.transform : player1.transform;
                    if (ChessPieceData.Type == Data.Piece.KING)
                    {
                        kings[(int)ChessPieceData.Color] = gObjt;
                    }
                    ChessPieceData.GameObject3D = gObjt;
                }

                gObjt = Instantiate(CasePefab3D);
                gObjt.transform.position = Find3DWorldPosition(i, j);
                gObjt.name = "[" + i + "]" + "[" + j + "]";
                possibleCases3D[i, j] = gObjt;
                possibleCases3D[i, j].SetActive(false);
                possibleCases3D[i, j].transform.parent = grid3D.transform;
            }
        }
    }

    private void Clear()
    {
        if(is3D)
        {
            ChangeMode();
        }
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Data.ChessPiece ChessPieceData = gm.Board.GetChessPiece(i, j);
   
                if (ChessPieceData != null)
                {
                    Destroy(ChessPieceData.GameObject2D);
                    Destroy(ChessPieceData.GameObject3D);
                }
                Destroy(possibleCases2D[i, j]);
                Destroy(possibleCases3D[i, j]);
            }
        }
    }

    public Vector3 Find3DWorldPosition(int _i, int _j)
    {
        return new Vector3(board3D.transform.position.x + _i * 2 - 7, board3D.transform.position.y + 1.2f, board3D.transform.position.z + _j * 2 - 7);
    }

    public Vector2Int Find3DGridPosition(Vector3 pos)
    {
        int i = (int)((8 + pos.x - board3D.transform.position.x) / 2);
        int j = (int)((8 + pos.z - board3D.transform.position.z) / 2);
        return new Vector2Int(i, j);
    }

    public Vector3 Find2DWorldPosition(int _i, int _j)
    {
        float scale = 8.0f;
        return new Vector3(
            (board2D.bounds.min.y + w * _i + offsetcase) * scale,
            (board2D.bounds.min.x + w * _j + offsetcase) * scale,
             1.0f) + camera2D.transform.position;
    }

    public Vector2Int Find2DGridPosition(Vector2 pos)
    {
        float scale = 8.0f;
        int i = (int)((((pos.x - camera2D.transform.position.x) / scale) - board2D.bounds.min.x) / w);
        int j = (int)((((pos.y - camera2D.transform.position.y) / scale) - board2D.bounds.min.y) / w);
        return new Vector2Int(i, j);
    }

    public void SelectPiece(Vector2Int _position, Data.Color _color)
    {

        selectChessPieceData = gm.Board.GetChessPiece(_position.x, _position.y);

        bool kingIsFocus = gm.Board.KingIsFocus(_color);
        if (kingIsFocus)
        {
            Debug.Log("Le roi est en echec");
            GameObject GB = kings[(int)_color];
            for (int j = 0; j < GB.transform.childCount; ++j)
            {
                Renderer rend = GB.transform.GetChild(j).gameObject.GetComponent<Renderer>();
                rend.material.color = Color.red;
            }
        }

        //Display possible cases
        if (selectChessPieceData != null && selectChessPieceData.Color == _color)
        {

            List<Vector2Int> solution = gm.Board.FindNextPositions(_position.x, _position.y);

            for (int i = solution.Count - 1; i >= 0; i--)
            {
                Data.Grid g = new Data.Grid(gm.Board);

                g.SetId(_position.x, _position.y, null);
                g.SetId(solution[i].x, solution[i].y, selectChessPieceData);
                kingIsFocus = g.KingIsFocus(_color);
                if (kingIsFocus)
                {
                    solution.RemoveAt(i);
                }
                else
                {
                    possibleCases2D[solution[i].x, solution[i].y].SetActive(true);
                    possibleCases3D[solution[i].x, solution[i].y].SetActive(true);


                    if (gm.Board.GetChessPiece(solution[i].x, solution[i].y) == null)
                    {
                        GameObject GB = Instantiate(selectChessPieceData.GameObject3D);
                        GB.transform.position = possibleCases3D[solution[i].x, solution[i].y].transform.position;
                        GB.transform.parent = possibleCases3D[solution[i].x, solution[i].y].transform;
                        Material m = GB.transform.parent.GetComponent<Renderer>().material;
                        for (int j = 0; j < GB.transform.childCount; ++j)
                        {
                            Renderer rend = GB.transform.GetChild(j).gameObject.GetComponent<Renderer>();
                            rend.material = m;
                        }
                    }
                }
            }
        }
    }

    public bool MovePiecePlayer(Vector2Int _position, Data.Color _color)
    {
        bool isMoving = false;

        Vector2Int currentPieceInGrid = gm.Board.GetId(selectChessPieceData);
        Data.ChessPiece ChessPieceData = gm.Board.GetChessPiece(_position.x, _position.y);

        if (_position.x >= 0 && _position.x < 8 &&
            _position.y >= 0 && _position.y < 8 &&
            possibleCases2D[_position.x, _position.y].activeSelf)
        {
            if (ChessPieceData != null)
            {
                Destroy(ChessPieceData.GameObject2D);
                Destroy(ChessPieceData.GameObject3D);
                Data.whitePieces--;
            }
            gm.Board.SetId(currentPieceInGrid.x, currentPieceInGrid.y, null);
            gm.Board.SetId(_position.x, _position.y, selectChessPieceData);

            Promotion(_position, _color);
            playerTurn = !playerTurn;
            isMoving = true;
        }

        //clear possible cases
        DesactivatePossibleCases();

        return isMoving;
    }

    private void DesactivatePossibleCases()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                possibleCases2D[i, j].SetActive(false);
                possibleCases3D[i, j].SetActive(false);
                for (int k = 0; k < possibleCases3D[i, j].transform.childCount; ++k)
                {
                    Destroy(possibleCases3D[i, j].transform.GetChild(k).gameObject);
                }

            }
        }
    }

    void Update()
    {
        if (!mouvement && gameStarted)
        {
            if (!playerTurn)
            {
                Player(Data.Color.BLACK);
            }
            else
            {
                if (ai)
                {
                    AI();
                }
                else
                {
                    Player(Data.Color.WHITE);
                }
            }
        }
        mouvement = false;
    }

    private void Player(Data.Color _color)
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (selectChessPieceData != null)
            {
                Vector2Int gridPos = Vector2Int.zero;

                if (!is3D)
                {
                    RaycastHit hit;
                    Ray r = camera3D.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(r, out hit))
                    {
                        gridPos = Find3DGridPosition(hit.point);
                    }
                }
                else gridPos = Find2DGridPosition(camera2D.ScreenToWorldPoint(Input.mousePosition));

                if (MovePiecePlayer(gridPos, _color))
                {
                    selectChessPieceData.GameObject2D.GetComponent<ChessPiece>().target = Find2DWorldPosition(gridPos.x, gridPos.y);
                    selectChessPieceData.GameObject3D.GetComponent<ChessPiece>().target = Find3DWorldPosition(gridPos.x, gridPos.y);
                    selectChessPieceData = null;

                    GameObject GB = kings[(int)_color];
                    for (int j = 0; j < GB.transform.childCount; ++j)
                    {
                        Renderer rend = GB.transform.GetChild(j).gameObject.GetComponent<Renderer>();
                        rend.material.color = (_color == Data.Color.BLACK) ? new Color(0.5f, 0.5f, 0.5f, 1.0f) : Color.white;
                    }
                }
            }
            if (!playerTurn == (_color == Data.Color.BLACK) ? true : false)
            {
                Vector2Int gridPos = Vector2Int.zero;
                if (!is3D)
                {
                    RaycastHit hit;
                    Ray r = camera3D.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(r, out hit))
                    {
                        gridPos = Find3DGridPosition(hit.point);
                    }
                }
                else gridPos = Find2DGridPosition(camera2D.ScreenToWorldPoint(Input.mousePosition));


                SelectPiece(gridPos, _color);
            }
        }
    }

    private void AI()
    {
        gm.Tree = new Data.Tree(gm.Board);
        int value = gm.Board.Alphabeta(gm.Tree.Root, 3, 3, int.MinValue, int.MaxValue, false);

        // List<Data.Node> children = new List<Data.Node>();

        foreach (Data.Node child in gm.Tree.Root.children)
        {
            if (child.value == value)
            {
                //children.Add(child);
                selectChessPieceData = gm.Board.GetChessPiece(child.selected.x, child.selected.y);
                Data.ChessPiece ChessPieceData = gm.Board.GetChessPiece(child.newPos.x, child.newPos.y);

                if (ChessPieceData != null)
                {
                    Destroy(ChessPieceData.GameObject2D);
                    Destroy(ChessPieceData.GameObject3D);
                    Data.blackPieces--;
                }
                gm.Board.SetId(child.selected.x, child.selected.y, null);
                gm.Board.SetId(child.newPos.x, child.newPos.y, selectChessPieceData);

                selectChessPieceData.GameObject2D.GetComponent<ChessPiece>().target = Find2DWorldPosition(child.newPos.x, child.newPos.y);
                selectChessPieceData.GameObject3D.GetComponent<ChessPiece>().target = Find3DWorldPosition(child.newPos.x, child.newPos.y);
                Promotion(child.newPos, Data.Color.WHITE);
                break;
            }
        }

        /* int rand = Random.Range(0, children.Count);
         selectChessPieceData = gm.Board.GetChessPiece(children[rand].selected.x, children[rand].selected.y);
         Data.ChessPiece ChessPieceData = gm.Board.GetChessPiece(children[rand].newPos.x, children[rand].newPos.y);

         if (ChessPieceData != null)
         {
             Destroy(ChessPieceData.GameObject2D);
             Destroy(ChessPieceData.GameObject3D);
         }
         gm.Board.SetId(children[rand].selected.x, children[rand].selected.y, null);
         gm.Board.SetId(children[rand].newPos.x, children[rand].newPos.y, selectChessPieceData);

         selectChessPieceData.GameObject2D.GetComponent<ChessPiece>().target = Find2DWorldPosition(children[rand].newPos.x, children[rand].newPos.y);
         selectChessPieceData.GameObject3D.GetComponent<ChessPiece>().target = Find3DWorldPosition(children[rand].newPos.x, children[rand].newPos.y);

         Promotion(children[rand].newPos);*/
        playerTurn = false;
        selectChessPieceData = null;
    }

    private void Promotion(Vector2Int _pos, Data.Color _color)
    {
        if ((_pos.x == 0 || _pos.x == 7) && selectChessPieceData.Type == Data.Piece.PAWN)
        {
            selectChessPieceData.Type = Data.Piece.QUEEN;
            gm.Board.SetId(_pos.x, _pos.y, selectChessPieceData);
            int offset = (_color == Data.Color.BLACK) ? 0 : 6;
            int index = (int)selectChessPieceData.Type - 1 + offset;

            selectChessPieceData.GameObject2D.GetComponent<SpriteRenderer>().sprite = sprites[index];

            for (int i = 0; i < selectChessPieceData.GameObject3D.transform.childCount; i++)
            {
                Destroy(selectChessPieceData.GameObject3D.transform.GetChild(i).gameObject);
            }

            GameObject GB = prefabs[index];

            for (int j = 0; j < GB.transform.childCount; ++j)
            {
                GameObject child = Instantiate(GB.transform.GetChild(j).gameObject);
                child.transform.parent = selectChessPieceData.GameObject3D.transform;
                child.transform.position = selectChessPieceData.GameObject3D.transform.position + child.transform.position;
            }
        }
    }

    public void ChangeMode()
    {
        camera2D.gameObject.SetActive(!is3D);
        is3D = !is3D;
    }

    public void StartGame(bool _ai)
    {
        gameStarted = true;
        ai = _ai;
        btns[0].gameObject.SetActive(false);
        btns[1].gameObject.SetActive(false);
        btns[2].gameObject.SetActive(true);
        btns[3].gameObject.SetActive(true);
        btns[4].gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Clear();
        gm = new Data.GameManager();
        gm.Tree = new Data.Tree(gm.Board);

        gameStarted = false;
        ai = false;
        playerTurn = false;
        mouvement = false;
        is3D = false;
        gameStarted = false;
        ai = true;

        selectChessPieceData = null;

        btns[0].gameObject.SetActive(true);
        btns[1].gameObject.SetActive(true);
        btns[2].gameObject.SetActive(false);
        btns[3].gameObject.SetActive(false);
        btns[4].gameObject.SetActive(true);

        
        Initialize2DGameObjects();
        Initialize3DGameObjects();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
