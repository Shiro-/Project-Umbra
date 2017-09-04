using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSys : MonoBehaviour
{
    public Light flash;

    public float minTimeToStrike;
    public float maxTimeToStrike;

    public float minFlashTime;
    public float maxFlashTime;

    [Tooltip("X Rotation: Affects length of shadows on ground. Recommended range 5-45.")]
    public float minX;
    [Tooltip("X Rotation: Affects length of shadows on ground. Recommended range 5-45.")]
    public float maxX;

    [Tooltip("Y Rotation: Affects directions flash will come from. Range of 0 to 360 unless you want to limit where the light will come from.")]
    public float minY;
    [Tooltip("Y Rotation: Affects directions flash will come from. Range of 0 to 360 unless you want to limit where the light will come from.")]
    public float maxY;

    private float rotX;
    private float rotY;

    private float strikeT;
    private float lastTime;
    private float onTime;

    private Transform t;

	// Use this for initialization
	void Start ()
    {
        flash = GetComponentInChildren<Light>();
        flash.enabled = false;
        t = GetComponent<Transform>();
        lastTime = 0;
        onTime = 0;
        StrikeSet();
	}

    void FixedUpdate()
    {
        if (Time.time - lastTime > strikeT && flash.enabled == false)
        {
            flash.enabled = true;
            lastTime = Time.time;
        }
        else if (flash.enabled == true && Time.time - lastTime > onTime)
        {
            flash.enabled = false;
            StrikeSet();
        }
       
    }

    void StrikeSet()
    {
        strikeT = Random.Range(minTimeToStrike, maxTimeToStrike);
        onTime = Random.Range(minFlashTime, maxFlashTime);
        rotX = Random.Range(minX, maxX);
        rotY = Random.Range(minY, maxY);
        t.SetPositionAndRotation(t.position, Quaternion.Euler(rotX, rotY, 0));
    }
}
