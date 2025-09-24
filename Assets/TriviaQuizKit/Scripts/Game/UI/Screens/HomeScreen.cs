// Copyright (C) 2018 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TriviaQuizKit
{
	/// <summary>
	/// The home screen.
	/// </summary>
	public class HomeScreen : BaseScreen
	{
		public List<Sprite> AvatarSprites;

		[SerializeField]
		protected Image AvatarImage;

		private void Start()
		{
			var selectedAvatar = ES3.Load("player_avatar", 0);
			SetAvatar(selectedAvatar);
		}

		public void OnAvatarButtonPressed()
		{
			OpenPopup<ProfilePopup>("Popups/ProfilePopup");
		}

		public void OnSettingsButtonPressed()
		{
			OpenPopup<SettingsPopup>("Popups/SettingsPopup", popup => { });
		}

		public void SetAvatar(int avatar)
		{
			AvatarImage.sprite = AvatarSprites[avatar];
		}

		public void OnPlayButtonPressed()
		{
			// 固定設定: TrueFalse問題タイプと時間制限なしを設定
			ES3.Save("question_type", 2); // TrueFalse
			ES3.Save("time_mode", 1);     // Unlimited

			// 直接カテゴリー選択画面に遷移
			SceneManager.LoadScene("CategorySelection");
		}
	}
}
