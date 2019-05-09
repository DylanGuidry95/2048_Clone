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

    public GameObject MergerParticle;

    List<ParticleTimers> Particles;

    public float MaxLife;

    // Start is called before the first frame update
    void Start()
    {
        Particles = new List<ParticleTimers>();
        Events.MergedCells.AddListener(SpawnMergerParticle);
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

    void SpawnMergerParticle(Vector3 position)
    {
        var newPartilce = GameObject.Instantiate(MergerParticle, position, Quaternion.identity);
        Particles.Add(new ParticleTimers(newPartilce, 0));
    }
}
