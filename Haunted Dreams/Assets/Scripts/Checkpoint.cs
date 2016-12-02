using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Checkpoint : MonoBehaviour
{

    public static int _currentMatches = 3;
    public int tempMatches;
    private float speed = 1f;

    public bool inZone = false;
    private bool triggered = false;

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
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F) && inZone && levelManager.currentCheckpoint != gameObject)
        {
            StopCoroutine("Opacity");
            triggered = false;
            if (_currentMatches >= 1)
            {
                levelManager.currentCheckpoint = gameObject;
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
                gameObject.GetComponentInChildren<Light>().enabled = true;
                _currentMatches--;
                
                tempMatches = _currentMatches;
                matches_text.text = "X " + _currentMatches;
                if (respawn == true)
                {
                    Debug.Log("Just respawned");
                    _currentMatches = tempMatches;
                    Debug.Log("Number of matches is:" + _currentMatches);
                    matches_text.text = "X " + _currentMatches;
                    respawn = false;
                }

                StartCoroutine("Opacity");
                _HUD_Text.text = "Checkpoint Activated";
            }
            else if (_currentMatches <= 0)
            { _currentMatches = 0; }
        }
        if (inZone && levelManager.currentCheckpoint != gameObject)
        {
            _HUD_Text.text = "Checkpoint Discovered \nPress F to activate";
            StartCoroutine("Opacity");
        }

        if (!inZone && levelManager.currentCheckpoint != gameObject)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            gameObject.GetComponentInChildren<Light>().enabled = false;
        }
        if (inZone && levelManager.currentCheckpoint != gameObject && _currentMatches == 0)
        {
            _HUD_Text.text = "No Matches To Activate Checkpoint \nGo Find Some";
            StartCoroutine("Opacity");
        }
    }
    IEnumerator Opacity()
    {
        if (!triggered)
        {
            triggered = true;
            if (canvasGroup.alpha == 1)
            {
                canvasGroup.alpha = 0;
            }
            yield return FadeIn();
            yield return new WaitForSeconds(1f);
            yield return FadeOut();
            triggered = false;
        }
    }

    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * speed;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * speed;
            yield return null;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            inZone = true;
            if (levelManager.currentCheckpoint == gameObject)
            {
                _HUD_Text.text = "Can't Activate Checkpoint Again";
                StartCoroutine(Opacity());
            } 
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