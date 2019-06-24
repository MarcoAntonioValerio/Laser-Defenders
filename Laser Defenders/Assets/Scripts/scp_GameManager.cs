using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scp_GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;    
    [SerializeField] public float totalScore;
    

    // Start is called before the first frame update
    void Start()
    {
        SetupInitialValues();
    }    

    // Update is called once per frame
    void Update()
    {
        NewScore();
    }

    private void SetupInitialValues()
    {        
        scoreText.text = ("Total Score: " + totalScore);        
    }

    private void NewScore()
    {        
        scoreText.text = ("Total Score: " + totalScore);
    }
}
