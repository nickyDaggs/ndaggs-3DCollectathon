using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StringList
{
    public List<string> list;
}

[System.Serializable]
public class Attack
{
    public GameObject pro;
    public bool inside;
    public bool timed;
    public int times;
    public float timeDeath;
}

public class bossScript : MonoBehaviour
{
    public List<StringList> bossDialogue;
    public SignDialogue sign;
    int curPhase = 0;
    public AudioSource Audio;
    public List<int> phaseTimes;
    public Animator animator;
    public float time = 0;
    public float proTime = .5f;
    bool attack = false;
    public List<Attack> attacks;
    public Attack dancer;
    public List<Transform> insideSpawns;
    public List<Transform> outsideSpawns;
    bool themeP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time > time && attack)
        {
            animator.SetTrigger("Damage");
            attack = false;
        }
        
    }

    public void Damage()
    {
        if(!themeP)
        {
            Audio.Play();
            themeP = true;
        }
        foreach(Transform pos in insideSpawns)
        {
            foreach(Animator anim in pos.GetComponentsInChildren<Animator>())
            {
                anim.SetTrigger("bie");
            }
        }
        
        if(curPhase > 0)
        {
            foreach (Attack pro in attacks)
            {
                pro.times = pro.times + 10;
            }
        }
        time = Time.time + phaseTimes[curPhase];
        curPhase++;
        if (curPhase >= bossDialogue.Count)
        {
            
            SceneManager.LoadScene(6);
        } else
        {
            animator.SetTrigger("Damage");
            sign.Lines = bossDialogue[curPhase].list;
            attack = true;
        }
        StartCoroutine(projectiles(1f));
    }
    IEnumerator projectiles(float time)
    {
        yield return new WaitForSeconds(5f);
        for (int i = attacks.Count - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            Attack currentCon = attacks[i];
            Attack conToSwap = attacks[swapIndex];
            attacks[i] = conToSwap;
            attacks[swapIndex] = currentCon;
        }
        List<Transform> spawnsD = new List<Transform>(insideSpawns);
        /*for (int j = 0; j < dancer.times; j++)
        {
            int spawn = Random.Range(0, spawnsD.Count);
            Instantiate(dancer.pro, spawnsD[spawn]);
            spawnsD.Remove(spawnsD[spawn]);
            yield return new WaitForSeconds(1f);
        }*/

        for (int i = 0; i < attacks.Count; i++)
        {
            List<Transform> spawns;
            if(attacks[i].inside)
            {
                spawns = new List<Transform>(insideSpawns);
            } else
            {
                spawns = new List<Transform>(outsideSpawns);
            }
            for (int j = 0; j < attacks[i].times; j++)
            {
                int spawn = Random.Range(0, spawns.Count - 1);
                Transform Spawn = spawns[spawn];
                Instantiate(attacks[i].pro, Spawn);
                spawns.Remove(Spawn);
                spawns.Add(Spawn);
                yield return new WaitForSeconds(proTime);
            }
            
        }
        

    }
}
