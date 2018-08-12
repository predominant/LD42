using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD42
{
	public class LevelSwitcher : MonoBehaviour
	{
		public void SwitchLevel(string name)
		{
			SceneManager.LoadScene(name);
		}

		public void QuitGame()
		{
			Application.Quit();
		}
	}
}