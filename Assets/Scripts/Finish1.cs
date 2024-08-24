using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finish1 : MonoBehaviour
{
    public GameObject back_go;
    Image back;
    public TMP_Text one;
    public TMP_Text two;
    public TMP_Text three;
    public GameObject btn;
    public GameObject player;
    Control cnt_loud;
    public GameObject menu;

    public int step_for_finish = 0;
    public bool flag_work = false;
    BoxCollider bcl;
    // Start is called before the first frame update
    void Start()
    {
        bcl = GetComponent<BoxCollider>();
        cnt_loud = player.GetComponent<Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flag_work == false)
        {
            if(step_for_finish == 5)
            {
                flag_work = true;
                bcl.enabled = true;
            }
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
        {
            Debug.Log("WWWWWIIIIIIIIIINNNNNNNN");
            StartCoroutine("Finish_COR");
        }
	}

    IEnumerator Finish_COR()
    {
        back = back_go.GetComponent<Image>();
        one.GetComponent<TMP_Text>();
        two.GetComponent<TMP_Text>();
        three.GetComponent<TMP_Text>();
        Color back_c = back.color;
        Color one_c = one.color;
        Color two_c = two.color;
        Color three_c = three.color;
        back_go.SetActive(true);
        menu.SetActive(false);
		while (back_c.a < 1.0f)
		{
			back_c.a += 1f * Time.deltaTime;
			back.color = back_c;
		}
        yield return new WaitForSeconds(1f);
        while (one_c.a < 1.0f)
        {
            one_c.a += 1f * Time.deltaTime;
            one.color = one_c;
        }
        yield return new WaitForSeconds(1f);
		two.text = "Cтолько раз вы испугались:" + cnt_loud.cnt;
		while (two_c.a < 1.0f)
		{
			two_c.a += 1f * Time.deltaTime;
			two.color = two_c;
		}
		yield return new WaitForSeconds(1f);

        if(cnt_loud.cnt < 5) 
        {
			three.text = "Уровень вашего стресса: Минимальный";
		}
		if (cnt_loud.cnt >= 5 && cnt_loud.cnt < 10)
		{
			three.text = "Уровень вашего стресса: Низкий";
		}
		if (cnt_loud.cnt >= 10 && cnt_loud.cnt < 15)
		{
			three.text = "Уровень вашего стресса: Средний";
		}
		if (cnt_loud.cnt >= 15)
		{
			three.text = "Уровень вашего стресса: Высокий";
		}

		while (three_c.a < 1.0f)
		{
			three_c.a += 1f * Time.deltaTime;
			three.color = three_c;
		}
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
        btn.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
	}

    public void Back_to_Menu()
    {
        SceneManager.LoadScene(0);
    }
}
