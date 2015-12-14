using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Creature : MonoBehaviour
{
    enum State
    {
        idle,
        walking,
        eating
    }

    State curState = State.idle;

    public Animator anim;

    List<BaitController> listOfBait;

    public float moveSpeed;

    NavMeshAgent navigator;

    NavMeshPath[] paths;

    int numberOfActiveBaits = -1;

    BaitController target;

    float stunTime;

    Renderer _renderer;

    Color originalColor;

    Color stunColor;

    public GameObject stunParticles;
    //these are the bounds the creatures can move within
    public Vector2 creatureBoundsX = new Vector2(-1, 1);
    public Vector2 creatureBoundsZ = new Vector2(-1, 1);

    public float idleTimer;

    private float idleTimerCounter;

    private bool gotCreaturePos;

    public float waypointSoftEdge = 0.1f;
    Vector3 waypoint;

    void Start()
    {
        listOfBait = new List<BaitController>();

        navigator = GetComponent<NavMeshAgent>();
        PlayerController.Instance.creatureList.Add(this);
        _renderer = GetComponent<Renderer>();
        stunColor = Color.red;
        originalColor = _renderer.material.color;

        if (stunParticles == null)
        {
            Debug.LogWarning("There is no game object set for the stun effect.");
            stunParticles = new GameObject();
            stunParticles.name = "NoAttachedGameObjectForParticleSystem (Creature)";
        }
    }

    public void AddBait(BaitController bait)
    {
        listOfBait.Add(bait);
        FindClosestPath();
    }

    public void RemoveBait(BaitController bait)
    {
        listOfBait.Remove(bait);
        FindClosestPath();
    }

    void SetState()
    {
        anim.SetInteger("State", (int)curState);
        Debug.Log((int)curState);
    }

    public void Stun(float _stunTime)
    {

        stunParticles.SetActive(true);

        //apply the stun time
        stunTime = _stunTime;

        navigator.Stop();

        _renderer.material.color = stunColor;
    }

    bool IsBeingLit(Light light)
    {
        //store light's position
        Vector3 lightPos = light.transform.position;
        //store the light's forward direction
        Vector3 lightDir = light.transform.forward;
        //store the difference in position of the camera and this transforms'
        Vector3 lightToThisDif = transform.position - lightPos;
        //get the dot product of this and the light's facing direction
        float dotProduct = Vector3.Dot(lightDir.normalized, lightToThisDif.normalized);
        //change the dotproduct to an angle
        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        if (gameObject.name == "Creature")
        {
       //     Debug.Log(angle);
            //     Debug.Log(dotProduct);
        }
        //check if the angle is less than the angle the light shines at
        if (angle < light.spotAngle)
            return true;
        return false;
    }

    void FindRoamingPosition()
    {

        if (!gotCreaturePos)
        {

            NavMeshPath path = new NavMeshPath();
            Vector3 point = transform.position;
            int whileLoopBreakIndex = 0;
            while (path.status != NavMeshPathStatus.PathComplete)
            {
                point += new Vector3(Random.Range(creatureBoundsX.x, creatureBoundsX.y), 0, Random.Range(creatureBoundsZ.x, creatureBoundsZ.y));
                NavMesh.CalculatePath(transform.position, point, NavMesh.AllAreas, path);
                whileLoopBreakIndex++;
                if (whileLoopBreakIndex >= 1000)
                {
                    Debug.LogError("Change the Bounds to a smaller value, it took 1000 iterations and still didn't find a path (Creature)");

                    break;
                }
            }
            waypoint = point;
            gotCreaturePos = true;
        }
        waypoint.y = transform.position.y;

        //Debug.Log (waypoint);

        if (Vector3.Distance(transform.position, waypoint) <= waypointSoftEdge)
        {
            idleTimerCounter = idleTimer;
            gotCreaturePos = false;

        }
        else
        {
            navigator.SetDestination(waypoint);
        }
    }

    Vector3 lastPosition = new Vector3(0,0,0);

    void Update()
    {
        if (lastPosition != transform.position)
        {
            curState = State.walking;
        }
        else
        {
            curState = State.idle;
        }
        lastPosition = transform.position;
        if (idleTimerCounter <= 0)
        {
            FindRoamingPosition();
        }

        if (stunTime < 0)
        {
            if (target != null)
            {

                navigator.SetDestination(target.transform.position);

                idleTimerCounter = idleTimer;

                if ((target.transform.position - transform.position).magnitude < 1.5f)
                {
                    target.Eat();
                    curState = State.eating;
                    navigator.Stop();
                }
                else
                {
                    navigator.Resume();
                }
            }

            if (target == null)
            {

                idleTimerCounter -= Time.deltaTime;
                //navigator.SetDestination(transform.position);
            }
        }
        else
        {
            stunTime -= Time.deltaTime;

            if (stunTime < 0)
            {
                navigator.Resume();

                _renderer.material.color = originalColor;
            }
        }
        IsBeingLit(PlayerController.Instance.lightObject);

        SetState();
    }

    void FindClosestPath()
    {
        float[] pathLengths = new float[listOfBait.Count];
        paths = new NavMeshPath[listOfBait.Count];
        List<float> arrangeablePathLengths = new List<float>();

        for (int index = 0; index < listOfBait.Count; ++index)
        {
            paths[index] = new NavMeshPath();
            if (!NavMesh.CalculatePath(transform.position, listOfBait[index].transform.position, NavMesh.AllAreas, paths[index]))
            {
                paths[index] = null;
            }

            arrangeablePathLengths.Add(0);

            if (paths[index] != null)
            {
                float distance = 0;

                for (int index2 = 1; index2 < paths[index].corners.Length; ++index2)
                {
                    distance += (paths[index].corners[index2] - paths[index].corners[index2 - 1]).magnitude;
                }
                pathLengths[index] = distance;
            }
            else
            {
                pathLengths[index] = float.MaxValue;
            }
            arrangeablePathLengths[index] = pathLengths[index];
        }

        arrangeablePathLengths.Sort();

        for (int index = 0; index < paths.Length; ++index)
        {
            if (pathLengths[index] == arrangeablePathLengths[0])
            {
                target = listOfBait[index];
                break;
            }
        }
    }
    void OnDestroy()
    {
        if (PlayerController.Instance != null && PlayerController.Instance.creatureList != null)
            PlayerController.Instance.creatureList.Remove(this);

        foreach (BaitController bait in listOfBait)
        {
            if (bait != null)
                bait.CreatureDeath(this);
        }
    }
}