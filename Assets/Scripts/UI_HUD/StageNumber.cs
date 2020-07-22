using UnityEngine;
using TMPro;

public class StageNumber : MonoBehaviour 
{
    public TextMeshProUGUI countText;

    void Update() 
    {
        int stage = GameManager.Instance.stage + 1;
        countText.text = "Stage " + stage.ToString();
    }
}
