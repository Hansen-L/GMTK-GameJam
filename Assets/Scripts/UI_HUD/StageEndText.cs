using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class StageEndText : MonoBehaviour
{
    public TextMeshProUGUI stageCompleteText;

    // Start is called before the first frame update
    void Start()
    {
        stageCompleteText.text = "STAGE " + GameManager.Instance.stage.ToString() + " COMPLETE";

        AudioManager.Instance.PlayOneShot("tada");
    }
}
