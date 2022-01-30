using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseCanvasScript : MonoBehaviour
{
    public AudioMixer mixer;
    [Space]
    public Slider master_scroll;
    public Slider music_scroll;
    public Slider coll_scroll;
    public Slider sfx_scroll;
    [Space]
    public TextMeshProUGUI master_num;
    public TextMeshProUGUI music_num;
    public TextMeshProUGUI coll_num;
    public TextMeshProUGUI sfx_num;
    [Space]
    [Space]
    public GameObject ELEPHANT;
    public GameObject PausePanel;

    private bool isPaused = false;

    void Awake()
    {
        mixer.SetFloat("masterVol", Convert(master_scroll.value));
        mixer.SetFloat("musicVol", Convert(music_scroll.value));
        mixer.SetFloat("collisionVol", Convert(coll_scroll.value));
        mixer.SetFloat("SFXVol", Convert(sfx_scroll.value));
    }

    void Start()
    {
        PausePanel.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("escaped!");
            isPaused = !isPaused; //toggle pause state
            ELEPHANT.GetComponent<TrunkAimer>().paused = isPaused;
        }
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;
        PausePanel.SetActive(isPaused);
        if (!isPaused) return;
        Cursor.lockState = CursorLockMode.None;

        mixer.SetFloat("masterVol",Convert(master_scroll.value));
        mixer.SetFloat("musicVol",Convert(music_scroll.value));
        mixer.SetFloat("collisionVol",Convert(coll_scroll.value));
        mixer.SetFloat("SFXVol",Convert(sfx_scroll.value));

        master_num.text = ((int)(master_scroll.value)).ToString();
        music_num.text = ((int)(music_scroll.value)).ToString();
        coll_num.text = ((int)(coll_scroll.value)).ToString();
        sfx_num.text = ((int)(sfx_scroll.value)).ToString();
    }

    public void ResumeGame()
    {
        isPaused = !isPaused; //toggle pause state
        ELEPHANT.GetComponent<TrunkAimer>().paused = isPaused;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private float Convert(float val)
    {
        float i = val / 100;
        //return Mathf.Log10(val) * 20);
        return 5.0f * i + (-80.0f) * (1 - i); //20.0 Db is the loudest, -80.0 Db is the quietest
    }
}
