using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the goal_1 (firework when triggered)
/// </summary>
public class Goal_Firework : MonoBehaviour
{
    #region attribute
    public GameObject EffectToShow;
    private int numberEffect;

    /// <summary> list of effect </summary>
    private List<GameObject> effects;
    #endregion

    #region monobehaviour

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
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

    /// <summary>
    /// Set firework that will be actived
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) 
    {
        numberEffect = Random.Range(0, effects.Count);
        StartCoroutine(ShowEffect());
    }
    #endregion

    #region coroutine
    /// <summary>
    /// Active one firework when trigger
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowEffect()
    {
        effects[numberEffect].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        effects[numberEffect].SetActive(false);
    }
    #endregion
}
