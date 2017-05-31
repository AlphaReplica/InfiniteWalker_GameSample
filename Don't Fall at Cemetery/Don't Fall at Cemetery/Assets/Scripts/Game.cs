using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game Class where game update is happeing
/// </summary>
public class Game : MonoBehaviour
{
    public Parallax  environment;
    public Character character;
    public Button    playBtn;
    public Text      label;
    
    void Start ()
    {
        // Registering events in start
        playBtn.onClick.AddListener(onPlayClicked);
        character.onCharacterDeath = onCharacterDeath;
        character.gameObject.SetActive(false);

    }

    /// <summary>
    /// Called when UI Play Button gets clicked
    /// </summary>
    private void onPlayClicked()
    {
        character.reset();
        environment.reset();
        playBtn.gameObject.SetActive(false);
        character.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called when Game character dies
    /// </summary>
    private void onCharacterDeath()
    {
        playBtn.gameObject.SetActive(true);
    }

    /// <summary>
    /// game update cycle, when touched or clicked on button character makes step
    /// character and environment gets updated
    /// </summary>
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            character.makeStep();
        }
        character.updateCharacter();
        environment.updateParalax(character.stepsMoved * 0.3f);

        label.text = "There is Nothing More, Go Home!\n\n Uh and BTW You walked " + character.stepsMoved.ToString() + " steps";
    }
}
