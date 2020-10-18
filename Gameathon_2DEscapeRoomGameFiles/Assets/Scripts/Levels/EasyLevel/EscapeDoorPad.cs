using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeDoorPad : MonoBehaviour
{
    public GameObject m_escapeDoorPad;
    public GameObject[] m_codeTextArr = new GameObject[4];

    // StateManager
    private StateManager m_stateManager;
    private Phases m_currentPhase;
    private GamePhases m_currentGamePhase;
    private Levels m_selectedLevel;

    private string m_correctCode = "1213";
    private string m_enteredText = "";
    private int m_textIndex = 0;
    private bool m_isCodeCorrectlyEntered = false;

    public bool GetIsCodeCorrectlyEntered() { return m_isCodeCorrectlyEntered; }

    public void ResetParameters()
    {
        m_enteredText = "";
        m_textIndex = 0;
        m_isCodeCorrectlyEntered = false;
        foreach (GameObject textComp in m_codeTextArr)
        {
            textComp.GetComponent<Text>().text = "";
        }
    }

    public void PressEnterButton()
    {
        if (m_enteredText == m_correctCode) // Correct text is entered
        {
            m_isCodeCorrectlyEntered = true;
            m_escapeDoorPad.SetActive(false);
            Debug.Log("Correct");
            ResetParameters();
            FindObjectOfType<EasyLevelInteraction>().ResetParameters();
        }
        else
        {
            Debug.Log("Incorrect");
            m_textIndex = 0;
            m_enteredText = "";
            foreach (GameObject textComp in m_codeTextArr)
            {
                textComp.GetComponent<Text>().text = "";
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
        if (m_escapeDoorPad.activeSelf is true)
        {
            if (Input.anyKey)
            {
                if (m_textIndex < m_codeTextArr.Length && Input.inputString.Length == 1 &&
                    int.Parse(Input.inputString) >= 0 && int.Parse(Input.inputString) <= 9)
                {
                    m_codeTextArr[m_textIndex].GetComponent<Text>().text = Input.inputString;
                    m_enteredText += Input.inputString;
                    m_textIndex++;
                }
            }
        }
    }
}
