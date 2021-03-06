﻿using UnityEngine;
using CallbackEvents;
using TMPro;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    // editor fields
    [SerializeField] private int levelNumber = 1; //this should equal to the scene number
    [SerializeField] private TMP_Text levelLabel;
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite filledStar;

    // score
    private int score;

    void Start() {
        score = -1;

        string key = $"level{levelNumber}.score";
        if (PlayerPrefs.HasKey(key))
            score = PlayerPrefs.GetInt(key);

        // test if beat level before this
        if (levelNumber > 1 && !PlayerPrefs.HasKey($"level{levelNumber-1}.score")) {
            GetComponent<Button>().interactable = false;
        }
        
        levelLabel.text = $"{levelNumber}";

        if (score > -1) {
            scoreLabel.text = $"{score}";

            for(int i=0; i < stars.Length; i++) {
                if (score > LevelData.STAR_SCORES[levelNumber, i]) {
                    stars[i].sprite = filledStar;
                }
            }
        }else{
            scoreLabel.text = "-";
        }
    }

    public void OnClick() {
        EventSystem.Current.FireEvent(new SwitchSceneContext(levelNumber));
    }
}
