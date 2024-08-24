using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Dynamica : MonoBehaviour
{
    Light _light;
    Control script_cnt;
    [SerializeField] private GameObject control;

    private float start_intensity;

    public float MIN_intensity = 0.3f;
    public float MAX_intensity = 1.5f;
    public float noise_speed = 0.15f;
    public bool flicker_ON;
    public bool random_timer;
    public float random_timer_value_MIN = 5.1f;
	public float random_timer_value_MAX = 20.1f;
    public float random_timer_value;
    public bool is_work = false;
    public int local_cnt;


	// Start is called before the first frame update
	void Start()
    {
        _light= GetComponent<Light>();
        script_cnt = control.GetComponent<Control>();
        start_intensity = _light.intensity;
        local_cnt = script_cnt.cnt;
    }

    // Update is called once per frame
    void Update()
    {
        if(local_cnt != script_cnt.cnt)
        {
            StartCoroutine("Update_Options_COR");
            local_cnt = script_cnt.cnt;
        }

        if (script_cnt.cnt > 0 && is_work == false)
        {
            StartCoroutine("Start_Flicker_COR");
            is_work= true;
        }
        if (flicker_ON)
        {
			_light.intensity = Mathf.Lerp(MIN_intensity, MAX_intensity, Mathf.PerlinNoise(10, Time.time / noise_speed));
		}
    }

    IEnumerator Start_Flicker_COR()
    {
        while (random_timer)
        {
            random_timer_value = UnityEngine.Random.Range(random_timer_value_MIN, random_timer_value_MAX);
            yield return new WaitForSeconds(random_timer_value);
            if (flicker_ON)
            {
                _light.intensity = start_intensity;
                flicker_ON = false;
            }
            else
            {
                flicker_ON = true;
            }
        }
    }

    IEnumerator Update_Options_COR()
    {
        if(noise_speed > 0.1f)
        {
			noise_speed -= 0.2f;
		}
        if(random_timer_value_MIN > 0.1f)
        {
            random_timer_value_MIN -= 0.5f;
        }
        if(random_timer_value_MAX > 0.1f)
        {
            random_timer_value_MAX -= 0.5f;
        }

        yield return null;
    }
}
