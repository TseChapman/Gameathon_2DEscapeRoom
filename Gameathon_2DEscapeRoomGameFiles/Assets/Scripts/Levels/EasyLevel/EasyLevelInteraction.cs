using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectPositions {
    CHEST_POSITION = 0,
    STORAGE_FRAME_POSITION = 1,
    STORAGE_DOOR_POSITION = 2,
    HALLWAY_FRAME_POSITION = 3,
    HALLWAY_DOOR_POSITION = 4,
    MUSEUM_GLASSFRAME_NOTE_POSITION = 5,
    TOP_MUSEUM_FRAME_POSITION = 6,
    RIGHT_MUSEUM_FRAME_POSITION = 7,
    ESCAPE_DOOR_POSITION = 8,
    NUM_OBJECT_POSITIONS = 9
}

public class EasyLevelInteraction : MonoBehaviour
{
    public GameObject m_camera;
    public GameObject m_useButton;
    public GameObject[] m_objectPositions =
        new GameObject[(int) ObjectPositions.NUM_OBJECT_POSITIONS];
    public GameObject[] m_actionObjects =
        new GameObject[(int) ObjectPositions.NUM_OBJECT_POSITIONS];
    public GameObject m_hallwayStartPos;
    public GameObject m_museumStartPos;

    // StateManager
    private StateManager m_stateManager;
    private Phases m_currentPhase;
    private GamePhases m_currentGamePhase;
    private Levels m_selectedLevel;

    // Character's position
    private CharacterMovement m_characterMovement;
    private Vector3 m_characterPosition;

    // Closest Object
    private ObjectPositions m_closestObjectPos = ObjectPositions.NUM_OBJECT_POSITIONS;

    private ChestPasscode m_chestPasscode;
    private bool m_isChestCorrectlyEntered = false;
    private float m_needKeyTextFlashTimer = 0f;

    private PasscodePad m_passcodePad;
    private bool m_isHallwayCodeEnterCorrectly = false;
    private bool m_isCharacterInMuseum = false;

    public void ResetParameters()
    {
        m_isChestCorrectlyEntered = false;
        m_isHallwayCodeEnterCorrectly = false;
        m_isCharacterInMuseum = false;
        // Easy Level Done
        // Reset parameters
        m_stateManager.SetCurrentPhase(Phases.SELECT_LEVEL);
        m_stateManager.SetCurrentGamePhase(GamePhases.EXIT);
        m_chestPasscode.ResetParameters();
        m_passcodePad.ResetParameters();
        gameObject.SetActive(false);
    }

