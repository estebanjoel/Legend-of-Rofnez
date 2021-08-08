using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class CursorSettings : MonoBehaviour
    {
        [SerializeField] Texture2D defaultCursor;

        public void SetDefaultCursor()
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
