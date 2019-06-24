using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework_Goal : MonoBehaviour
{

    public GameObject EffectToShow;
    private int numberEffect;

    private List<GameObject> effects;
    // Start is called before the first frame update
    void Start()
    {
        if(EffectToShow != null)
        {
            effects = new List<GameObject>();
            foreach(Transform g in EffectToShow.transform)
            {
                g.gameObject.SetActive(false);
                effects.Add(g.gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other) 
    {
        numberEffect = Random.Range(0, effects.Count);
        StartCoroutine(ShowEffect());
    }

    IEnumerator ShowEffect()
    {
        effects[numberEffect].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        effects[numberEffect].SetActive(false);
    }
}
