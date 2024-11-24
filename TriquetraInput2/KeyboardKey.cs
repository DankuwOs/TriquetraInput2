﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Triquetra.Input
{
    [Serializable]
    public class KeyboardKey
    {
        [XmlAttribute] public KeyCode PrimaryKey = KeyCode.None;
        [XmlAttribute] public KeyCode SecondaryKey = KeyCode.None;

        [XmlIgnore] public bool PrimaryKeyDown = false;
        [XmlIgnore] public bool SecondaryKeyDown = false;
        
        [XmlIgnore] public float PrimaryPressTime;
        [XmlIgnore] public float SecondaryPressTime;

        [XmlAttribute] public bool IsAxis = false;
        [XmlAttribute] public bool IsRepeatButton = false;

        [XmlAttribute] public float Smoothing = 0.5f;

        [XmlIgnore] public int t = 32000;

        public int GetAxisTranslatedValue()
        {
            if (UnityEngine.Input.GetKeyDown(PrimaryKey))
                PrimaryPressTime = Time.time;
            if (UnityEngine.Input.GetKeyDown(SecondaryKey))
                SecondaryPressTime = Time.time;

            bool isPrimaryPressed = UnityEngine.Input.GetKey(PrimaryKey);
            bool isSecondaryPressed = UnityEngine.Input.GetKey(SecondaryKey);

            if (isPrimaryPressed && !isSecondaryPressed)
                t = (int)Mathf.Lerp(t, Binding.AxisMax, Time.deltaTime / Smoothing);
            else if (isSecondaryPressed && !isPrimaryPressed)
                t = (int)Mathf.Lerp(t, Binding.AxisMin, Time.deltaTime / Smoothing);
            
            if (!isPrimaryPressed && !isSecondaryPressed)
                t = (int)Mathf.Lerp(t, Binding.AxisMiddle, Time.deltaTime / Smoothing);
            

            return t;
        }
    }
}
