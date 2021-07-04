﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core;
using Assets.scripts.core.objects;

namespace Assets.scripts.levels.lecturehall
{
    // disables the sparkle sprite and gives player the book + omeeds note
    public class NoteEvent : ObjectObtainedEvent
    {

        public BasicNote noteItem;
        private InvisibleBlock invisibleWall;

        public NoteEvent(BasicNote note, ref InvisibleBlock invisibleWall) : base()
        {
            this.noteItem = note;
            this.invisibleWall = invisibleWall;
        }

        public override void execute()
        {
            player p = GameState.getGameState().playerReference;
            p.removeFromInventory(this.noteItem.name());
            Debug.unityLogger.Log($"Setting invisible wall to background image {Layers.BACKGROUND_IMAGE_LAYER_VALUE}");
            this.invisibleWall.Blocking = false;
            GameState.getGameState().levelState.LECTURE_HALL.completed = true;
        }
    }
}