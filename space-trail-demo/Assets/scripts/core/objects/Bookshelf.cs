﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;

namespace Assets.scripts.core.objects
{
    public class Bookshelf : ObtainableObject, IClickable
    {
        [SerializeField]
        public List<Book> books = null;
        [SerializeField]
        public List<Sprite> bookShelfImages = null;
        [SerializeField]
        public LayerMask interactLayer;
        [SerializeField]
        public Transform body;
        [SerializeField]
        public string name = "bookshelf";

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public bool CanInteract()
        {
            Collider2D interactChecks = Physics2D.OverlapCircle(body.position, .5f, interactLayer);

            if (interactChecks != null)
            {
                Debug.unityLogger.Log($"Colided with :{interactChecks.attachedRigidbody.gameObject.name}");
                return true;
            }
            return false;
        }

        public void click()
        {
            if (CanInteract())
            {
                if (this.books.Count > 0)
                {
                    Book bookToReturn = this.books[0];
                    Debug.unityLogger.Log($"User obtained book {bookToReturn.name}");
                    this.books.RemoveAt(0);

                    player player = GameObject.Find(Physics2D.OverlapCircle(body.position, .5f, interactLayer).attachedRigidbody.gameObject.name).GetComponent<player>();
                    Debug.unityLogger.Log($"Adding to player {player.name}'s inventory");
                    player.addToInventory(bookToReturn);
                    if (this.bookShelfImages.Count > 0)
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = this.bookShelfImages[0];
                        this.bookShelfImages.RemoveAt(0);
                    }

                    if (this.books.Count <= 0)
                    {
                        this.associatedAnimation.disableAnimation();
                        this.emitEvent(new ObjectObtainedEvent(true, this.name));
                    }
                }
                else
                {
                    Debug.unityLogger.Log("No more books to give :(");
                }
            }
            else
            {
                Debug.unityLogger.Log("USer is not close enough for interaction..");
            }
        }
    }
}