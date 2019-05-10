using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public class ParticleTimers
    {
        public GameObject ParticleObject;
        public float CurrentLife;

        public ParticleTimers(GameObject obj, float timer)
        {
            ParticleObject = obj; CurrentLife = timer;
        }
    }    

    List<ParticleTimers> Particles;

    public float MaxLife;

    public List<int> Values;
    public List<GameObject> ParticlePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        Particles = new List<ParticleTimers>();
        Events.MergedCells.AddListener(SpawnMergerParticle);
        int val = 2;
        while(val != 2048)
        {
            Values.Add(val);
            val += val;
        }
    }

    private void Update()
    {
        if(Particles.Count > 0)
        {
            for(int i = 0; i < Particles.Count; i++)
            {
                Particles[i].CurrentLife += Time.deltaTime;
                if(Particles[i].CurrentLife > MaxLife)
                {
                    Destroy(Particles[i].ParticleObject);
                    Particles.RemoveAt(i);
                }
            }
        }
    }

    void SpawnMergerParticle(Vector3 position, int value)
    {
        if(!IsShaking)
            StartCoroutine(GridShake());
        
        foreach(var val in Values)
        {
            if(value == val)
            {
                var mergeParticle = ParticlePrefabs[Values.IndexOf(val)];
                var newPartilce = Instantiate(mergeParticle, position, Quaternion.identity);
                Particles.Add(new ParticleTimers(newPartilce, 0));
                break;
            }
        }        
    }

    bool IsShaking = false;

    IEnumerator GridShake()
    {
        var obj = FindObjectOfType<GridBehaviour>();
        IsShaking = true;
        var initialPosition = obj.transform.position;
        for (int i = 0; i < 2; i++)
        {
            obj.transform.position += new Vector3(0.1f, 0.1f, 0);
            yield return new WaitForSeconds(0.01f);
            obj.transform.position = initialPosition;
            yield return new WaitForSeconds(0.01f);
            obj.transform.position += new Vector3(-0.1f, -0.1f, 0);
            yield return new WaitForSeconds(0.1f);
        }
        obj.transform.position = initialPosition;
        IsShaking = false;
    }
}
