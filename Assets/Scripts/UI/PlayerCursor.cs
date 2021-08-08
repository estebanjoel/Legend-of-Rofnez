using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class PlayerCursor : MonoBehaviour
    {
        [SerializeField] Texture2D defaultCursor;
        [SerializeField] Texture2D moveCursor;
        [SerializeField] Texture2D fightCursor;

        public void SetDefaultCursor()
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        public void SetMoveCursor()
        {
            Cursor.SetCursor(moveCursor, Vector2.zero, CursorMode.ForceSoftware);
        }

        public void SetFightCursor()
        {
            Cursor.SetCursor(fightCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
