#if UNITY_5_3 || UNITY_5_4_OR_NEWER
	#define RH_UNITY_FEATURE_SCENEMANAGMENT
#endif
#if UNITY_5_4_OR_NEWER || (UNITY_5 && !UNITY_5_0)
	#define RH_UNITY_FEATURE_DEBUG_ASSERT
#endif

using UnityEngine;
// Note: This script is only for the editor, but we don't put it into an Editor folder because it needs to be attached to a Monobehaviour
#if UNITY_EDITOR
using UnityEditor;
#if RH_UNITY_FEATURE_SCENEMANAGMENT
using UnityEditor.SceneManagement;
#endif

//-----------------------------------------------------------------------------
// Copyright 2017-2019 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

namespace RenderHeads.Framework.Editor
{
	/// <summary>
	/// This component is mainly used to detect when a scene is unloaded as it uses the OnDestroy() hook to save the current hierarchy state
	/// </summary>
	[ExecuteInEditMode]
	public class HierarchyNode : MonoBehaviour
	{
		private string _sceneGUID;
		private string _selectionPathList;
		private string _hierarchyPathList;
		private int editorUpdateCount;
		private bool _isHierarchyDirty;
		private static Transform[] _lastSelection;

		private void Start()
		{
			RestoreSelection();
		}

		private void OnEnable()
		{
			EditorApplication.update += OnEditorUpdate;
		}
		
		private void OnDisable()
		{
			EditorApplication.update -= OnEditorUpdate;
		}

		// When the scene closes, this node will get destroyed, at which point we save the scene state information
		private void OnDestroy()
		{
			if (HierarchyUtils.IsHierarchyWindowInPrefabMode())
			{
				return;
			}

			SaveHierarchyState(_hierarchyPathList, _sceneGUID);
			SaveSelectionState(_selectionPathList, _sceneGUID);
		}

		private void RestoreSelection()
		{
			if (HierarchyUtils.IsHierarchyWindowInPrefabMode())
			{
				return;
			}

			UpdateSceneGUID();
			if (!string.IsNullOrEmpty(_sceneGUID))
			{
				_hierarchyPathList = RestoreHierarchyState(_sceneGUID, true);
				_selectionPathList = RestoreSelectionState(_sceneGUID, true);
				//Debug.Log("hierarchy " + _hierarchyPathList);
				//Debug.Log("selection " + _selectionPathList);
				if (string.IsNullOrEmpty(_selectionPathList))
				{
					// No selection was restored, so we need to select at least one node so that the hierarchy expansion happens.
					// So just select one of the items in the hierarchyList
					string[] paths = null;
					{
						// Convert the string list to an array
						if (!string.IsNullOrEmpty(_hierarchyPathList))
						{
							paths = _hierarchyPathList.Split(new char[] { '*' }, System.StringSplitOptions.RemoveEmptyEntries);
						}

						// Select the first found expanded object
						if (paths != null && paths.Length > 0)
						{
							foreach (string path in paths)
							{
								GameObject go = GameObject.Find(path);
								if (go != null)
								{
									Selection.activeGameObject = go;
									break;
								}
							}
						}
					}
				}
				
				// Only force the hierarchy window to update if something has changed
				if (Selection.activeGameObject != null || !string.IsNullOrEmpty(_hierarchyPathList))
				{
					// Force the hierarchy window to update to the current selection
					EditorApplication.DirtyHierarchyWindowSorting();

					// NOTE: We need to use the delayCall because sometimes DirtyHierarchyWindowSorting() doesn't work the first time
					EditorApplication.delayCall += () =>
					{
						// Force the hierarchy window to update to the current selection
						EditorApplication.DirtyHierarchyWindowSorting();
					};
				}
			}
		}

