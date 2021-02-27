using UnityEngine;

public class ReciPiece : MonoBehaviour
{
    // public int score; // 每片的得分

    private int _x;
    private int _y;

    public int X
    {
        get => _x;
        // set { if (IsMovable()) { _x = value; } }
        set { _x = value; }
    }

    public int Y
    {
        get => _y;
        // set { if (IsMovable()) { _y = value; } }
        set { _y = value; }
    }

    // private PieceType _type; // 每片的種類 normal bubble .. fixme 這裏似乎沒用

    // public PieceType Type => _type;

    private Recipe _recipe;

    public Recipe ReciRef => _recipe;

    // private MovablePiece _movableComponent;

    // public MovablePiece MovableComponent => _movableComponent;

    private ColorPiece _colorComponent;

    public ColorPiece ColorComponent => _colorComponent;

    private ClearablePiece _clearableComponent;

    public ClearablePiece ClearableComponent => _clearableComponent;

    private void Awake()
    {
        _colorComponent = GetComponent<ColorPiece>();
        _clearableComponent = GetComponent<ClearablePiece>();
    }

    public void Init(int x, int y, Recipe recipe)
    {
        _x = x;
        _y = y;
        _recipe = recipe;
        // _type = type;
    }

    public bool IsColored()
    {
        return _colorComponent != null;
    }

    public bool IsClearable()
    {
        return _clearableComponent != null;
    }
}
