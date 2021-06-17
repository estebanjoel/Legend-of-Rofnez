using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;
    [SerializeField] float forwardBound;
    [SerializeField] float backBound;

    public void SetBounds(float left, float right, float forward, float back)
    {
        leftBound = left;
        rightBound = right;
        forwardBound = forward;
        backBound = back;
    }

    // Devuelve un array con los cuatro bounds sabiendo que se ordena: izquierda, derecha, arriba y abajo
    public float[] GetBounds()
    {
        float[] bounds = new float[4];
        bounds[0] = leftBound;
        bounds[1] = rightBound;
        bounds[2] = forwardBound;
        bounds[3] = backBound;
        return bounds;
    }
}
