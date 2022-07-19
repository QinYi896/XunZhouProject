using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public  Text scoreText;
    [Range(0,2)]
    public float scoreAmiTime;
    private void Start()
    {
      //  scoreText = transform .GetComponent<Text>();


    }
   
    public void ScoreShowUI(int score)
    {
       switch(score)
        {
            case 1:
                
                break;
            case 2:
                // scoreText.color = new Color();
                scoreText.color = Color.green;
                break;
            case 3:
                // scoreText.color = new Color();
                scoreText.color = Color.red;
                break;

        }


        Debug.Log(score+"yyyyyyyyyyyyy");
        scoreText.text ="+"+ score.ToString();
        transform.DOScale(0.1f, scoreAmiTime);
        scoreText.DOFade(0, scoreAmiTime);
        transform.DOLocalMoveY(500, scoreAmiTime);
        Invoke("Destroy", scoreAmiTime);
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
