using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TitleManager : MonoBehaviour
{
    public string[] scenes;

    public TMP_Text[] highScores;
    [Space]
    public float floatTime = 0.3f;
    [Space]
    private GameObject _focused_panel;
    private Vector3 _prev_location;
    public GameObject MENU;
    public GameObject options_panel;
    public GameObject controls_panel;
    public GameObject levels_panel;
    public GameObject credits_panel;
    [Space]
    [Space]
    [Space]
    public AudioMixer mixer;
    public Slider master_scroll;
    public Slider music_scroll;
    public Slider coll_scroll;
    public Slider sfx_scroll;
    public TextMeshProUGUI master_num;
    public TextMeshProUGUI music_num;
    public TextMeshProUGUI coll_num;
    public TextMeshProUGUI sfx_num;
    public GameObject dummy; // reference for the middle of the canvas

    private void Start()
    {
        for (int i = 0; i < highScores.Length; ++i)
        {
            if (PlayerPrefs.HasKey("MaxScene" + (i + 2)))
                highScores[i].text = "Egg Score: " + PlayerPrefs.GetInt("MaxScene" + (i + 2));
            else
                highScores[i].text = "No Egg Score";
        }
        mixer.SetFloat("masterVol", Convert(master_scroll.value));
        mixer.SetFloat("musicVol", Convert(music_scroll.value));
        mixer.SetFloat("collisionVol", Convert(coll_scroll.value));
        mixer.SetFloat("SFXVol", Convert(sfx_scroll.value));
    }

    public void Update()
    {
        mixer.SetFloat("masterVol", Convert(master_scroll.value));
        mixer.SetFloat("musicVol", Convert(music_scroll.value));
        mixer.SetFloat("collisionVol", Convert(coll_scroll.value));
        mixer.SetFloat("SFXVol", Convert(sfx_scroll.value)); 
        master_num.text = ((int)(master_scroll.value)).ToString();
        music_num.text = ((int)(music_scroll.value)).ToString();
        coll_num.text = ((int)(coll_scroll.value)).ToString();
        sfx_num.text = ((int)(sfx_scroll.value)).ToString();
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(scenes[level]);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        MENU.SetActive(true);
        StartCoroutine("MovePanelBack");
    }
    // Valid inputs: options, controls, levels, credits
    public void Focus(string choice)
    {
        MENU.SetActive(false);
        if (choice.Equals("options")) _focused_panel = options_panel;
        if (choice.Equals("controls")) _focused_panel = controls_panel;
        if (choice.Equals("levels")) _focused_panel = levels_panel;
        if (choice.Equals("credits")) _focused_panel = credits_panel;

        // move the focused panel into view.
        StartCoroutine("MovePanel");
    }   
    public IEnumerator MovePanel()
    {
        _prev_location = _focused_panel.GetComponent<RectTransform>().anchoredPosition;
        float t = 0.0f;
        while (t <= floatTime)
        {
            Vector3 temp = Vector3.Lerp(_prev_location, dummy.GetComponent<RectTransform>().anchoredPosition, t / floatTime);
            _focused_panel.GetComponent<RectTransform>().anchoredPosition = temp;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _focused_panel.transform.position = transform.position;
    }
    public IEnumerator MovePanelBack()
    {
        Vector3 start_loc = _focused_panel.GetComponent<RectTransform>().anchoredPosition;
        float t = 0.0f;
        while (t <= floatTime)
        {
            Vector3 temp = Vector3.Lerp(start_loc, _prev_location, t / floatTime);
            //_focused_panel.transform.position = temp;
            _focused_panel.GetComponent<RectTransform>().anchoredPosition = temp;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _focused_panel.GetComponent<RectTransform>().anchoredPosition = _prev_location;
        _focused_panel = null;
    }

    private float Convert(float val)
    {
        float i = val / 100;
        //return Mathf.Log10(val) * 20);
        return 5.0f * i + (-80.0f) * (1 - i); //20.0 Db is the loudest, -80.0 Db is the quietest
    }
}
