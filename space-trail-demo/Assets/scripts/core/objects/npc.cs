using Assets.scripts.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core.events;
using System;

public class npc : MonoBehaviour, IClickable
{
    [SerializeField]
    public Dialog dialog;
    [SerializeField]
    public Transform body;
    [SerializeField]
    public string name;
    public LayerMask interactLayer;
    [SerializeField]
    List<string> dynamicSentences = null;
    [SerializeField]
    public Sprite interactImageNorth;
    [SerializeField]
    public Sprite interactImageSouth;
    [SerializeField]
    public Sprite interactImageWest;
    [SerializeField]
    public Sprite interactImageEast;

    public List<IEvent> events = null;
    public List<EventLookupInfo> eventLookups = null;
    private MovementController _moveController;

    public void Awake()
    {
        this.interactLayer = Layers.PLAYER_LAYER;
        if (this.dynamicSentences!= null)
        {
            this.generateDynamicSentences();
        }

        if(this.dialog.sentences.Count == 0)
        {
            this.dialog.sentences = new List<string> {"..."};
        }

        this.gameObject.AddComponent<MovementController>();
        this._moveController = this.gameObject.GetComponent<MovementController>();
        this._moveController.objectToControl = this.gameObject;
    }

    public bool CanInteract()
    {
        Collider2D interactChecks = Physics2D.OverlapCircle(body.position, 0.5f, interactLayer);
        if (interactChecks != null)
        {
            return true;
        }
        return false;
    }

    public void orientImage()
    {
        if (this.interactImageEast && this.interactImageNorth && this.interactImageWest && this.interactImageSouth)
        {
            Collider2D col = Physics2D.OverlapCircle(body.position, 0.5f, interactLayer);
            GameObject g = col.gameObject;
            
            if (Mathf.Abs(g.transform.position.x - this.transform.position.x) > Mathf.Abs(g.transform.position.y - this.transform.position.y))
            {
                if (g.transform.position.x > this.transform.position.x)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = this.interactImageEast;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = this.interactImageWest;
                }
            }
            else
            {
                if (g.transform.position.y > this.transform.position.y)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = this.interactImageNorth;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = this.interactImageSouth;
                }
            }
        }
    }

    public virtual void click()
    {
        if (this.dialog != null)
        {
            Debug.unityLogger.Log("NPC was clicked!");
            if (CanInteract())
            {
                this.orientImage();
                Debug.unityLogger.Log("User is close enough for interaction");
                DialogManager manager = DialogManager.instance;
                manager.StartDialogue(this.dialog);
            }
            else
            {
                Debug.unityLogger.Log("USer is not close enough for interaction..");
            }
        }
    }

    private void generateDynamicSentences()
    {
        List<string> newSentencesList = new List<string>();

        for(int i = 0; i < this.dynamicSentences.Count; i++)
        {
            if (this.dynamicSentences[i].Contains("${name}"))
            {
                this.dynamicSentences[i] = this.dynamicSentences[i].Replace("${name}", this.name);
            }
        }
        newSentencesList.AddRange(this.dynamicSentences);
        newSentencesList.AddRange(this.dialog.sentences);

        this.dialog.sentences = newSentencesList;
    }

    public void ExecuteEventListeners()
    {
        if (this.events != null)
        {
            foreach (IEvent even in this.events)
            {
                if (even.active())
                {
                    Debug.unityLogger.Log($"Event {even.name()} IS active");
                    even.execute();
                    even.setEventInactive();
                }
                else
                {
                    Debug.unityLogger.Log($"Event {even.name()} is not active");
                }
            }
        }
        if(this.eventLookups != null)
        {
            foreach(EventLookupInfo info in this.eventLookups)
            {
                EventSubscriptionFactory.instance.ExecuteEvent(info);
            }
        }
    }

    public void AddEventListener(IEvent e)
    {
        if(this.events == null)
        {
            this.events = new List<IEvent>();
        }

        this.events.Add(e);
    }

    public void AddEventLookup(EventLookupInfo info)
    {
        if(this.eventLookups == null)
        {
            this.eventLookups = new List<EventLookupInfo>();
        }
        this.eventLookups.Add(info);
    }

    public void IgnorePlayerCollision()
    {
        Physics2D.IgnoreCollision(GameState.getGameState().playerReference.gameObject.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>());
    }

    public void Move(float xdestination, float ydestination, Vector2 movementSpeed, Action endMovementCallBack = null, int waitSeconds = 0)
    {
        this.IgnorePlayerCollision();
        StartCoroutine(this._moveController.Move(xdestination, ydestination, movementSpeed, endMovementCallBack, waitSeconds));
    }
}
