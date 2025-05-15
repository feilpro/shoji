using UnityEngine.UI;
using UnityEngine;

public class CementaryView : MonoBehaviour
{
    [SerializeField] CementaryCell pawnView;
    [SerializeField] CementaryCell spearView;
    [SerializeField] CementaryCell horseView;
    [SerializeField] CementaryCell silverView;
    [SerializeField] CementaryCell goldView;
    [SerializeField] CementaryCell towerView;
    [SerializeField] CementaryCell bishopView;

    
    public void SetCementaryView(View view)
    {
        pawnView.SetCementaryView (view, PieceType.Pawn);
        spearView.SetCementaryView (view,PieceType.Spear);
        horseView.SetCementaryView (view, PieceType.Horse);
        silverView.SetCementaryView (view,PieceType.Silver);
        goldView.SetCementaryView (view, PieceType.Golden);
        towerView.SetCementaryView (view, PieceType.Tower);
        bishopView.SetCementaryView (view, PieceType.Bishop);
    }

    public void EnableCementaryView(bool enabled = true)
    {
        pawnView.EnableButton(enabled);
        spearView.EnableButton(enabled);
        horseView.EnableButton(enabled);
        silverView.EnableButton(enabled);
        goldView.EnableButton(enabled);
        towerView.EnableButton(enabled);
        bishopView.EnableButton(enabled);
    }

    public void UpdateCellView(PieceType pieceType,int count)
    {
        switch (pieceType)
        {
            case PieceType.Pawn:
                pawnView.UpdateCountText(count);
                break;
            case PieceType.Spear:
                spearView.UpdateCountText(count);
                break;
            case PieceType.Horse:
                horseView.UpdateCountText(count);
                break;
            case PieceType.Silver:
                silverView.UpdateCountText(count);
                break;
            case PieceType.Golden:
                goldView.UpdateCountText(count);
                break;
            case PieceType.Tower:
                towerView.UpdateCountText(count);
                break;
            case PieceType.Bishop:
                bishopView.UpdateCountText (count);
                break;
        }
    }
}