    public void UseButton()
    {
        if (m_closestObjectPos == ObjectPositions.CHEST_POSITION)
        {
            // Active the chest passcode pad
            m_actionObjects[(int) ObjectPositions.CHEST_POSITION].SetActive(true);
            Debug.Log("Chest");
        }
        else if (m_closestObjectPos == ObjectPositions.STORAGE_FRAME_POSITION)
        {
            // Active the storage frame
            m_actionObjects[(int) ObjectPositions.STORAGE_FRAME_POSITION].SetActive(true);
            Debug.Log("Storage Frame");
        }
        else if (m_closestObjectPos == ObjectPositions.STORAGE_DOOR_POSITION)
        {
            // check if chest passcode is entered correctly
            if (m_isChestCorrectlyEntered is true)
            {
                FindObjectOfType<AudioManager>().Play("DoorOpen");
                m_characterMovement.SetCharacterPosition(m_hallwayStartPos.transform.position);
            }
            else
            {
                m_actionObjects[(int) ObjectPositions.STORAGE_DOOR_POSITION].SetActive(
                    (m_actionObjects[(int) ObjectPositions.STORAGE_DOOR_POSITION]
                         .activeSelf is true)
                        ? false
                        : true);
                m_needKeyTextFlashTimer = 3f;
            }
            Debug.Log("Storage Door");
        }
        else if (m_closestObjectPos == ObjectPositions.HALLWAY_FRAME_POSITION)
        {
            // Active the hallway frame
            m_actionObjects[(int) ObjectPositions.HALLWAY_FRAME_POSITION].SetActive(true);
            Debug.Log("Hallway Frame");
        }
        else if (m_closestObjectPos == ObjectPositions.HALLWAY_DOOR_POSITION)
        {
            // Active the check if passcode is entered correctly
            m_actionObjects[(int) ObjectPositions.HALLWAY_DOOR_POSITION].SetActive(true);
            Debug.Log("Hallway Door");
        }
        else if (m_closestObjectPos == ObjectPositions.MUSEUM_GLASSFRAME_NOTE_POSITION)
        {
            // Active the glassframe note
            m_actionObjects[(int) ObjectPositions.MUSEUM_GLASSFRAME_NOTE_POSITION].SetActive(true);
            Debug.Log("Glassframe");
        }
        else if (m_closestObjectPos == ObjectPositions.TOP_MUSEUM_FRAME_POSITION)
        {
            // Active the top museum frames
            m_actionObjects[(int) ObjectPositions.TOP_MUSEUM_FRAME_POSITION].SetActive(true);
            Debug.Log("Top Museum Frame");
        }
        else if (m_closestObjectPos == ObjectPositions.RIGHT_MUSEUM_FRAME_POSITION)
        {
            // Active the top museum frames
            m_actionObjects[(int) ObjectPositions.RIGHT_MUSEUM_FRAME_POSITION].SetActive(true);
            Debug.Log("Right Museum Frame");
        }
        else if (m_closestObjectPos == ObjectPositions.ESCAPE_DOOR_POSITION)
        {
            // Active the Escape door passcode pad
            m_actionObjects[(int) ObjectPositions.ESCAPE_DOOR_POSITION].SetActive(true);
            Debug.Log("Escape Door");
        }
        Debug.Log("Pressed Use Button");
        Debug.Log(m_closestObjectPos.ToString());
    }

    private void CheckDistance()
    {
        float currentMin = 9999999999999f;
        ObjectPositions closestObject = ObjectPositions.NUM_OBJECT_POSITIONS;
        for (int objectIndex = 0; objectIndex < m_objectPositions.Length; objectIndex++)
        {
            float distance = Vector3.Distance(m_objectPositions[objectIndex].transform.position, m_characterPosition);
            if (distance < currentMin)
            {
                currentMin = distance;
                closestObject = (ObjectPositions) objectIndex;
            }
            if (distance > 15f) // Unactive the object that is too far away
            {
                m_actionObjects[objectIndex].SetActive(false);
            }
        }
        m_closestObjectPos = closestObject;
        m_useButton.SetActive((currentMin <= 15f) ? true : false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_stateManager = GameObject.FindObjectOfType<StateManager>();
        m_characterMovement = GameObject.FindObjectOfType<CharacterMovement>();
        m_chestPasscode = GameObject.FindObjectOfType<ChestPasscode>();
        m_passcodePad = GameObject.FindObjectOfType<PasscodePad>();
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
        if (m_selectedLevel != Levels.EASY)
        {
            return;
        }
        m_needKeyTextFlashTimer = Math.Max(m_needKeyTextFlashTimer - Time.deltaTime, 0);
        if (m_needKeyTextFlashTimer <= 0f)
        {
            m_actionObjects[(int) ObjectPositions.STORAGE_DOOR_POSITION].SetActive(false);
        }
        m_characterPosition = m_characterMovement.GetCharacterPosition(); // Get character position
        m_camera.transform.position = new Vector3(m_characterPosition.x,m_characterPosition.y, m_camera.transform.position.z); // Track the character with camera
        CheckDistance();
        m_isChestCorrectlyEntered = m_chestPasscode.GetIsChestCorrectlyEntered();
        m_isHallwayCodeEnterCorrectly = m_passcodePad.GetIsCorrect();
        if (m_isCharacterInMuseum is false && m_isHallwayCodeEnterCorrectly is true)
        {
            FindObjectOfType<AudioManager>().Play("DoorOpen");
            m_characterMovement.SetCharacterPosition(m_museumStartPos.transform.position);
            m_isCharacterInMuseum = true;
        }
    }
}
