using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
   
    [SerializeField] int maxCount = 10;
   
   
    
    
    private Rigidbody2D m_Rigibody;
    private bool m_IsStartGame;
    private int m_score;
    public float jump = 1f;
    public GameObject PipePrifab;
    public Text scoreText;
    public GameObject massage;
    public GameObject gameOver;
    private bool isGameOver = false;

    
    private void Awake()
    {
        m_score = 0;
        m_Rigibody = GetComponent<Rigidbody2D>();
        m_IsStartGame = false;
        m_Rigibody.gravityScale = 0f;
        scoreText.text = m_score.ToString();
        massage.GetComponent<SpriteRenderer>().enabled = true;
        gameOver.SetActive(false);
    }
    private void Start()
    {
       
       
       scoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            SoundController.instance.PlayThisSound( "wing", 3f);
            if (m_IsStartGame == false)
            {
                m_IsStartGame = true;
                m_Rigibody.gravityScale = 1f;
                PipePrifab.GetComponent<PipeGenerator>().enableGeneratorPipe= true;
                massage.GetComponent<SpriteRenderer>().enabled = false;
                if (m_score == 2)
                {
                    PipePrifab.GetComponent<Pipe>().speed = 3f;
                }
            }
            BirdMoveUp();
            
        }
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            ReloadScene();
            isGameOver = false; // Reset trạng thái game over
        }

    }
    private void BirdMoveUp()
    {
       
            m_Rigibody.velocity = Vector2.up * jump;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundController.instance.PlayThisSound("point", 3f);
        m_score += 1;
        scoreText.text = m_score.ToString();
        if(m_score >= PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", m_score);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        SoundController.instance.PlayThisSound("die", 3f);
   
        gameOver.SetActive(true);
        Time.timeScale = 0;
        isGameOver = true;
       

    m_score = 0;
        scoreText.text = m_score.ToString();
        if (m_score == 2)
        {
            PipePrifab.GetComponent<Pipe>().speed = 3f;
        }
       
    }
    IEnumerator Deley()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0;
        
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("_gamePlay");
    }

   
}
