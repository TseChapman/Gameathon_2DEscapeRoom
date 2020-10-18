using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestPasscode : MonoBehaviour
{
    public GameObject m_chestPasscodePad;
    public GameObject[] m_enteredText = new GameObject[3]; // 3 slot for text entered
    public GameObject m_gotTheKeyText;

    // StateManager
    private StateManager m_stateManager;
    private Phases m_currentPhase;
    private GamePhases m_currentGamePhase;
    private Levels m_selectedLevel;

    private bool m_isChestCorrectlyEntered = false;
    private int m_textIndex = 0;
    private string m_enteredTextStr = "";
    private string m_correctCode = "763";

    public bool GetIsChestCorrectlyEntered() { return m_isChestCorrectlyEntered; }

    public void ResetParameters()
    {
        m_isChestCorrectlyEntered = false;
        m_textIndex = 0;
        m_enteredTextStr = "";
        foreach (GameObject textComp in m_enteredText)
        {
            textComp.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }
        m_gotTheKeyText.SetActive(false);
    }

    public void PressEnterButton()
    {
        if (m_enteredTextStr == m_correctCode) // Correct text is entered
        {
            m_isChestCorrectlyEntered = true;
            m_gotTheKeyText.SetActive(true);
        }
        else
        {
            m_textIndex = 0;
            m_enteredTextStr = "";
            foreach (GameObject textComp in m_enteredText)
            {
                textComp.GetComponent<TMPro.TextMeshProUGUI>().text = "";
            }
        }
    }

    // Start is called before the first frame update
    private void Start() { m_stateManager = GameObject.FindObjectOfType<StateManager>(); }

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
        if (m_selectedLevel != Levels.EASY)
        {
            return;
        }
        if (m_chestPasscodePad.activeSelf is true)
        {
            if (Input.anyKey)
            {
                // Check if text did not exceed text length limits. And character is between 0
                // and 9.
                if (m_textIndex < m_enteredText.Length && Input.inputString.Length == 1 &&
                    int.Parse(Input.inputString) >= 0 && int.Parse(Input.inputString) <= 9)
                {
                    m_enteredText[m_textIndex].GetComponent<TMPro.TextMeshProUGUI>().text =
                        Input.inputString;
                    m_enteredTextStr += Input.inputString;
                    m_textIndex++;
                }
            }
        }
    }
}
