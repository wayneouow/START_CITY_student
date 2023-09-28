using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace LO.Editor
{

	public class LOSceneEditorWindow : EditorWindow
	{

		string[] m_SceneLabels = new string[] { "Preload Scene", "Main Scene", "Game Scene", "Demo Scene" };
		string[] m_SceneNames = new string[] { "preload-scene", "main-scene", "game-scene", "demo-scene" };

		[MenuItem("City Game/Scene Window")]
		static void Init()
		{
			// Get existing open window or if none, make a new one:
			LOSceneEditorWindow window = (LOSceneEditorWindow)EditorWindow.GetWindow(typeof(LOSceneEditorWindow));
			window.Show();
		}

		private void OnGUI()
		{

			GUILayout.BeginVertical();

			for (int i = 0; i < m_SceneLabels.Length; i++)
			{

				GUILayout.Space(5);
				SceneButton(m_SceneLabels[i], m_SceneNames[i]);
			}

			GUILayout.EndVertical();
		}

		void SceneButton(string label, string sceneName)
		{

			if (GUILayout.Button(label, GUILayout.Height(30)))
			{

				// auto save current scene
				EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), "");
				EditorSceneManager.OpenScene($"Assets/Scenes/{sceneName}.unity");
			}
		}
	}
}