using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Levels { INTRODUCTION = 0, EASY = 1, NUM_LEVELS = 2 }

public class LevelSelection : MonoBehaviour
{
    // Use to set button active/unactive
    public GameObject[] m_levelsButtons = new GameObject[(int) Levels.NUM_LEVELS];
    public GameObject m_levels;

    // StateManager
    private StateManager m_stateManager;
    private Phases m_currentPhase;

    // Parameters
    private bool m_isLevelsActive = false;
    private bool m_isPlayerFirstGame = true;

    // Intro Level Button's function
    public void PlayIntroLevel()
    {
        // Deactivate the all buttons
        SetButtonActivation(false);
        // Deactivate the level selection UI
        gameObject.SetActive(false);
        // Switch phases to Start_Game and game phases to setup_map
        m_stateManager.SetCurrentPhase(Phases.START_GAME);
        m_stateManager.SetCurrentGamePhase(GamePhases.SETUP_MAP);
        // Set the selected level to intro
        m_stateManager.SetSelectedLevel(Levels.INTRODUCTION);
        // Set is player first game to false
        m_isPlayerFirstGame = false;
        m_isLevelsActive = false;
    }

    public void PlayEasyLevel()
    {
        SetButtonActivation(false);

        gameObject.SetActive(false);

        m_stateManager.SetCurrentPhase(Phases.START_GAME);
        m_stateManager.SetCurrentGamePhase(GamePhases.SETUP_MAP);

        m_stateManager.SetSelectedLevel(Levels.EASY);
        m_isLevelsActive = false;
    }

    // Set isLevel to inputted boolean
    public void SetIsLevelActive(bool isActive) { m_isLevelsActive = isActive; }

    // Set all the buttons' activation to isActive: true/false
    private void SetButtonActivation(bool isActive)
    {
        foreach(GameObject button in m_levelsButtons) { button.SetActive(isActive); }
    }

    // Start is called before the first frame update
    private void Start() { m_stateManager = GameObject.FindObjectOfType<StateManager>(); }

    // Update is called once per frame
    private void Update()
    {
        m_currentPhase = m_stateManager.GetCurrentPhase();
        if (m_currentPhase != Phases.SELECT_LEVEL)
        {
            return;
        }
        if (m_isLevelsActive is false)
        {
            // Active the buttons for the level
            if (m_isPlayerFirstGame is true)
            {
                // Activate the Introduction level's button only
                m_levelsButtons[(int) Levels.INTRODUCTION].SetActive(true);
                Debug.Log("Only Active Intro level");
            }
            else
            {
                // Activate all levels' button
                SetButtonActivation(true);
                Debug.Log("Active all level");
            }
            m_isLevelsActive = true;
        }
    }
}
