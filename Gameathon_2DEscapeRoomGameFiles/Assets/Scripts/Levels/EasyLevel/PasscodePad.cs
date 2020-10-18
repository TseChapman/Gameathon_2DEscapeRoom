using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasscodePad : MonoBehaviour
{
    public GameObject m_passcodePad;
    public GameObject m_passcodeText;
    public GameObject m_incorrectImage;
    public GameObject m_correctImage;

    // StateManager
    private StateManager m_stateManager;
    private Phases m_currentPhase;
    private GamePhases m_currentGamePhase;
    private Levels m_selectedLevel;

    private string m_password = "4034";
    private string m_enteredText = "";
    private const int MAX_PASSCODE = 11;
    private const int MAX_IMAGE_FLICKS = 6;
    private int m_numFlick;
    private float m_timer = 1f;
    private bool m_showEnteredResult = false;
    private bool m_isCorrect = false;

    public bool GetIsCorrect() { return m_isCorrect; }

    public void ResetParameters()
    {
        m_enteredText = "";
        m_timer = 1f;
        m_showEnteredResult = false;
        m_isCorrect = false;
    }

    // Passcode Pad's Buttons:
    public void Button0() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("0"); 
    }

    public void Button1() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("1"); 
    }

    public void Button2() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("2"); 
    }

    public void Button3() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("3"); 
    }

    public void Button4() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("4"); 
    }

    public void Button5() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("5"); 
    }

    public void Button6() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("6"); 
    }

    public void Button7() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("7"); 
    }

    public void Button8() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("8");
    }

    public void Button9() 
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        AddPasscode("9"); 
    }

    public void ButtonDel()
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        if (m_enteredText.Length > 0)
            m_enteredText = m_enteredText.Remove(m_enteredText.Length - 1, 1);
    }

    public void ButtonEnter()
    {
        FindObjectOfType<AudioManager>().Play("ButtonBeep");
        if (m_enteredText == m_password)
        {
            // Entered the correct Password, escaped the room
            // Debug.Log("Correct");
            m_showEnteredResult = true;
            m_isCorrect = true;
        }
        else
        {
            // Tell player that the password entered is incorrect, re-enter the password
            m_enteredText = ""; // Reset the text
            // Debug.Log("Incorrect");
            m_showEnteredResult = true;
        }
    }

    private void AddPasscode(string code)
    {
        if (m_enteredText.Length < MAX_PASSCODE)
            m_enteredText += code;
    }

    private void UpdateText()
    {
        m_passcodeText.GetComponent<TMPro.TextMeshProUGUI>().text = m_enteredText;
    }

    private void DoFlicks()
    {
        if (m_showEnteredResult is true)
        {
            m_timer -= Time.deltaTime;
            if (m_timer <= 0f)
            {
                if (m_isCorrect is false)
                {
                    m_incorrectImage.SetActive(m_incorrectImage.activeSelf ? false : true);
                    m_correctImage.SetActive(false);
                }
                else if (m_isCorrect is true)
                {
                    m_correctImage.SetActive(m_correctImage.activeSelf ? false : true);
                    m_incorrectImage.SetActive(false);
                }
                m_numFlick += 1;
                m_timer = 1f;
            }
        }
        if (m_numFlick >= MAX_IMAGE_FLICKS)
        {
            // Reset Parameters
            m_showEnteredResult = false;
            m_numFlick = 0;

            // TODO: Teleport character to Museum start position
        }
    }

    // Start is called before the first frame update
    void Start() { m_stateManager = GameObject.FindObjectOfType<StateManager>(); }

    // Update is called once per frame
    void Update()
    {
        m_currentPhase = m_stateManager.GetCurrentPhase();
        if (m_currentPhase != Phases.PLAY)
        {
            return;
        }
        m_currentGamePhase = m_stateManager.GetCurrentGamePhase();
        if (m_currentGamePhase != GamePhases.PLAY)
        {
            return;
        }
        m_selectedLevel = m_stateManager.GetSelectedLevel();
        if (m_selectedLevel != Levels.EASY)
        {
            return;
        }
        if (m_passcodePad.activeSelf is true)
        {
            UpdateText();
            DoFlicks();
        }
    }
}
