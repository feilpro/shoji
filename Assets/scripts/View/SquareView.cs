using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SquareView : MonoBehaviour
{
    TextMeshProUGUI text;
    [Header("Components")]
    [SerializeField] Image image;
    [HideInInspector] public View view;
    Button buttonComponent;

    [Header("Sprites")]
    [SerializeField] Sprite pawnSprite;
    [SerializeField] Sprite spearSprite;
    [SerializeField] Sprite horseSprite;
    [SerializeField] Sprite bishopSprite;
    [SerializeField] Sprite towerSprite;
    [SerializeField] Sprite silverSprite;
    [SerializeField] Sprite goldenSprite;
    [SerializeField] Sprite BlackKingSprite;
    [SerializeField] Sprite WhiteKingSprite;

    int2 gridPos;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        buttonComponent = GetComponentInChildren<Button>();
        //image = GetComponentInChildren<Image>();
        image.enabled = false;
    }

    private void OnEnable()
    {
        buttonComponent.onClick.AddListener(OnSelectSquare);

    }

    private void OnDisable()
    {
        buttonComponent.onClick.RemoveListener(OnSelectSquare);
    }

    public void SetSquare(int x, int y, View view)
    {
        this.view = view;
        gridPos = new int2(x, y);
        text.text = $"{x},{y}";
    }

    public void AddPiece(ref Piece piece)
    {
        text.enabled = false;
        image.enabled = true;

        image.sprite = piece.type switch
        {
            PieceType.Pawn => pawnSprite,
            PieceType.Spear => spearSprite,
            PieceType.Horse => horseSprite,
            PieceType.Silver => silverSprite,
            PieceType.Golden => goldenSprite,
            PieceType.Tower => towerSprite,
            PieceType.Bishop => bishopSprite,
            PieceType.King => piece.team == Team.White ? WhiteKingSprite : BlackKingSprite,
            _ => null
        };
        image.gameObject.transform.rotation = piece.team switch {
            Team.White => Quaternion.Euler(0, 0, 0),
            Team.Black => Quaternion.Euler(0, 0, 180),
            _ => Quaternion.identity

        };
    }

    public void RemovePiece()
    {
        text.enabled = true;
        image.enabled = false;
    }

    void OnSelectSquare()
    {
        view?.SelectSquare(gridPos);
    }
}
