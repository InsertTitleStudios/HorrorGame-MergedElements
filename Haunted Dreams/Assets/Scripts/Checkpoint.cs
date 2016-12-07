using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Checkpoint : MonoBehaviour
{
    public static int _currentMatches = 3;
    public static int tempMatches = 0;
    public int _Respawnedmatches;
    public bool inZone = false;
    public Text matches_text;
    public Text _HUD_Text;
    public CanvasGroup canvasGroup = null;   
    public LevelManager levelManager;
    public bool respawn = false;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        matches_text.text = "X " + _currentMatches;
        _HUD_Text.text = " ";
        tempMatches = _currentMatches;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inZone && levelManager.currentCheckpoint != gameObject)
        {
            if (_currentMatches >= 1)
            {
                levelManager.currentCheckpoint = gameObject;
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
                gameObject.GetComponentInChildren<Light>().enabled = true;
                _currentMatches--;
                tempMatches = _currentMatches;
                foreach (PickUpMatches match in levelManager.tempPickedUpMatches)
                {
                    match.checkpointActivated = true;
                }
                matches_text.text = "X " + _currentMatches;
            }
            else if (_currentMatches <= 0)
            {
                _currentMatches = 0;
            }
        }
        if (!inZone && levelManager.currentCheckpoint != gameObject)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            gameObject.GetComponentInChildren<Light>().enabled = false;
        }
        if (respawn == true)
        {
            _currentMatches = tempMatches;
            matches_text.text = "X " + _currentMatches;
            respawn = false;
        }
    }
      public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            inZone = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        { inZone = false; }
    }
    public void AddMatches(int _matchesAmount)
    {
        _currentMatches += _matchesAmount;
        matches_text.text = "X " + _currentMatches;
    }
}