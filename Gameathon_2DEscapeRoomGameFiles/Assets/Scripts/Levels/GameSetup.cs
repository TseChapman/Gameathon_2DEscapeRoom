using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject[] m_levels = new GameObject[(int) Levels.NUM_LEVELS];
    public GameObject m_character;
    public GameObject m_levelSelectionGO;

    // StateManager
    private StateManager m_stateManager;
    private Phases m_currentPhase;
    private GamePhases m_currentGamePhase;
    private Levels m_selectedLevel;

    // LevelSelection
    // private LevelSelection m_levelSelection;

    // parameter
    // private bool m_isLevelSetup = false;

    // Reset the parameter
    // public void ResetGameSetup() { m_isLevelSetup = false; }

    // Set the level map active to true and place the character to the start position
    private void SetupLevel()
    {
        // Set the level map active to true
        m_levels[(int) m_selectedLevel].SetActive(true);
        // Place the character in map start position
        Vector3 startPosition =
            m_levels[(int) m_selectedLevel].transform.Find("StartPosition").transform.position;
        m_character.transform.position = startPosition;
        m_character.SetActive(true);
    }

    // Change from Setup phase to play phase
    private void ChangePhases()
    {
        m_stateManager.SetCurrentPhase(Phases.PLAY);
        m_stateManager.SetCurrentGamePhase(GamePhases.PLAY);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_stateManager = GameObject.FindObjectOfType<StateManager>();
        // m_levelSelection = m_levelSelectionGO.GetComponent<LevelSelection>();
    }

    // Update is called once per frame
    private void Update()
    {
        m_currentPhase = m_stateManager.GetCurrentPhase();
        m_currentGamePhase = m_stateManager.GetCurrentGamePhase();
        if (m_currentPhase == Phases.SELECT_LEVEL && m_currentGamePhase == GamePhases.EXIT)
        {
            m_character.SetActive(false);
            m_levelSelectionGO.SetActive(true);
            // m_levelSelection.SetIsLevelActive(false);
        }
        if (m_currentPhase == Phases.PLAY && m_currentGamePhase == GamePhases.PLAY)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
        if (m_currentPhase != Phases.START_GAME)
        {
            return;
        }
        if (m_levels[(int) m_selectedLevel].activeSelf is false)
        {
            m_selectedLevel = m_stateManager.GetSelectedLevel();
            SetupLevel();
            ChangePhases();
            // m_isLevelSetup = true;
        }
    }
}