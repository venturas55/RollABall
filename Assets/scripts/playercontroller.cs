using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playercontroller : MonoBehaviour
{

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI countPU;
    public TextMeshProUGUI countSU;
    public Canvas menu;
   
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public int dif_velEnemy;
    public int dif_cada;
    public int dif_timePU;
    public int dif_timeSU;
    public TextMeshProUGUI highScore;


    private float movementX;
    private float movementY;
    public GameObject Enemigo;
    private Rigidbody rb;
    private float count;
    private float timeRemainingPU;
    private float timeRemainingSU;
    private bool timerPowerUpIsRunning = false;
    private bool timerSpeedUpIsRunning = false;

    private void Awake()
    {
        //Recupero los valores guardados en Preferencias del jugador, y si no hay pongo unos valores por defecto
        dif_velEnemy = PlayerPrefs.GetInt("velocidadenemigos", 5);
        dif_timePU = PlayerPrefs.GetInt("tiempoBooster", 12);
        dif_timeSU = PlayerPrefs.GetInt("tiempoBooster", 12);
        dif_cada = PlayerPrefs.GetInt("Respawn");
    }
    void Start()
    {
        speed = 30;
    
        Time.timeScale = 1f;
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();
        this.timerPowerUpIsRunning = false;
        countPU.enabled = false;
        countSU.enabled = false;
        menu.enabled = false;

        // Set the score to zero 
        count = 0;

        SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

        //Recupero el record de PlayerPrefs
        highScore.text = "Record " + PlayerPrefs.GetInt("HighScore", 0);
     


    }

    void FixedUpdate()
    {
        // Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        if (rb.position[1] < -5)
            morir();
    }

    void morir()
    {
        FindObjectOfType<AudioManager>().Play("Muerte");
        Debug.Log("Pierdes");
        loseTextObject.SetActive(true);
        updateRecord();  //Se actualiza el record?
        menu.enabled = true;
        Time.timeScale = 0f;
    }


    void OnTriggerEnter(Collider other)
    {
        // ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("ComePick");
            count = count + 100;
            SetCountText();

            if (GameObject.FindGameObjectsWithTag("PickUp").Length % dif_cada == 0)
            {
                Instantiate(Enemigo, new Vector3(0, 10, 0), Quaternion.identity);
              
            }
        }
        if (other.gameObject.CompareTag("powerUP"))
        {
            FindObjectOfType<AudioManager>().Play("ComeBoost");
            count = count - 50;                  //Coger "magias" resta puntos
            SetCountText();
            other.gameObject.SetActive(false);   //elimino el PU
            timeRemainingPU = dif_timePU;        //Reinicio el tiempo 
            countPU.enabled = true;              //Hago visible el contador
            timerPowerUpIsRunning = true;        //Pongo en marcha el tiempo

        }
        if (other.gameObject.CompareTag("speedUP"))
        {
            FindObjectOfType<AudioManager>().Play("ComeBoost");
            count = count - 50;                  //Coger "magias" resta puntos
            SetCountText(); 
            other.gameObject.SetActive(false);   //elimino el PU
            timeRemainingSU = dif_timeSU;        //Reinicio el tiempo 
            countSU.enabled = true;              //Hago visible el contador
            timerSpeedUpIsRunning = true;        //Pongo en marcha el tiempo
            speed = 50;
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           if (timerPowerUpIsRunning)
            {
                FindObjectOfType<AudioManager>().Play("ComeEnemigo");
                count = count + 250;    //Comerse un enemy +250puntos
                SetCountText();
                Destroy(collision.gameObject);
                Instantiate(Enemigo, new Vector3(0, 10, 0), Quaternion.identity);
              //  BlinkGameObject(Enemigo, 10, 10);
            }
            else
            {
                morir();
            }
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void SetCountText()
    {
        countText.text = "Puntos: " + count.ToString();
        Debug.Log("Puntos" + count.ToString());
        if (GameObject.FindGameObjectsWithTag("PickUp").Length == 0)
        {
            // Set the text value of your 'winText'
            Debug.Log("Has ganado");
            count = count + 500;
            updateRecord();  //Se actualiza el record?
            menu.enabled=true;
            winTextObject.SetActive(true);
            Time.timeScale = 0;
        }
   
    }

    void Update()
    {
        if (timerPowerUpIsRunning)
        {
            if (timeRemainingPU > 0)
            {
                timeRemainingPU -= Time.deltaTime;
                countPU.text = ((int)Math.Round(timeRemainingPU)).ToString();
            }
            else
            {
                countPU.enabled = false;
                timeRemainingPU = 0;
                timerPowerUpIsRunning = false;
            }
        }
        if (timerSpeedUpIsRunning)
        {
            if (timeRemainingSU > 0)
            {
                timeRemainingSU -= Time.deltaTime;
                countSU.text = ((int)Math.Round(timeRemainingSU)).ToString();
            }
            else
            {
                countSU.enabled = false;
                timeRemainingSU = 0;
                speed = 15;
                timerSpeedUpIsRunning = false;
            }
        }
    }

    public void updateRecord()
    {
        if (count > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int) count);
            highScore.text = count.ToString();
        }
    }


}




