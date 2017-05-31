using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform body;
    public Transform left_leg;
    public Transform right_leg;
    public Animator  anim;
    public float     leg_speed = 10;
    public float     body_angle = 30;
    public Action    onCharacterDeath;

    private bool     isStanding = false;
    private Vector3  startPosition;
    private float    right_step;
    private float    left_step;
    private float    body_rotation;
    private int      steps;

    /// <summary>
    /// when started character placement position saved
    /// </summary>
    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    /// <summary>
    /// helper method for shortening code, basically it rotates model to desired angle with given speed
    /// </summary>
    private void move(Transform leg, float to, float speed)
    {
        leg.localRotation = Quaternion.Lerp(leg.localRotation, Quaternion.Euler(0, 0, to), Time.deltaTime * speed);
    }

    /// <summary>
    /// when character makes step it's y axis moves down
    /// </summary>
    private void moveCharacterToY()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(startPosition.x,
                                                                                    startPosition.y - (steps * 0.1f),
                                                                                    startPosition.z), Time.deltaTime * body_angle);
    }

    /// <summary>
    /// method where character data is updated,
    /// checks and updates character body rotation and notifies listener if character gets killed
    /// updates legs rotation and randomly sets facial expression
    /// </summary>
    public void updateCharacter()
    {
        if (Mathf.Abs(body_rotation) >= 40)
        {
            isStanding = false;
            move(body, 0, body_angle);
            move(transform, (body_rotation > 0) ? 90 : -90, body_angle);
            anim.SetBool("isDead", true);

            if(onCharacterDeath!=null)
            {
                onCharacterDeath.Invoke();
            }
        }

        if (!isStanding)
        {
            return;
        }
        move(right_leg, right_step   , leg_speed);
        move(left_leg , left_step    , leg_speed);
        move(body     , body_rotation, body_angle);
        moveCharacterToY();

        body_rotation -= Time.deltaTime * (body_angle * 2);
        anim.SetInteger("variation", UnityEngine.Random.Range(0, 3));
    }

    /// <summary>
    /// called when when we want to move character
    /// basically it's changing variables
    /// </summary>
    public void makeStep()
    {
        if (!isStanding)
        {
            return;
        }

        float min  = -10.0f;
        float max  = 30.0f;
        left_step  = (left_step == min || left_step == 0) ? max : min;
        right_step = (left_step == max) ? min : max;

        body_rotation += body_angle;
        steps++;
    }

    /// <summary>
    /// called when we want to reset character
    /// </summary>
    public void reset()
    {
        anim.SetBool   ("isDead"   , false);
        anim.SetInteger("variation", 0);
        steps         = 0;
        left_step     = 0;
        right_step    = 0;
        body_rotation = 0;
        isStanding    = true;

        transform.localPosition    = startPosition;
        right_leg.localEulerAngles = Vector3.zero;
        left_leg .localEulerAngles = Vector3.zero;
        body     .localEulerAngles = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    public int stepsMoved
    {
        get
        {
            return steps;
        }
    }

    public bool isDead
    {
        get
        {
            return !isStanding;
        }
    }
}