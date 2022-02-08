using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGGE{
    public abstract class TPCBase
    {
        protected Transform camera, player;

        public TPCBase(Transform _camera, Transform _player){
            camera = _camera;
            player = _player;
        }

        public abstract void Start();
        public abstract void Update();
    }
}
