using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CementaryCell : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;

    View view;
    Button buttonComponent;

    PieceType pieceType;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
    }

    private void OnEnable()
    {
        buttonComponent.onClick.AddListener(SelectedCementeryCell);
    }

    private void OnDisable()
    {
        buttonComponent.onClick.RemoveListener(SelectedCementeryCell );
    }

    public void SetCementaryView(View view, PieceType pieceType)
    {
        this.view = view;
        this.pieceType = pieceType;
    }

    public void EnableButton(bool enabled = true)
    {
        buttonComponent.enabled = enabled;
    }


    public void Start()
    {
        countText.text = "0";
    }
    public void UpdateCountText(int count)
    {
        countText.text = count.ToString();
    }

    private void SelectedCementeryCell()
    {
        view?.SelectCementaryPiece(pieceType);
    }
}
