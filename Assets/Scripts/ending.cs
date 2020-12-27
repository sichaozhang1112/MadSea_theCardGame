using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ending : MonoBehaviour
{
    [SerializeField] Text score;
    [SerializeField] Text txtDiff;
    // Start is called before the first frame update
    void Start()
    {
        score.text = PlayerPrefs.GetInt("score").ToString();
        switch (PlayerPrefs.GetInt("Difficult"))
        {
            case 0:
                txtDiff.text = "难度：龟龟";
                break;
            case 1:
                txtDiff.text = "难度：正常难度";
                break;
            case 2:
                txtDiff.text = "难度：所有人上甲板";
                break;
            case 3:
                txtDiff.text = "难度：大海你全是水";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
