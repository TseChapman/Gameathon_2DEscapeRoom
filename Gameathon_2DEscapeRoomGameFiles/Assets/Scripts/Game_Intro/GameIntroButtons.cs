using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntroButtons : MonoBehaviour
{
    public GameObject m_gameIntro;        // Whole Game Intro page
    public GameObject m_gameIntroButtons; // Whole Game Intro Buttons
    public GameObject m_levelSelection;   // Level Selection UI, active = false

    // StateManager
    private StateManager m_stateManager;
    private Phases m_currentPhase;
    private GamePhases m_currentGamePhase;

    // is Start UI setup?
    private bool m_isPlayGameSetup = false;

    public void SetLevelSelectionActive(bool isActive) { m_levelSelection.SetActive(isActive); }

    // End the Game Intro page and move onto the select level page
    public void Play()
    {
        m_gameIntro.SetActive(false);
        m_gameIntroButtons.SetActive(false);
        m_levelSelection.SetActive(true);
        m_stateManager.SetCurrentPhase(Phases.SELECT_LEVEL);
    }

    // Start is called before the first frame update
    private void Start() { m_stateManager = GameObject.FindObjectOfType<StateManager>(); }

    // Update is called once per frame
    private void Update()
    {
        m_currentPhase = m_stateManager.GetCurrentPhase();
        m_currentGamePhase = m_stateManager.GetCurrentGamePhase();
        if (m_currentPhase == Phases.SELECT_LEVEL && m_currentGamePhase == GamePhases.EXIT)
        {
            m_levelSelection.SetActive(true);
        }
        if (m_currentPhase == Phases.PLAY_GAME && m_isPlayGameSetup is false)
        {
            m_gameIntro.SetActive(true);
            m_gameIntroButtons.SetActive(true);
            m_levelSelection.SetActive(false);
        }
    }
}
