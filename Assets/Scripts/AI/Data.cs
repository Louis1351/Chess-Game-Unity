using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public static int whitePieces = 16;
    public static int blackPieces = 16;

    public static int[,] improvePawn
        = new int[,] {
            { 0,  0,  0,  0,  0,  0,  0,  0 },
            { 50, 50, 50, 50, 50, 50, 50, 50 },
            { 10, 10, 20, 30, 30, 20, 10, 10 },
            { 5,  5, 10, 25, 25, 10,  5,  5 },
            { 0,  0,  0, 20, 20,  0,  0,  0 },
            { 5, -5,-10,  0,  0,-10, -5,  5 },
            { 5, 10, 10,-20,-20, 10, 10,  5 },
            { 0,  0,  0,  0,  0,  0,  0,  0 }
        };

    public static int[,] improveKnight
        = new int[,] {
            { -50,-40,-30,-30,-30,-30,-40,-50 },
            { -40,-20,  0,  0,  0,  0,-20,-40 },
            { -30,  0, 10, 15, 15, 10,  0,-30 },
            { -30,  5, 15, 20, 20, 15,  5,-30 },
            { -30,  0, 15, 20, 20, 15,  0,-30 },
            { -30,  5, 10, 15, 15, 10,  5,-30 },
            { -40,-20,  0,  5,  5,  0,-20,-40 },
            { -50,-40,-30,-30,-30,-30,-40,-50 }
        };

    public static int[,] improveBishop
        = new int[,] {
            { -20,-10,-10,-10,-10,-10,-10,-20 },
            { -10,  0,  0,  0,  0,  0,  0,-10 },
            { -10,  0,  5, 10, 10,  5,  0,-10 },
            { -10,  5,  5, 10, 10,  5,  5,-10 },
            { -10,  0, 10, 10, 10, 10,  0,-10 },
            { -10, 10, 10, 10, 10, 10, 10,-10 },
            { -10,  5,  0,  0,  0,  0,  5,-10 },
            { -20,-10,-10,-10,-10,-10,-10,-20 }
        };

    public static int[,] improveRook
        = new int[,] {
            { 0,  0,  0,  0,  0,  0,  0,  0 },
            { 5, 10, 10, 10, 10, 10, 10,  5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { 0,  0,  0,  5,  5,  0,  0,  0 }
        };

    public static int[,] improveQueen
        = new int[,] {
            { -20,-10,-10, -5, -5,-10,-10,-20 },
            { -10,  0,  0,  0,  0,  0,  0,-10 },
            { -10,  0,  5,  5,  5,  5,  0,-10 },
            { -5,  0,  5,  5,  5,  5,  0, -5 },
            { -5,  0,  5,  5,  5,  5,  0, -5 },
            { -10,  0,  5,  5,  5,  5,  0,-10 },
            { -10,  0,  0,  0,  0,  0,  0,-10 },
            { -20,-10,-10, -5, -5,-10,-10,-20 }
        };

    public static int[,] improveKing
         = new int[,] {
            { -30,-40,-40,-50,-50,-40,-40,-30 },
            { -30,-40,-40,-50,-50,-40,-40,-30 },
            { -30,-40,-40,-50,-50,-40,-40,-30 },
            { -30,-40,-40,-50,-50,-40,-40,-30 },
            { -20,-30,-30,-40,-40,-30,-30,-20 },
            { -10,-20,-20,-20,-20,-20,-20,-10 },
            { 20, 20,  0,  0,  0,  0, 20, 20 },
            { 20, 30, 10,  0,  0, 10, 30, 20 }
        };

    public enum Piece
    {
        PAWN = 1,
        KNIGHT,
        BISHOP,
        ROOK,
        QUEEN,
        KING
    }

    public enum Color
    {
        BLACK,
        WHITE
    }

    public enum State
    {
        WIN,
        LOOSE,
        DRAW
    }

    public class ChessPiece
    {
        private Piece type;
        private Color color;
        private GameObject gameObject2D;
        private GameObject gameObject3D;

        #region assessors
        public Piece Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public GameObject GameObject2D
        {
            get
            {
                return gameObject2D;
            }

            set
            {
                gameObject2D = value;
            }
        }

        public GameObject GameObject3D
        {
            get
            {
                return gameObject3D;
            }

            set
            {
                gameObject3D = value;
            }
        }
        #endregion

        public ChessPiece(Piece _type, Color _color)
        {
            type = _type;
            color = _color;
        }


    }

    public class Grid
    {
        private int rows;
        private int columns;
        private ChessPiece[,] grid;
        private List<Vector2Int> gridPositions;


        public Grid(int _rows, int _columns)
        {
            grid = new ChessPiece[_rows, _columns];
            gridPositions = new List<Vector2Int>();
        }

        public Grid(Grid _grid)
        {
            grid = (ChessPiece[,])_grid.grid.Clone();
            gridPositions = new List<Vector2Int>();
        }

        public void SetId(int _row, int _column, ChessPiece _piece)
        {
            grid[_row, _column] = _piece;
        }

        public ChessPiece GetChessPiece(int _row, int _column)
        {
            if (_row >= 0 && _row < 8 &&
               _column >= 0 && _column < 8)
            {
                return grid[_row, _column];
            }

            return null;
        }

        public Vector2Int GetId(ChessPiece _chPiece)
        {
            Vector2Int id = Vector2Int.zero;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_chPiece == grid[i, j])
                    {
                        id.x = i;
                        id.y = j;
                    }
                }
            }

            return id;
        }

        public bool KingIsFocus(Color _color)
        {
            bool isFocus = false;
            Vector2Int kingPos = new Vector2Int(-1, -1);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (grid[i, j] != null && grid[i, j].Color == _color && grid[i, j].Type == Piece.KING)
                    {
                        kingPos.x = i;
                        kingPos.y = j;
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (grid[i, j] != null && grid[i, j].Color != _color)
                    {
                        if (FindNextPositions(i, j).Contains(kingPos))
                        {
                            isFocus = true;
                            break;
                        }
                    }
                }
            }

            return isFocus;
        }

        private void AddPossibleCase(int _row, int _column, Color _color)
        {
            if (_row >= 0 && _row < 8 &&
                _column >= 0 && _column < 8)
            {
                ChessPiece chPiece = grid[_row, _column];
                if (chPiece == null)
                    gridPositions.Add(new Vector2Int(_row, _column));
                else
                {
                    if (chPiece.Color != _color)
                    {
                        gridPositions.Add(new Vector2Int(_row, _column));
                    }
                }
            }
        }

        public List<Vector2Int> FindNextPositions(int _row, int _column)
        {
            if (_row >= 0 && _row < 8 &&
                _column >= 0 && _column < 8)
            {
                ChessPiece chPiece = grid[_row, _column];
                gridPositions.Clear();
                if (chPiece != null)
                {
                    int i = 0;
                    bool horizontal1 = true;
                    bool horizontal2 = true;
                    bool horizontal3 = true;
                    bool horizontal4 = true;
                    bool diagonale1 = true;
                    bool diagonale2 = true;
                    bool diagonale3 = true;
                    bool diagonale4 = true;
                    ChessPiece tmpchPiece;

                    switch (chPiece.Type)
                    {
                        case Piece.PAWN:
                            i = (chPiece.Color == Color.BLACK) ? 1 : -1;

                            tmpchPiece = GetChessPiece(_row + 1 * i, _column + 1);
                            if (tmpchPiece != null)
                                AddPossibleCase(_row + 1 * i, _column + 1, chPiece.Color);
                            tmpchPiece = GetChessPiece(_row + 1 * i, _column - 1);
                            if (tmpchPiece != null)
                                AddPossibleCase(_row + 1 * i, _column - 1, chPiece.Color);

                            tmpchPiece = GetChessPiece(_row + 1 * i, _column);
                            if (tmpchPiece == null)
                            {
                                AddPossibleCase(_row + 1 * i, _column, chPiece.Color);
                                if ((_row == 1 && i > 0) || (_row == 6 && i < 0))
                                {
                                    tmpchPiece = GetChessPiece(_row + 2 * i, _column);
                                    if (tmpchPiece == null)
                                        AddPossibleCase(_row + 2 * i, _column, chPiece.Color);
                                }
                            }


                            break;
                        case Piece.ROOK:
                            for (i = 1; i < 8; i++)
                            {
                                if (horizontal1)
                                    AddPossibleCase(_row + i, _column, chPiece.Color);
                                if (horizontal2)
                                    AddPossibleCase(_row, _column + i, chPiece.Color);
                                if (horizontal3)
                                    AddPossibleCase(_row - i, _column, chPiece.Color);
                                if (horizontal4)
                                    AddPossibleCase(_row, _column - i, chPiece.Color);

                                tmpchPiece = GetChessPiece(_row + i, _column);
                                horizontal1 = (tmpchPiece == null) ? horizontal1 : false;

                                tmpchPiece = GetChessPiece(_row, _column + i);
                                horizontal2 = (tmpchPiece == null) ? horizontal2 : false;

                                tmpchPiece = GetChessPiece(_row - i, _column);
                                horizontal3 = (tmpchPiece == null) ? horizontal3 : false;

                                tmpchPiece = GetChessPiece(_row, _column - i);
                                horizontal4 = (tmpchPiece == null) ? horizontal4 : false;
                            }
                            break;
                        case Piece.KNIGHT:

                            AddPossibleCase(_row + 2, _column + 1, chPiece.Color);
                            AddPossibleCase(_row + 2, _column - 1, chPiece.Color);

                            AddPossibleCase(_row - 2, _column + 1, chPiece.Color);
                            AddPossibleCase(_row - 2, _column - 1, chPiece.Color);

                            AddPossibleCase(_row + 1, _column + 2, chPiece.Color);
                            AddPossibleCase(_row - 1, _column + 2, chPiece.Color);

                            AddPossibleCase(_row + 1, _column - 2, chPiece.Color);
                            AddPossibleCase(_row - 1, _column - 2, chPiece.Color);
                            break;
                        case Piece.BISHOP:
                            for (i = 1; i < 8; i++)
                            {

                                if (diagonale1)
                                    AddPossibleCase(_row + i, _column + i, chPiece.Color);
                                if (diagonale2)
                                    AddPossibleCase(_row - i, _column - i, chPiece.Color);
                                if (diagonale3)
                                    AddPossibleCase(_row - i, _column + i, chPiece.Color);
                                if (diagonale4)
                                    AddPossibleCase(_row + i, _column - i, chPiece.Color);

                                tmpchPiece = GetChessPiece(_row + i, _column + i);
                                diagonale1 = (tmpchPiece == null) ? diagonale1 : false;

                                tmpchPiece = GetChessPiece(_row - i, _column - i);
                                diagonale2 = (tmpchPiece == null) ? diagonale2 : false;

                                tmpchPiece = GetChessPiece(_row - i, _column + i);
                                diagonale3 = (tmpchPiece == null) ? diagonale3 : false;

                                tmpchPiece = GetChessPiece(_row + i, _column - i);
                                diagonale4 = (tmpchPiece == null) ? diagonale4 : false;
                            }
                            break;
                        case Piece.QUEEN:
                            for (i = 1; i < 8; i++)
                            {

                                if (diagonale1)
                                    AddPossibleCase(_row + i, _column + i, chPiece.Color);
                                if (diagonale2)
                                    AddPossibleCase(_row - i, _column - i, chPiece.Color);
                                if (diagonale3)
                                    AddPossibleCase(_row - i, _column + i, chPiece.Color);
                                if (diagonale4)
                                    AddPossibleCase(_row + i, _column - i, chPiece.Color);
                                if (horizontal1)
                                    AddPossibleCase(_row + i, _column, chPiece.Color);
                                if (horizontal2)
                                    AddPossibleCase(_row, _column + i, chPiece.Color);
                                if (horizontal3)
                                    AddPossibleCase(_row - i, _column, chPiece.Color);
                                if (horizontal4)
                                    AddPossibleCase(_row, _column - i, chPiece.Color);

                                tmpchPiece = GetChessPiece(_row + i, _column + i);
                                diagonale1 = (tmpchPiece == null) ? diagonale1 : false;

                                tmpchPiece = GetChessPiece(_row - i, _column - i);
                                diagonale2 = (tmpchPiece == null) ? diagonale2 : false;

                                tmpchPiece = GetChessPiece(_row - i, _column + i);
                                diagonale3 = (tmpchPiece == null) ? diagonale3 : false;

                                tmpchPiece = GetChessPiece(_row + i, _column - i);
                                diagonale4 = (tmpchPiece == null) ? diagonale4 : false;

                                tmpchPiece = GetChessPiece(_row + i, _column);
                                horizontal1 = (tmpchPiece == null) ? horizontal1 : false;

                                tmpchPiece = GetChessPiece(_row, _column + i);
                                horizontal2 = (tmpchPiece == null) ? horizontal2 : false;

                                tmpchPiece = GetChessPiece(_row - i, _column);
                                horizontal3 = (tmpchPiece == null) ? horizontal3 : false;

                                tmpchPiece = GetChessPiece(_row, _column - i);
                                horizontal4 = (tmpchPiece == null) ? horizontal4 : false;
                            }
                            break;
                        case Piece.KING:
                            AddPossibleCase(_row + 1, _column + 1, chPiece.Color);
                            AddPossibleCase(_row - 1, _column - 1, chPiece.Color);
                            AddPossibleCase(_row - 1, _column + 1, chPiece.Color);
                            AddPossibleCase(_row + 1, _column - 1, chPiece.Color);
                            AddPossibleCase(_row + 1, _column, chPiece.Color);
                            AddPossibleCase(_row, _column + 1, chPiece.Color);
                            AddPossibleCase(_row - 1, _column, chPiece.Color);
                            AddPossibleCase(_row, _column - 1, chPiece.Color);
                            break;
                    }
                }
            }
            return gridPositions;
        }

        public int EvaluateGame()
        {
            int whiteScore = 0;
            int blackScore = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ChessPiece chessPiece = grid[i, j];
                    if (chessPiece != null)
                    {
                        if (chessPiece.Color == Color.WHITE)
                        {
                            /* whiteScore += (int)chessPiece.Type;
                             if (chessPiece.Type == Data.Piece.KNIGHT)
                                 whiteScore++;*/

                            switch (chessPiece.Type)
                            {
                                case Data.Piece.PAWN:
                                    whiteScore += 100 + Data.improvePawn[i, j];
                                    break;
                                case Data.Piece.KNIGHT:
                                    whiteScore += 350 + Data.improveKnight[i, j];
                                    break;
                                case Data.Piece.BISHOP:
                                    whiteScore += 350 + Data.improveBishop[i, j];
                                    break;
                                case Data.Piece.ROOK:
                                    whiteScore += 525 + Data.improveRook[i, j];
                                    break;
                                case Data.Piece.QUEEN:
                                    whiteScore += 1000 + Data.improveQueen[i, j];
                                    break;
                                case Data.Piece.KING:
                                    whiteScore += 10000 + Data.improveKing[i, j];
                                    break;
                            }

                        }
                        else
                        {
                            /*blackScore += (int)chessPiece.Type;
                            if (chessPiece.Type == Data.Piece.KNIGHT)
                                blackScore++;*/
                            switch (chessPiece.Type)
                            {
                                case Data.Piece.PAWN:
                                    blackScore += 100 + Data.improvePawn[7 - i, j];
                                    break;
                                case Data.Piece.KNIGHT:
                                    blackScore += 350 + Data.improveKnight[7 - i, j];
                                    break;
                                case Data.Piece.BISHOP:
                                    blackScore += 350 + Data.improveBishop[7 - i, j];
                                    break;
                                case Data.Piece.ROOK:
                                    blackScore += 525 + Data.improveRook[7 - i, j];
                                    break;
                                case Data.Piece.QUEEN:
                                    blackScore += 1000 + Data.improveQueen[7 - i, j];
                                    break;
                                case Data.Piece.KING:
                                    blackScore += 10000 + Data.improveKing[7 - i, j];
                                    break;
                            }
                        }
                    }
                }
            }
            return blackScore - whiteScore;
        }

        public int Alphabeta(Node _node, int _depth, int _maxDepth, int _alpha, int _beta, bool isMaximisingPlayer)
        {
            _node.GenerateChildren(isMaximisingPlayer ? Color.BLACK : Color.WHITE, _depth == _maxDepth);

            if (_depth == 0 || _node.children.Count == 0)
            {
                _node.value = _node.Grid.EvaluateGame();
                return _node.value;
            }

            if (isMaximisingPlayer)
            {
                int value = int.MinValue;
                foreach (Node child in _node.children)
                {
                    value = Mathf.Max(value, Alphabeta(child, _depth - 1, _maxDepth, _alpha, _beta, !isMaximisingPlayer));
                    _alpha = Mathf.Max(_alpha, value);
                    if (_alpha >= _beta)
                    {
                        break;
                    }
                }
                _node.value = value;
                return _node.value;
            }
            else
            {
                int value = int.MaxValue;
                foreach (Node child in _node.children)
                {
                    value = Mathf.Min(value, Alphabeta(child, _depth - 1, _maxDepth, _alpha, _beta, !isMaximisingPlayer));
                    _beta = Mathf.Min(_beta, value);
                    if (_alpha >= _beta)
                    {
                        break;
                    }
                }
                _node.value = value;
                return _node.value;
            }
        }

        /* public int MiniMax(Node _node, int _depth, bool isMaximisingPlayer)
         {
             Color col = isMaximisingPlayer ? Color.BLACK : Color.WHITE;

             _node.GenerateChildren(col);

             if (_depth == 0 || _node.children.Count == 0)
             {
                 return _node.value;
             }


             if (isMaximisingPlayer)
             {
                 int value = int.MinValue;
                 foreach (Node child in _node.children)
                 {
                     value = Mathf.Max(value, MiniMax(child, _depth - 1, !isMaximisingPlayer));
                 }
                 _node.value = value;
                 return _node.value;
             }
             else
             {
                 int value = int.MaxValue;
                 foreach (Node child in _node.children)
                 {
                     value = Mathf.Min(value, MiniMax(child, _depth - 1, !isMaximisingPlayer));
                 }
                 _node.value = value;
                 return _node.value;
             }
         }*/

    }

    public class Node
    {
        private Grid grid;
        public int value;
        public List<Node> children;
        public Vector2Int selected;
        public Vector2Int newPos;
        #region assessors

        public Grid Grid
        {
            get
            {
                return grid;
            }

            set
            {
                grid = value;
            }
        }
        #endregion

        public Node()
        {
            children = new List<Node>();
        }

        public void GenerateChildren(Color _color, bool _kingTest)
        {
            //int count = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ChessPiece ch = grid.GetChessPiece(i, j);

                    if (ch != null)
                    {
                        if (_color == ch.Color)
                        {
                            List<Vector2Int> nextPositions = grid.FindNextPositions(i, j);

                            for (int k = nextPositions.Count - 1; k >= 0; k--)
                            {
                                if (_kingTest)
                                {
                                    Data.Grid g = new Data.Grid(grid);
                                    g.SetId(i, j, null);
                                    g.SetId(nextPositions[k].x, nextPositions[k].y, ch);
                                    bool kingIsFocus = g.KingIsFocus(_color);

                                    if (kingIsFocus)
                                    {
                                        nextPositions.RemoveAt(k);
                                    }
                                    else
                                    {
                                        Node child = new Node();
                                        child.selected.x = i;
                                        child.selected.y = j;
                                        child.newPos.x = nextPositions[k].x;
                                        child.newPos.y = nextPositions[k].y;
                                        child.grid = new Grid(grid);
                                        child.grid.SetId(child.selected.x, child.selected.y, null);
                                        child.grid.SetId(child.newPos.x, child.newPos.y, ch);


                                        children.Add(child);
                                    }
                                }
                                else
                                {
                                    Node child = new Node();
                                    child.selected.x = i;
                                    child.selected.y = j;
                                    child.newPos.x = nextPositions[k].x;
                                    child.newPos.y = nextPositions[k].y;
                                    child.grid = new Grid(grid);
                                    child.grid.SetId(child.selected.x, child.selected.y, null);
                                    child.grid.SetId(child.newPos.x, child.newPos.y, ch);


                                    children.Add(child);
                                }
                            }
                            /*foreach (Vector2Int newPos in nextPositions)
                            {
                                Node child = new Node();
                                child.selected.x = i;
                                child.selected.y = j;
                                child.newPos.x = newPos.x;
                                child.newPos.y = newPos.y;
                                child.grid = new Grid(grid);
                                child.grid.SetId(child.selected.x, child.selected.y, null);
                                child.grid.SetId(child.newPos.x, child.newPos.y, ch);


                                children.Add(child);
                            }*/
                            /*count++;
                            if (_color == Color.WHITE && count >= whitePieces) break;
                            if (_color == Color.BLACK && count >= blackPieces) break;*/

                        }
                    }
                }
            }
        }
    }

    public class Tree
    {
        private Node root;

        #region assessors
        public Node Root
        {
            get
            {
                return root;
            }

            set
            {
                root = value;
            }
        }
        #endregion

        public Tree(Grid _grid)
        {
            root = new Node();
            root.Grid = _grid;
        }
    }

    public class GameManager
    {
        private Grid board;
        private Tree tree;

        #region assessors
        public Grid Board
        {
            get
            {
                return board;
            }

            set
            {
                board = value;
            }
        }

        public Tree Tree
        {
            get
            {
                return tree;
            }

            set
            {
                tree = value;
            }
        }
        #endregion

        public GameManager()
        {
            InitializeGameData();
        }

        public void InitializeGameData()
        {
            whitePieces = 16;
            blackPieces = 16;
            board = new Grid(8, 8);
            SetPieces(Color.BLACK, 0, 1);
            SetPieces(Color.WHITE, 7, 6);
        }

        public void SetPieces(Color _color, int _row1, int _row2)
        {
            board.SetId(_row1, 0, new ChessPiece(Piece.ROOK, _color));
            board.SetId(_row1, 1, new ChessPiece(Piece.KNIGHT, _color));
            board.SetId(_row1, 2, new ChessPiece(Piece.BISHOP, _color));
            board.SetId(_row1, 3, new ChessPiece(Piece.KING, _color));
            board.SetId(_row1, 4, new ChessPiece(Piece.QUEEN, _color));
            board.SetId(_row1, 5, new ChessPiece(Piece.BISHOP, _color));
            board.SetId(_row1, 6, new ChessPiece(Piece.KNIGHT, _color));
            board.SetId(_row1, 7, new ChessPiece(Piece.ROOK, _color));

            for (int i = 0; i < 8; i++)
            {
                board.SetId(_row2, i, new ChessPiece(Piece.PAWN, _color));
            }
        }
    }
}




