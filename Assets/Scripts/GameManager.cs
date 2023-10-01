using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;
    private int gridSize = 9;

    private void Start()
    {
        SpawnButtons();
    }

    void SpawnButtons()
    {
        for (int i = 0; i < gridSize; i++)
        {
            GameObject card = Instantiate(buttonPrefab, container);
            ButtonScript buttonScript = card.GetComponent<ButtonScript>();
            buttonScript.ButtonID = i;
        }
    }
}