using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class View : MonoBehaviour
{
    [Header("prefabs")]
    [SerializeField] GameObject boxPrefab;

    [Header("view Objects")]
    [SerializeField] Transform gridParent;

    Controller controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controller = new Controller(this);
    }

    // Update is called once per frame
    public void CreateGrid(ref Board board, int rows, int cols)
    {
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                GameObject newSquare = Instantiate(boxPrefab, gridParent);
                int2 coor = board.GetSquare(i, j).GetCoor;
                newSquare.GetComponentInChildren<TextMeshProUGUI>().text = $"{coor.x},{coor.y}";
            }
        }
    }
}