		private void OnEditorUpdate()
		{
			// Editor updates are expensive, so don't do them too often as it can slow down the UI
			if (editorUpdateCount > 50 &&
				// We don't want to save hierarchy/selection when the app is playing
				!EditorApplication.isPlaying &&
				!EditorApplication.isPlayingOrWillChangePlaymode &&
				!EditorApplication.isCompiling &&
				!EditorApplication.isUpdating &&
				!HierarchyUtils.IsHierarchyWindowInPrefabMode())
			{
				editorUpdateCount = 0;
				if (HasSelectionChanged() || HasHierarchyChanged())
				{
					CacheHierarchy();
				}
			}
			editorUpdateCount++;
		}

		public void SetHierarchyDirty()
		{
			_isHierarchyDirty = true;
		}

		private bool HasHierarchyChanged()
		{
			return _isHierarchyDirty;
		}

		private static bool HasSelectionChanged()
		{
			bool result = false;
			// Ignore null selections
			if (Selection.activeGameObject != null)
			{
				if (_lastSelection == null)
				{
					result = true;
				}
				else
				{
					if (Selection.transforms.Length != _lastSelection.Length)
					{
						result = true;
					}
					else
					{
#if RH_UNITY_FEATURE_DEBUG_ASSERT
						Debug.Assert(Selection.transforms.Length == _lastSelection.Length);
#endif
						for (int i = 0; i < _lastSelection.Length; i++)
						{
							if (_lastSelection[i] != Selection.transforms[i])
							{
								result = true;
								break;
							}
						}
					}
				}

				if (result)
				{
					_lastSelection = Selection.transforms;
				}
			}
			return result;
		}

		private void CacheHierarchy()
		{
			UpdateSceneGUID();

			if (Selection.activeGameObject != null)
			{
				_selectionPathList = HierarchyUtils.GetSelectionAsString();
			}

			_hierarchyPathList = HierarchyUtils.GetHierarchyAsString();
			_isHierarchyDirty = false;
		}

		private void UpdateSceneGUID()
		{
#if RH_UNITY_FEATURE_SCENEMANAGMENT
			_sceneGUID = GetSceneGUID(EditorSceneManager.GetActiveScene().path);
#else
			_sceneGUID = GetSceneGUID(EditorApplication.currentScene);
#endif
		}

		public static string GetSceneGUID(string path)
		{
			return GetFullPath(path);
		}

		private static string GetFullPath(string relativePath)
		{
			string result = relativePath;
			if (!string.IsNullOrEmpty(result))
			{
				result = System.IO.Path.GetFullPath(result);
				result = result.Replace('\\', '/');
			}
			return result;
		}

		public static string RestoreSelectionState(string sceneId, bool logMissingNodes)
		{
			string result = EditorPrefs.GetString(GetEditorPref("Selection", sceneId), string.Empty);
			if (!HierarchyUtils.SetSelectionFromString(result, logMissingNodes))
			{
				result = string.Empty;
			}
			return result;
		}

		public static string RestoreHierarchyState(string sceneId, bool logMissingNodes)
		{
			// If it returns notset then this scene hasn't had any hierarchy state saved before
			// If it returns null/empty, then all hierarchy is collapsed
			string notset = "notset";
			string result = EditorPrefs.GetString(GetEditorPref("Hierarchy", sceneId), notset);
			if (result != notset)
			{
				HierarchyUtils.SetHierarchyFromString(result, logMissingNodes);
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		public static void SaveSelectionState(string state, string sceneId)
		{
			// No point in saving empty selection
			if (!string.IsNullOrEmpty(state))
			{
				SaveState("Selection", state, sceneId);
			}
		}

		public static void SaveHierarchyState(string state, string sceneId)
		{
			SaveState("Hierarchy", state, sceneId);
		}

		private static void SaveState(string stateName, string state, string sceneId)
		{
			//Debug.Log("SAVING: " + state + " " + GetEditorPref(stateName, sceneId));
			EditorPrefs.SetString(GetEditorPref(stateName, sceneId), state);
		}

		private static string GetEditorPref(string name, string sceneId)
		{
			return string.Format("RenderHeads-HierarchyRestore-{0}-{1}", name, sceneId);
		}
	}
}
#endif