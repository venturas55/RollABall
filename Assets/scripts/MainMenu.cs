using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas opcDificultad;
    public Canvas menu;
    public Slider sliderOpc1;
    public Slider sliderOpc2;
    public Slider sliderOpc3;
    public Text highScore;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            reestablecerOpcDefecto();
            sliderOpc1.value = PlayerPrefs.GetInt("velocidadenemigos");
            sliderOpc2.value = PlayerPrefs.GetInt("tiempoBooster");
            sliderOpc2.value = PlayerPrefs.GetInt("tiempoBooster");
            sliderOpc3.value = PlayerPrefs.GetInt("Respawn");
            guardarOpciones();
        }
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            opcDificultad.enabled = false;
            highScore.text = "Record actual: " + PlayerPrefs.GetInt("HighScore", 0).ToString() + " puntos";
        }
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void Instrucciones()
    {
        SceneManager.LoadScene("Instrucciones");
    }


    public void Salir()
    {
        Debug.Log("Click en VerPuntuaciones");
        Application.Quit();
    }

    public void opcion1(int val)
    {
        Debug.Log(GameObject.Find("Volume Slider").GetComponent<Slider>().value);
    }

    public void ToggleMostrarOpc()
    {
        menu.enabled = !menu.enabled;
        opcDificultad.enabled = !opcDificultad.enabled;
    }

    public void guardarOpciones()
    {
        PlayerPrefs.SetInt("velocidadenemigos", (int) sliderOpc1.value);
        PlayerPrefs.SetInt("tiempoBooster", (int) sliderOpc2.value);
        PlayerPrefs.SetInt("Respawn", (int) sliderOpc3.value);

    }

    public void reestablecerOpcDefecto()
    {
        sliderOpc1.value = 8;
        sliderOpc2.value = 10;
        sliderOpc3.value = 5;
    }

    public void  update()
    {

    }
    public void resetRecord()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        highScore.text = "Record actual: " + PlayerPrefs.GetInt("HighScore", 0).ToString() + " puntos";
    }




}
