using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SquareView : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField]Image image;

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
    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        //image = GetComponentInChildren<Image>();
        image.enabled = false;
    }

    public void SetSquare(int x, int y)
    {
        //squareCoorText = $"{x},{y}";
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
            _=>null
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
}
