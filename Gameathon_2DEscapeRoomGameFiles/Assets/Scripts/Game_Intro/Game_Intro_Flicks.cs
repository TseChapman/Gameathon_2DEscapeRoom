using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Intro_Flicks : MonoBehaviour
{
    public GameObject m_GameIntroMarks; // Initialize Active is false

    private StateManager m_stateManager;
    private Phases m_currentPhase;

    private float m_timer = 1f;

    // Start is called before the first frame update
    private void Start() { m_stateManager = GameObject.FindObjectOfType<StateManager>(); }

    // Update is called once per frame
    private void Update()
    {
        m_currentPhase = m_stateManager.GetCurrentPhase();
        if (m_currentPhase != Phases.PLAY_GAME)
        {
            return;
        }
        m_timer -= Time.deltaTime;
        if (m_timer <= 0f)
        {
            m_GameIntroMarks.SetActive(m_GameIntroMarks.activeSelf ? false : true);
            m_timer = 1f;
        }
    }
}
