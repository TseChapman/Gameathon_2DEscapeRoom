using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Rigid body and character sprites
    public Rigidbody2D m_rigidBody;
    public Animator m_animator;
    // public GameObject m_camaera;

    // StateManager and phases:
    private StateManager m_stateManager;
    private Phases m_currentPhase;
    private GamePhases m_currentGamePhase;

    // Parameters
    private float m_movementSpeed = 30f;
    private Vector2 m_movement;

    public Vector3 GetCharacterPosition() { return gameObject.transform.position; }

    public void SetCharacterPosition(Vector3 position) { gameObject.transform.position = position; }

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
        // Vector3 cameraPosition =
        // gameObject.transform.Find("CharacterCameraPos").transform.position;
        // m_camaera.transform.position = cameraPosition;
        // Get input movements:
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        m_animator.SetFloat("horizontal", m_movement.x);
        m_animator.SetFloat("vertical", m_movement.y);
        m_animator.SetFloat("speed", m_movement.sqrMagnitude);
    }

    // Fixed Update, update 50 times per sec
    private void FixedUpdate()
    {
        m_rigidBody.MovePosition(m_rigidBody.position +
                                 m_movement * m_movementSpeed * Time.fixedDeltaTime);
    }
}
