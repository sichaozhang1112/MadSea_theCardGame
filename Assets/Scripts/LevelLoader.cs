using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Text txtDiff;
    [SerializeField] Text txtDetail;
    public void LoadScene(string levelName)
    {
        if(levelName == "main")
        {
            if(PlayerPrefs.GetInt("Difficult") == 5)
            {
                SceneManager.LoadScene("end_brave");
                return;
            }
        }
        SceneManager.LoadScene(levelName);
    }

    public void SetDifficulty(int level)
    {
        switch (level)
        {
            case 0:
                PlayerPrefs.SetInt("FieldSize", 5);
                PlayerPrefs.SetInt("Life", 25);
                PlayerPrefs.SetInt("Difficult", 0);
                txtDiff.text = "难度：龟龟";
                break;
            case 1:
                PlayerPrefs.SetInt("FieldSize", 6);
                PlayerPrefs.SetInt("Life", 20);
                PlayerPrefs.SetInt("Difficult", 1);
                txtDiff.text = "难度：正常难度";
                break;
            case 2:
                PlayerPrefs.SetInt("FieldSize", 7);
                PlayerPrefs.SetInt("Life", 15);
                PlayerPrefs.SetInt("Difficult", 2);
                txtDiff.text = "难度：所有人上甲板";
                break;
            case 3:
                PlayerPrefs.SetInt("FieldSize", 8);
                PlayerPrefs.SetInt("Life", 10);
                PlayerPrefs.SetInt("Difficult", 3);
                txtDiff.text = "难度：大海你全是水";
                break;
            case 5:
                PlayerPrefs.SetInt("FieldSize", 10);
                PlayerPrefs.SetInt("Life", 1);
                PlayerPrefs.SetInt("Difficult", 5);
                txtDiff.text = "难度：死而无憾";
                break;
            default:
                break;
        }
        txtDetail.text = "中央牌列：" + PlayerPrefs.GetInt("FieldSize").ToString() + "\n" +
                         "生命值：" + PlayerPrefs.GetInt("Life").ToString() + "\n";
        if (level >= 3)
        {
            txtDetail.text += "双倍腐化！\n";
        }
        if (level >= 5)
        {
            txtDetail.text += "五倍腐化！\n 敌人更加敏捷！\n";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!(txtDiff is null))
        {
            SetDifficulty(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
