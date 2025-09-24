// Copyright (C) 2018 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace TriviaQuizKit
{
	/// <summary>
	/// Initialization utility component.
	/// </summary>
	public class Initialization : MonoBehaviour
	{
		private void Awake()
		{
			if (!ES3.KeyExists("sound_enabled"))
			{
				ES3.Save("sound_enabled", 1);
			}
			if (!ES3.KeyExists("music_enabled"))
			{
				ES3.Save("music_enabled", 1);
			}
		}
	}
}
