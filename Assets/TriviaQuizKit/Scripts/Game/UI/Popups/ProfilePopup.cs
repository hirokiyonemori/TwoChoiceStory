// Copyright (C) 2018 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TriviaQuizKit
{
	/// <summary>
	/// The player profile popup.
	/// </summary>
	public class ProfilePopup : Popup
	{
		public List<Sprite> AvatarSprites;

		[SerializeField]
		protected Image AvatarImage;

		[SerializeField]
		protected TextMeshProUGUI QuestionTypeText;

		[SerializeField]
		protected GameObject CategoryScrollContent;

		[SerializeField]
		protected GameObject CategoryScrollItemPrefab;

		private GameConfiguration gameConfig;

		private int selectedAvatar;
		private int selectedQuestionType;

		private List<CategoryScrollItem> categoryItems = new List<CategoryScrollItem>();

		protected override void Start()
		{
			base.Start();

			selectedAvatar = ES3.Load("player_avatar", 0);
			SetAvatar();
			SetQuestionTypeText();
			CreateCategories();
			LoadCategoryInfo();
		}

		public void OnCloseButtonPressed()
		{
			Close();
		}

		public void OnAvatarButtonPressed()
		{
			++selectedAvatar;
			if (selectedAvatar == AvatarSprites.Count)
			{
				selectedAvatar = 0;
			}
			SetAvatar();
			((HomeScreen)ParentScreen).SetAvatar(selectedAvatar);
		}

		public void OnPrevButtonPressed()
		{
			--selectedQuestionType;
			if (selectedQuestionType < 0)
			{
				selectedQuestionType = 2;
			}
			SetQuestionTypeText();
			LoadCategoryInfo();
		}

		public void OnNextButtonPressed()
		{
			++selectedQuestionType;
			if (selectedQuestionType == 3)
			{
				selectedQuestionType = 0;
			}
			SetQuestionTypeText();
			LoadCategoryInfo();
		}

		public void OnResetProgressButtonPressed()
		{
			var oldMusic = ES3.Load("music_enabled", 1);
			var oldSound = ES3.Load("sound_enabled", 1);
			var oldAvatar = ES3.Load("player_avatar", 0);
			ES3.DeleteFile();
			ES3.Save("music_enabled", oldMusic);
			ES3.Save("sound_enabled", oldSound);
			ES3.Save("player_avatar", oldAvatar);
			LoadCategoryInfo();
		}

		private void SetAvatar()
		{
			AvatarImage.sprite = AvatarSprites[selectedAvatar];
			ES3.Save("player_avatar", selectedAvatar);
		}

		private void SetQuestionTypeText()
		{
			switch (selectedQuestionType)
			{
				case 0:
					QuestionTypeText.text = "Single choice";
					break;

				case 1:
					QuestionTypeText.text = "Multiple choice";
					break;

				case 2:
					QuestionTypeText.text = "True/false";
					break;
			}
		}

		private void CreateCategories()
		{
			gameConfig = GameConfigurationLoader.LoadGameConfiguration("GameConfiguration");
			if (gameConfig != null)
			{
				foreach (var category in gameConfig.Categories)
				{
					var categoryItemGo = Instantiate(CategoryScrollItemPrefab);
					categoryItemGo.transform.SetParent(CategoryScrollContent.transform, false);

					var categoryItem = categoryItemGo.GetComponent<CategoryScrollItem>();
					categoryItem.Image.sprite = category.Sprite;
					categoryItem.NameText.text = category.Name;

					categoryItems.Add(categoryItem);
				}
			}
		}

		private void LoadCategoryInfo()
		{
			for (var i = 0; i < gameConfig.Categories.Count; i++)
			{
				var str = $"trophy_{selectedQuestionType}_{i}";
				var pref = ES3.Load(str, 0);
				categoryItems[i].CupImage.sprite = categoryItems[i].CupSprites[pref];
				str = $"score_{selectedQuestionType}_{i}";
				var highScore = ES3.Load(str, 0);
				categoryItems[i].HighScoreText.text = highScore.ToString();
				if (highScore == 0)
				{
					categoryItems[i].HighScoreText.color = categoryItems[i].NoHighScoreTextColor;
				}
				else
				{
					categoryItems[i].HighScoreText.color = categoryItems[i].HighScoreTextColor;
				}
			}
		}
	}
}
