
using UnityEngine;
using UnityEngine.AI;

public class MiniController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private ColorBind colorBindings;
    private ColorWatcher watcher;
    private PublisherManager publisherManager;
    private float throttle;
    private float DestoyAniMini;
    private int groupID = 1;
    private float TimeSinceChecked;
    private float TimeDeleteChecked;

    void Awake()
    {
        this.publisherManager = GameObject.FindGameObjectWithTag("Script Home").GetComponent<PublisherManager>();
        this.RandomizeBody();
        throttle = Random.Range(0.0f, 10.0f);
        DestoyAniMini = Random.Range(10.0f, 40.0f);
        this.groupID = Random.Range(1, 4);
        this.publisherManager.SubscribeToGroup(groupID, OnMoveMessage);
        this.TimeSinceChecked = 0.0f;
        this.TimeDeleteChecked = 0.0f;
        switch(this.groupID)
        {
            case 1:
                this.ChangeColor(this.colorBindings.GetGroup1Color());
                //set up a watcher for Stage 1.1
                watcher = new ColorWatcher(colorBindings.GetGroup1Color, this.ChangeColor);
                break;
            case 2:
                this.ChangeColor(this.colorBindings.GetGroup2Color());
                //set up a watcher for Stage 1.1
                watcher = new ColorWatcher(colorBindings.GetGroup2Color, this.ChangeColor);
                break;
            case 3:
                this.ChangeColor(this.colorBindings.GetGroup3Color());
                watcher = new ColorWatcher(colorBindings.GetGroup3Color, this.ChangeColor);
                //set up a watcher for Stage 1.1
                break;
            default:
                Debug.Log("MiniController is Awake but has no group.");
                break;
        }
        this.agent.SetDestination(new Vector3(Random.Range(-20f, 20f), this.gameObject.transform.position.y, Random.Range(-20f, 20f)));
    }

    /// <summary>
    /// Updates the color of the object.
    private void Update()
    {

        TimeSinceChecked = TimeSinceChecked + Time.deltaTime;
        if (this.TimeSinceChecked > throttle)
        {
            this.watcher.Watch();
            this.TimeSinceChecked = 0.0f;
        }
        TimeDeleteChecked = TimeDeleteChecked + Time.deltaTime;
        if (this.TimeDeleteChecked > DestoyAniMini)
        {
            this.publisherManager.UnsubscribeFromGroup(groupID, OnMoveMessage);
            Destroy(this.gameObject);
        }
    }

    /// </summary>
    /// <param name="color">The Renderer's material property will be updated
    /// with this color parameter.</param>
    private void ChangeColor(Color color)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material.SetColor("MainColor", color);
        }
    }

    private void RandomizeBody()
    {
        var randomScale = Random.Range(0.1f, 1.0f);
        this.gameObject.transform.localScale *= randomScale;
    }

    public void OnMoveMessage(Vector3 destination)
    {
        this.agent.SetDestination(destination);
    }
}
