using UnityEngine;

public class ArrowFlash : MonoBehaviour
{
    public float timer = 0f;
    public float timer_1 = 0f;

    public GameObject arrow;
    public GameObject arrow_1;
    public GameObject arrow_2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            arrow.SetActive(false);
            arrow_1.SetActive(false);
            arrow_2.SetActive(false);
            timer = 1f;
        }
        else
        {
            timer_1 -= Time.deltaTime;
            if (timer_1 <= 0f)
            {
                arrow.SetActive(true);
                arrow_1.SetActive(true);
                arrow_2.SetActive(true);
                timer_1 = 1f;
            }
        }

    }
}
