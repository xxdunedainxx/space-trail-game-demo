using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.ui;

public class ObjectAnimationHandler : MonoBehaviour
{

    [SerializeField]
    public SpriteRenderer spriteRender;
    [SerializeField]
    public Animator animator;

    private GameObject animationObject;

    public ObjectAnimationHandler(string gobject)
    {
        this.animationObject = GameObject.Find(gobject);
        this.spriteRender = this.animationObject.GetComponent<SpriteRenderer>();
        this.animator = this.animationObject.GetComponent<Animator>();
    }

    void Start()
    {
        UIObjectFactory objectFactory = UIObjectFactory.instance;
        //objectFactory.objectAnimationHandlers[animator.name] = this;
        this.enableAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableAnimation()
    {
        this.animator.enabled = true;
        this.animator.gameObject.SetActive(true);
    }

    public void disableAnimation()
    {
        this.animator.enabled = false;
        this.animator.gameObject.SetActive(false);
    }
}
