using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SimSinos
{
    public class Character : MonoBehaviour
    {
        private Animator anim;
        private NavMeshAgent agent;

        public int totalEnergy = 1000;
        public int totalFood = 1000;
        public int totalHappiness = 1000;

        private int energy;
        private int food;
        private int happiness;

        private float timer = 0.0f;
        private float waitTime = 2.0f;

		private GUIStyle style;
		private GUIStyle styleAlert;

        public Transform HungryTarget;
        public Transform IdleTarget;
        public Transform SadTarget;
        public Transform SleepyTarget;

        private void Start()
        {
            energy = totalEnergy;
            food = totalFood;
            happiness = totalHappiness;

            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

			style = new GUIStyle();
			style.normal.textColor = Color.black;
			styleAlert = new GUIStyle();
			styleAlert.normal.textColor = Color.red;
        }

        void Update()
        {
            timer += Time.deltaTime;

            // Check if we have reached beyond 2 seconds.
            // Subtracting two is more accurate over time than resetting to zero.
            if (timer > waitTime)
            {
                updateEnergy();
                updateFood();
                updateHappiness();

                anim.SetBool("Needy", (anim.GetBool("Hungry") || anim.GetBool("Sleepy") || anim.GetBool("Sad")));

                timer = timer - waitTime;
            }
 
            anim.SetFloat ("VelocityX", Input.GetAxis ("Vertical"));
            anim.SetFloat ("VelocityZ", Input.GetAxis ("Horizontal"));
        }

        void OnGUI()
        {
			if (anim.GetBool("Sleepy"))
			{
				GUI.Label(new Rect(10, 10, 100, 20), "Energy: " + energy, styleAlert);
			}
			else
			{
				GUI.Label(new Rect(10, 10, 100, 20), "Energy: " + energy, style);
			}

			if (anim.GetBool("Hungry"))
			{
				GUI.Label(new Rect(10, 40, 100, 20), "Food: " + food, styleAlert);
			}
			else
			{
				GUI.Label(new Rect(10, 40, 100, 20), "Food: " + food, style);
			}

			if (anim.GetBool("Sad"))
			{
				GUI.Label(new Rect(10, 25, 100, 20), "Happiness: " + happiness, styleAlert);
			}
			else
			{
				GUI.Label(new Rect(10, 25, 100, 20), "Happiness: " + happiness, style);
			}

			if (anim.GetBool("Starving")) GUI.Label(new Rect(10, 100, 100, 20), "Starving", styleAlert);
        }

        public void chooseTarget(Transform t)
        {
            if (t)
                agent.SetDestination(t.position);
        }

        public bool hasArrived()
        {
            return agent.remainingDistance == 0;
        }

        public void updateEnergy()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sleeping"))
            {
                energy += 70;

                if (energy > (totalEnergy * 0.8f) && anim.GetBool("Sleepy"))
                {
                    anim.SetBool("Sleepy", false);
                    Debug.Log("sem sono");
                }
            }
            else
            {
                energy -= 30;

                if (energy < (totalEnergy * 0.6f) && !anim.GetBool("Sleepy"))
                {
                    anim.SetBool("Sleepy", true);
                    Debug.Log("sono");
                }
            }
        }

        public void updateFood()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Eating"))
            {
                food += 70;

                if (food > (totalFood * 0.8f) && anim.GetBool("Hungry"))
                {
                    anim.SetBool("Hungry", false);
                    Debug.Log("sem fome");
                }
                if (food > (totalFood * 0.3f) && anim.GetBool("Starving"))
                {
                    anim.SetBool("Starving", false);
                    Debug.Log("sobreviveu");
                }
            }
            else
            {
                food -= 40;

                if (food < (totalFood * 0.6f) && !anim.GetBool("Hungry"))
                {
                    anim.SetBool("Hungry", true);
                    Debug.Log("com fome");
                }
                if (food < (totalFood * 0.4f) && !anim.GetBool("Starving"))
                {
                    anim.SetBool("Starving", true);
                    Debug.Log("faminto");
                }
            }
        }

        public void updateHappiness()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Playing"))
            {
                happiness += 100;

                if (happiness > (totalHappiness*0.8f) && anim.GetBool("Sad"))
                {
                    anim.SetBool("Sad", false);
                    Debug.Log("feliz");
                }
            }
            else
            {
                happiness -= 50;

                if (happiness < (totalHappiness * 0.6f) && !anim.GetBool("Sad"))
                {
                    anim.SetBool("Sad", true);
                    Debug.Log("triste");
                }
            }
        }
    }
}
