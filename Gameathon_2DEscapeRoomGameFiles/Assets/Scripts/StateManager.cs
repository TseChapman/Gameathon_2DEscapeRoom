using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phases { PLAY_GAME = 0, SELECT_LEVEL = 1, START_GAME = 2, PLAY = 3, NUM_PHASES = 4 }

public enum GamePhases { SETUP_MAP = 0, PLAY = 1, EXIT = 2, NUM_GAME_PHASES = 3 }

public class StateManager : MonoBehaviour
{
    private Phases m_currentPhase = Phases.PLAY_GAME; // Starts from Play_Game phase
    private GamePhases m_currentGamePhase =
        GamePhases.NUM_GAME_PHASES; // Initialized as invalid phase
    public GameObject m_creditText;

    private Levels m_selectedLevel = Levels.NUM_LEVELS;

    // Exit the game application
    public void Exit() { Application.Quit(); }

    // Credit Button
    public void Credit()
    {
        // Active a credit page if page is unactive
        m_creditText.SetActive((m_creditText.activeSelf is false) ? true : false);
    }

    // Set the current phase to the inputted phase
    public void SetCurrentPhase(Phases phase) { m_currentPhase = phase; }

    // Set the current game phase to inputted phase
    public void SetCurrentGamePhase(GamePhases phase) { m_currentGamePhase = phase; }

    // Set the selected level to inputted level
    public void SetSelectedLevel(Levels level) { m_selectedLevel = level; }

    // Get the current phase
    public Phases GetCurrentPhase() { return m_currentPhase; }

    // Get the current game phase
    public GamePhases GetCurrentGamePhase() { return m_currentGamePhase; }

    // Get the current selected level
    public Levels GetSelectedLevel() { return m_selectedLevel; }

    // Reset the selected level
    public void ResetSelectedLevel() { m_selectedLevel = Levels.NUM_LEVELS; }

    // Start is called before the first frame update
    private void Start() {}

    // Update is called once per frame
    private void Update() {}
}
