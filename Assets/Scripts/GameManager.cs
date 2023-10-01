using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;
    private int gridSize = 9;
    private List<ButtonScript> buttonCollection = new List<ButtonScript>();
    private List<int> targetCombination = new List<int>();
    private List<int> playerCombination = new List<int>();
    private int currentCombinationSize = 1;
    public float buttonFlashDuration = 1f; 
    private float flashTimer = 0f;

    private void Start()
    {
        SpawnButtons();
        GenerateTargetCombination();
        StartCoroutine(FlashButtonsInCombination());
    }

    void SpawnButtons()
    {
        for (int i = 0; i < gridSize; i++)
        {
            GameObject card = Instantiate(buttonPrefab, container);
            ButtonScript buttonScript = card.GetComponent<ButtonScript>();
            buttonScript.ButtonID = i;
            buttonCollection.Add(buttonScript);
        }
    }

    //void GenerateTargetCombination()//random generation
    //{
    //    targetCombination.Clear();
    //    for (int i = 0; i < currentCombinationSize; i++)
    //    {
    //        int randomButtonID = Random.Range(0, gridSize);
    //        targetCombination.Add(randomButtonID);
    //    }
    //}
    void GenerateTargetCombination()
    {
        if (targetCombination.Count < currentCombinationSize)
        {
            int remainingElements = currentCombinationSize - targetCombination.Count;
            for (int i = 0; i < remainingElements; i++)
            {
                int randomButtonID = Random.Range(0, gridSize);
                targetCombination.Add(randomButtonID);
            }
        }
        else if (targetCombination.Count > currentCombinationSize)
        {
            int elementsToRemove = targetCombination.Count - currentCombinationSize;
            targetCombination.RemoveRange(targetCombination.Count - elementsToRemove, elementsToRemove);
        }
    }

    IEnumerator FlashButtonsInCombination()
    {
        while (true)
        {
            flashTimer += Time.deltaTime;
            if (flashTimer >= buttonFlashDuration)
            {
                flashTimer = 0f;
                yield return new WaitForSeconds(1f); 
                foreach (int buttonID in targetCombination)
                {
                    buttonCollection[buttonID].FlashButton();
                    yield return new WaitForSeconds(0.5f);
                    yield return new WaitForSeconds(buttonFlashDuration);
                }
                break;
            }
            yield return null;
        }
    }

    public void OnButtonClicked(int buttonID)
    {
        playerCombination.Add(buttonID);
        if (playerCombination.Count == targetCombination.Count)
        {
            bool isCorrect = true;
            for (int i = 0; i < targetCombination.Count; i++)
            {
                if (targetCombination[i] != playerCombination[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                currentCombinationSize++;
                GenerateTargetCombination();
                flashTimer = 0f; 
                StartCoroutine(FlashButtonsInCombination()); 
                Debug.Log("ты чертовски прав");
            }
            else
            {
                currentCombinationSize = Mathf.Max(1, currentCombinationSize - 1);
                GenerateTargetCombination();
                flashTimer = 0f; 
                StartCoroutine(FlashButtonsInCombination()); 
                Debug.Log("не твой день братиш");
            }

            playerCombination.Clear();
        }
    }

}
