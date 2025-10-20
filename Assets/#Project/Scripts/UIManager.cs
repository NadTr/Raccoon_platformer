using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int counter = 0;
    [SerializeField] private TMP_Text score;

    void Start()
    {
        counter = 0;
        score.SetText($"Chestnuts : {counter}");

    }

    public void IncreaseCounter()
    {
        counter++;
        // Debug.Log($"increase counter = {counter}");
        score.SetText($"Chestnuts : {counter}");
    }
}
