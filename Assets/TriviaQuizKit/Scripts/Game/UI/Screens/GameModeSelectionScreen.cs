// Copyright (C) 2018 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TriviaQuizKit
{
    /// <summary>
    /// The screen where the player can select the game mode to play.
    /// </summary>
    public class GameModeSelectionScreen : MonoBehaviour
    {
        public ToggleButtonGroup QuestionTypeToggleGroup;
        public ToggleButtonGroup TimeModeToggleGroup;

        private QuestionType selectedQuestionType;
        private TimeMode selectedTimeMode;

        private void Start()
        {
            // 固定設定: TrueFalse問題タイプと時間制限なし
            SetQuestionType(2); // TrueFalse
            SetTimeMode(1);     // Unlimited
            QuestionTypeToggleGroup.SetToggle((int)selectedQuestionType);
            TimeModeToggleGroup.SetToggle((int)selectedTimeMode);
        }

        private void SetQuestionType(int type)
        {
            selectedQuestionType = (QuestionType)type;
            ES3.Save("question_type", (int)selectedQuestionType);
        }

        private void SetTimeMode(int mode)
        {
            selectedTimeMode = (TimeMode)mode;
            ES3.Save("time_mode", (int)selectedTimeMode);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("CategorySelection");
        }
    }
}
