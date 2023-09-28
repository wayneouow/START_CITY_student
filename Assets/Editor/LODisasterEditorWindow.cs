using UnityEngine;
using UnityEditor;
using LO.Meta;
using System.Collections.Generic;

public class LODisasterEditorWindow : EditorWindow
{
	private string objectNamePrefix = "disaster-";
	private string groupNamePrefix = "disaster-group";
	private string fileExtension = ".asset";
	private string pathName = "Assets/AddressableResource/Meta/Disaster/";
	private string groupPathName = "Assets/AddressableResource/Meta/Disaster/DisasterGroup/";

	private int positionStart = 0, positionStep = 1, positionEnd = 15;
	private int damageStart = 30, damageStep = 10, damageEnd = 50;

	private int areaCol = 4, areaRow = 4;
	private int groupCol = 2, groupRow = 2;
	private int groupDamage = 30;

	[MenuItem("City Game/Disaster Window")]
	static void Init()
	{
		var window = GetWindow<LODisasterEditorWindow>();
		window.titleContent = new GUIContent("LODisasterWindow");
		window.Show();
	}

	private void OnGUI()
	{
		GUILayout.Label("Disasters Creator", EditorStyles.boldLabel);

		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Path Name:");
		pathName = EditorGUILayout.TextField(pathName);

		EditorGUILayout.LabelField("Object Name Prefix:");
		objectNamePrefix = EditorGUILayout.TextField(objectNamePrefix);


		EditorGUILayout.LabelField("File Extension:");
		fileExtension = EditorGUILayout.TextField(fileExtension);

		EditorGUILayout.LabelField("Field Values:");

		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.LabelField("Position start:", GUILayout.Width(50));
		positionStart = int.Parse(EditorGUILayout.TextField(positionStart.ToString()));

		EditorGUILayout.LabelField("Position step:", GUILayout.Width(50));
		positionStep = int.Parse(EditorGUILayout.TextField(positionStep.ToString()));

		EditorGUILayout.LabelField("Position end:", GUILayout.Width(50));
		positionEnd = int.Parse(EditorGUILayout.TextField(positionEnd.ToString()));

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.LabelField("Damage start:", GUILayout.Width(50));
		damageStart = int.Parse(EditorGUILayout.TextField(damageStart.ToString()));

		EditorGUILayout.LabelField("Damage step:", GUILayout.Width(50));
		damageStep = int.Parse(EditorGUILayout.TextField(damageStep.ToString()));

		EditorGUILayout.LabelField("Damage end:", GUILayout.Width(50));
		damageEnd = int.Parse(EditorGUILayout.TextField(damageEnd.ToString()));

		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Create Disasters"))
		{
			CreateDisasters();
		}

		EditorGUILayout.Space(25);

		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.LabelField("area columns:", GUILayout.Width(80));
		areaCol = int.Parse(EditorGUILayout.TextField(areaCol.ToString(), GUILayout.Width(25)));

		EditorGUILayout.LabelField("area rows:", GUILayout.Width(80));
		areaRow = int.Parse(EditorGUILayout.TextField(areaRow.ToString(), GUILayout.Width(25)));

		EditorGUILayout.LabelField("group columns:", GUILayout.Width(80));
		groupCol = int.Parse(EditorGUILayout.TextField(groupCol.ToString(), GUILayout.Width(25)));

		EditorGUILayout.LabelField("group rows:", GUILayout.Width(80));
		groupRow = int.Parse(EditorGUILayout.TextField(groupRow.ToString(), GUILayout.Width(25)));

		EditorGUILayout.LabelField("group damage:", GUILayout.Width(90));
		groupDamage = int.Parse(EditorGUILayout.TextField(groupDamage.ToString(), GUILayout.Width(25)));

		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Create Disaster Group"))
		{
			CreateDisasterGroup();
		}
	}

	void CreateDisasters()
	{
		int count = 0;
		int totalCount = ((positionEnd - positionStart) / positionStep + 1) * ((damageEnd - damageStart) / damageStep + 1);
		string title = "Creating ScriptableObjects...";
		string message = "Creating ScriptableObject {0}/{1}...";

		for (int p = positionStart; p <= positionEnd; p += positionStep)
		{
			for (int d = damageStart; d <= damageEnd; d += damageStep)
			{
				LODisasterMeta disasterMeta = LODisasterMeta.CreateInstance<LODisasterMeta>();

				disasterMeta.Position = p;
				disasterMeta.Damage = d;

				string objectName = objectNamePrefix + p + "-" + d;
				string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(pathName + objectName + fileExtension);

				AssetDatabase.CreateAsset(disasterMeta, assetPathAndName);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();

				float progress = (float)count / totalCount;
				string progressMessage = string.Format(message, count + 1, totalCount);
				EditorUtility.DisplayProgressBar(title, progressMessage, progress);
				count += 1;
			}
		}

		EditorUtility.ClearProgressBar();
		EditorUtility.DisplayDialog("Disaster Creator", "Disaster Created!", "OK");
	}

	void CreateDisasterGroup()
	{
		int count = 0;
		int totalCount = (areaCol - groupCol + 1) * (areaRow - groupRow + 1);
		string title = "Creating ScriptableObjects...";
		string message = "Creating ScriptableObject {0}/{1}...";

		for (int i = 0; i < areaRow - groupRow + 1; i++)
		{
			for (int j = 0; j < areaCol - groupCol + 1; j++)
			{
				LODisasterGroupMeta disasterGroupMeta = LODisasterGroupMeta.CreateInstance<LODisasterGroupMeta>();

				disasterGroupMeta.DisasterList = new List<LODisasterMeta>();

				for (int gi = 0; gi < groupRow; gi++)
				{
					for (int gj = 0; gj < groupCol; gj++)
					{
						var position = (i * areaCol + j) + (gi * areaCol + gj);
						var objectName = objectNamePrefix + position + "-" + groupDamage;

						disasterGroupMeta.DisasterList.Add(AssetDatabase.LoadAssetAtPath<LODisasterMeta>(pathName + objectName + fileExtension));
					}
				}

				string groupObjectName = $"{groupNamePrefix}-{groupRow}x{groupCol}in{areaRow}x{areaCol}-d{groupDamage}-{count}";

				Debug.Log($"groupPathName: {groupPathName}, groupObjectName: {groupObjectName}");

				string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(groupPathName + groupObjectName + fileExtension);

				AssetDatabase.CreateAsset(disasterGroupMeta, assetPathAndName);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();

				float progress = (float)count / totalCount;
				string progressMessage = string.Format(message, count + 1, totalCount);
				EditorUtility.DisplayProgressBar(title, progressMessage, progress);
				count += 1;
			}
		}

		EditorUtility.ClearProgressBar();
		EditorUtility.DisplayDialog("Disaster Creator", "Disaster Created!", "OK");
	}
}