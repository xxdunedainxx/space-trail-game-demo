﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.scripts.levels.lecturehall;
using Assets.scripts.levels.outside_college_area;
using Assets.scripts.levels;

namespace Assets.scripts.core.gameplay
{
    class LevelFactory
    {
        public static string LECTURE_HALL = "LECTUREHALL";
        public static string HALLWAY = "HALLWAY";
        public static string TA_OFFICE = "TAOffice";
        public static string OUTSIDE_LECTUREHALL = "OutsideLectureHall";
        public static string TRANSITION_TO_CITY = "TransitionTocity";

        static Dictionary<string, Func<Level>> LEVELS = new Dictionary<string, Func<Level>>
        {
            {LevelFactory.HALLWAY, generateHallway },
            {LevelFactory.LECTURE_HALL, generateLectureHall},
            {LevelFactory.TA_OFFICE, generateTAOffice },
            {LevelFactory.OUTSIDE_LECTUREHALL, generateOutsideLectureHall },
            {LevelFactory.TRANSITION_TO_CITY, generateTransitionToCity }
        };

        public static Level FetchLevel(string name)
        {
            return LevelFactory.LEVELS[name]();
        }

        private static Hallway generateHallway()
        {
            return new Hallway();
        }

        private static LevelLectureHall generateLectureHall()
        {
            return new LevelLectureHall();
        }

        private static TAOffice generateTAOffice()
        {
            return new TAOffice();
        }

        private static OutsideLecturehall generateOutsideLectureHall()
        {
            return new OutsideLecturehall();
        }

        private static TransitionTocity generateTransitionToCity()
        {
            return new TransitionTocity();
        }
    }
}
