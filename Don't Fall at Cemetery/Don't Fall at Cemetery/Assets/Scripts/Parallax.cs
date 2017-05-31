using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parallax for environment
/// </summary>
public class Parallax : MonoBehaviour
{
    public  float       speed = 2;

    private float       pos;
    private Transform[] layers;

    /// <summary>
    /// gets all children in parallax gameobject transform when started
    /// </summary>
    void Start ()
    {
        layers = getLayers(transform);
    }

    /// <summary>
    /// gets all children in parallax gameobject transform,
    /// basically children are treated as layers
    /// </summary>
    private Transform[] getLayers(Transform tr)
    {
        Transform[] arr = new Transform[transform.childCount];

        for (int num = 0; num < transform.childCount; num++)
        {
            arr[num] = transform.GetChild(num);
        }

        return arr;
    }

    /// <summary>
    /// updates layer positions by private pos variable
    /// </summary>
    private void updateParallaxPosition()
    {
        for (int num = 0; num < layers.Length; num++)
        {
            layers[num].localPosition = new Vector3(pos * (num + 1), 0);
        }
    }

    /// <summary>
    /// lerps parallax to given position
    /// </summary>
    public void updateParalax(float position)
    {
        pos = Mathf.Lerp(pos, -position,Time.deltaTime * speed);
        updateParallaxPosition();
	}

    /// <summary>
    /// resets position and updates layers to starting position
    /// </summary>
    public void reset()
    {
        pos = 0;
        updateParallaxPosition();
    }
}
