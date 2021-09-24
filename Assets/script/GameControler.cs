using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    public GameObject Player;
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Helicopter;

    public GameObject pnStart, pnGameOver, pnPlay, pnWinner, pnLoser, pnAbout, pnPause;
    public int countPoints;
    private int totalPoints = 10;
    public GameObject PlayerOriginalPosition;
    public List<GameObject> listPoints;
    public List<GameObject> listOriginalPosition;
    public GameObject pointObj;
    public bool gameOver, starGame, playing, pause;
    public float timeRemainingInitial = 2; 
    private float timeRemaining; 
    public Text timeText, countPointsText, timeTextGameOver, txtMenssage;

    // Start is called before the first frame update
    void Start()
    {
        starGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(starGame){
            whileStart();
        }else{
            if(playing){
                whilePlay();
            }else{
                if(gameOver){
                    whileGameOver();
                }
            }
        }
    }

    private void whilePlay(){  
        //Check game over
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
            ShowPointsCount();
            AudioSource audioSource;
            if(countPoints == totalPoints){
                gameOver = true;
                playing = false;
                audioSource = Camera2.gameObject.GetComponent<AudioSource>();
                audioSource.mute = true;
            }
            if(timeRemaining <= 115 && timeRemaining > 114){
                //Debug.Log("Helicopter muted!" + timeRemaining.ToString());
                audioSource = Helicopter.gameObject.GetComponent<AudioSource>();
                audioSource.mute = true;
            }
        }
        else
        {
            //Debug.Log("Time is up!");
            playing = false;
            gameOver = true;

            AudioSource audioSource;
            audioSource = Camera2.gameObject.GetComponent<AudioSource>();
            audioSource.mute = true;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeTextGameOver.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

   private void whileStart(){
        if(!pnAbout.activeSelf)
            pnStart.SetActive(true);

        pnGameOver.SetActive(false);
        pnPlay.SetActive(false);
        pnWinner.SetActive(false);
        pnLoser.SetActive(false);
        pnPause.SetActive(false);
    }

    private void whileGameOver(){
        pnStart.SetActive(false);
        pnGameOver.SetActive(true);
        pnPlay.SetActive(false);
        pnPause.SetActive(false);
        
        if (timeRemaining > 0)
        {
            //Winner
            pnWinner.SetActive(true);
        }else{
            //lose
            pnLoser.SetActive(true);
            timeRemaining = 0;
        }
    }

    public void newGame(){

        pnStart.SetActive(false);
        pnGameOver.SetActive(false);
        pnPlay.SetActive(true);
        pnWinner.SetActive(false);
        pnLoser.SetActive(false);
        Camera1.SetActive(false);
        Player.SetActive(true);

        AudioSource audioSource;
        audioSource = Helicopter.gameObject.GetComponent<AudioSource>();
        audioSource.mute = false;

        audioSource = Camera2.gameObject.GetComponent<AudioSource>();
        audioSource.mute = false;

        countPoints = 0;
        timeRemaining = timeRemainingInitial * 60;
        ResumeGame();
        Player.transform.position = new Vector3(PlayerOriginalPosition.transform.position.x,PlayerOriginalPosition.transform.position.y,PlayerOriginalPosition.transform.position.z);
       
        starGame = false;
        playing = true;
        gameOver = false;
        pause = false;

        //Create new Points
        InstantiatePoints();
        showMenssage("You have 2 minute to find the UBISOFT studio and deliver my resume.");

    }

    public void mainMenu(){
        starGame = true;
        playing = false;
        gameOver = false;
        pause =false;
        pnStart.SetActive(true);
        pnAbout.SetActive(false);
        pnGameOver.SetActive(false);
        pnPlay.SetActive(false);
        pnWinner.SetActive(false);
        pnLoser.SetActive(false);
        pnPause.SetActive(false);
        Camera1.SetActive(true);
        Player.SetActive(false);
    }

    public void showAbout(){
        pnStart.SetActive(false);
        pnAbout.SetActive(true);
        pnGameOver.SetActive(false);
        pnPlay.SetActive(false);
        pnWinner.SetActive(false);
        pnLoser.SetActive(false);
    }

    public void PauseGame(){
        pnPause.SetActive(true);
        pause = true;
        Time.timeScale = 0;
        AudioSource audioSource;
        audioSource = Helicopter.gameObject.GetComponent<AudioSource>();
        audioSource.mute = true;
    }

    public void ResumeGame(){
        pnPause.SetActive(false);
        pause = false;
        Time.timeScale = 1;
    }

    public void openURL(string url){
        Application.OpenURL(url);
    }

    public void InstantiatePoints(){
        foreach(GameObject pointOld in listPoints){
            Destroy(pointOld);
        }

        for(int index = 0; index < totalPoints; index ++){
            //variablePosition = listOriginalPosition[index].transform.position;
            GameObject newPoint = Instantiate(pointObj, listOriginalPosition[index].transform.position, listOriginalPosition[index].transform.rotation);
            newPoint.SetActive(true);
            listPoints.Add(newPoint);
        }
    }

    public void ShowPointsCount(){
        countPointsText.text = countPoints.ToString() + "/" + totalPoints.ToString();
    }

    public void addPointToCount(){
        countPoints ++;
    }

    public void showMenssage(string menssage){
        //txtMenssage.text = menssage;
        Text newMsg = Instantiate(txtMenssage, txtMenssage.transform.position, txtMenssage.transform.rotation);
        newMsg.text = menssage;
        newMsg.transform.parent = pnPlay.gameObject.transform;
        newMsg.transform.localScale = new Vector3(1,1,1);
        Destroy(newMsg, 3);
    }
}
