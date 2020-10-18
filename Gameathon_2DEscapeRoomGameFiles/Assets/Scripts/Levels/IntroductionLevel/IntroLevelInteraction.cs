using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroLevelInteraction : MonoBehaviour
{
    public GameObject m_camera;
    public GameObject m_useButton;
    public GameObject m_paper;
    public GameObject m_passcodePad;
    public GameObject m_passcodeText;
    public GameObject m_paperPosition;
    public GameObject m_doorPosition;
    public GameObject m_incorrectImage;
    public GameObject m_correctImage;

    // StateManager
    private StateManager m_stateManager;
    private Phases m_currentPhase;
    private GamePhases m_currentGamePhase;
    private Levels m_selectedLevel;

    // Character's position
    private CharacterMovement m_characterMovement;
    private Vector3 m_characterPosition;

    // Parameters
    private float m_characterPaperDist;
    private float m_characterDoorDist;

    private bool m_isCloserToPaper = false;
    private bool m_isCloserToDoor = false;

    private string m_password = "5629";
    private string m_enteredText = "";
    private const int MAX_PASSCODE = 11;
    private const int MAX_IMAGE_FLICKS = 6;
    private int m_numFlick;
    private float m_timer = 1f;
    private bool m_showEnteredResult = false;
    private bool m_isCorrect = false;

    private bool m_isFinishLevel = false;

    // Show different Effect on pressing the button
    public void UseButton()
    {
        if (m_isCloserToPaper is true)
        {
            // Show the paper hint
            FindObjectOfType<AudioManager>().Play("PaperFlip");
            m_paper.SetActive(true);
        }
        else if (m_isCloserToDoor is true)
        {
            // Show a password keypad
            m_passcodePad.SetActive(true);
        }
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
            m_isFinishLevel = true;
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

    // Calculate the distance between character to paper and door
    private void CalculateDistance()
    {
        // Get paper's position
        float paperXPos = m_paperPosition.transform.position.x;
        float paperYPos = m_paperPosition.transform.position.y;

        // Get door's position
        float doorXPos = m_doorPosition.transform.position.x;
        float doorYPos = m_doorPosition.transform.position.y;

        // Get character's position
        float characterXPos = m_characterPosition.x;
        float characterYPos = m_characterPosition.y;

        // Get X and Y distance between paper and character
        float xPaperDiff = paperXPos - characterXPos;
        float yPaperDiff = paperYPos - characterYPos;

        // Get X and Y distance between door and character
        float xDoorDiff = doorXPos - characterXPos;
        float yDoorDiff = doorYPos - characterYPos;

        // Calculate and set the distances
        m_characterPaperDist = (float) Math.Sqrt(xPaperDiff * xPaperDiff + yPaperDiff * yPaperDiff);
        m_characterDoorDist = (float) Math.Sqrt(xDoorDiff * xDoorDiff + yDoorDiff * yDoorDiff);
    }

    private void CheckDistance()
    {
        // Turn button on if it get close to any of the paper and door
        m_useButton.SetActive((m_characterPaperDist <= 15f || m_characterDoorDist <= 15f) ? true
                                                                                          : false);

        // Set is closer to door or paper based on conditions
        if (m_characterPaperDist <= 15f)
        {
            m_isCloserToPaper = true;
            m_isCloserToDoor = false;
            m_passcodePad.SetActive(false);
        }
        else if (m_characterDoorDist <= 15f)
        {
            m_isCloserToPaper = false;
            m_isCloserToDoor = true;
            m_paper.SetActive(false);
        }
        else if (m_characterPaperDist > 15f && m_characterDoorDist > 15f)
        {
            m_isCloserToDoor = false;
            m_isCloserToPaper = false;
            m_paper.SetActive(false);
            m_passcodePad.SetActive(false);
        }
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

            // If the passcode is entered correctly, game phase = EXIT, phase = SELECT_LEVEL
            if (m_isFinishLevel is true)
            {
                m_stateManager.SetCurrentPhase(Phases.SELECT_LEVEL);
                m_stateManager.SetCurrentGamePhase(GamePhases.EXIT);
                m_enteredText = "";
                // Unactive the level
                m_useButton.SetActive(false);
                m_paper.SetActive(false);
                m_passcodePad.SetActive(false);
                m_incorrectImage.SetActive(false);
                m_correctImage.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_stateManager = GameObject.FindObjectOfType<StateManager>();
        m_characterMovement = GameObject.FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    private void Update()
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
        if (m_selectedLevel != Levels.INTRODUCTION)
        {
            return;
        }
        m_characterPosition = m_characterMovement.GetCharacterPosition();
        m_camera.transform.position = new Vector3(m_characterPosition.x, m_characterPosition.y, m_camera.transform.position.z);
        CalculateDistance();
        CheckDistance();
        UpdateText();
        DoFlicks();
    }
}
