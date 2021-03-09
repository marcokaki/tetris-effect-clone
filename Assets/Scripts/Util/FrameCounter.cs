using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCounter : MonoBehaviour
{
    #region Singleton Implementation

    private static FrameCounter _instance;
    public static FrameCounter Instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<FrameCounter>();
                
            }
            return _instance;
        }
    }

    #endregion

    private static List<Token> tokens;

    private void FixedUpdate()
    {
        
    }

    public class Token
    {
        private EventHandler onTimeReached;

        public Token(int frame)
        {

        }

        public Func<bool> Elapsed;





    }



}
