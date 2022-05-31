namespace TestUse
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Quadtree
    {
        private int MAX_OBJECTS = 3;
        private int MAX_LEVELS = 5;
 
        private int level;
        private List<Collider> _objects;
        private Bounds _bounds;
        private Quadtree[] _nodes;

        private Bounds[] _subBounds;
        
        public Quadtree(int pLevel, Bounds inBounds)
        {
            level = pLevel;
            _objects = new List<Collider>();
            _bounds = inBounds;
            _nodes = new Quadtree[4];
            _subBounds = new Bounds[4];
            
            // These could be in Split method to reduce the memory size
            var subWidth = (_bounds.extents.x / 2.0f);
            var subHeight = (_bounds.extents.z / 2.0f);
            var x = _bounds.center.x;
            var z = _bounds.center.z;

            _subBounds[0] = new Bounds(
                new Vector3((x + subWidth), 1.0f, (z + subHeight)),
                new Vector3(_bounds.extents.x, 1.0f, _bounds.extents.z));
            _subBounds[1] = new Bounds(
                new Vector3((x - subWidth), 1.0f, (z + subHeight)),
                new Vector3(_bounds.extents.x, 1.0f, _bounds.extents.z));
            _subBounds[2] = new Bounds(
                new Vector3((x - subWidth), 1.0f, (z - subHeight)),
                new Vector3(_bounds.extents.x, 1.0f, _bounds.extents.z));
            _subBounds[3] = new Bounds(
                new Vector3((x + subWidth), 1.0f, (z - subHeight)),
                new Vector3(_bounds.extents.x, 1.0f, _bounds.extents.z));
            
            // Debug.Log($"level: {level}");
            // Debug.Log($"center: {_subBounds[0].center.ToString()} extents: {_subBounds[0].extents.ToString()}");
            // Debug.Log($"center: {_subBounds[1].center.ToString()} extents: {_subBounds[1].extents.ToString()}");
            // Debug.Log($"center: {_subBounds[2].center.ToString()} extents: {_subBounds[2].extents.ToString()}");
            // Debug.Log($"center: {_subBounds[3].center.ToString()} extents: {_subBounds[3].extents.ToString()}");
        }
        
        public void Clear()
        {
            _objects.Clear();
 
            for (int i = 0; i < _nodes.Length; ++i)
            {
                if (_nodes[i] != null)
                {
                    _nodes[i].Clear();
                    _nodes[i] = null;
                }
            }
        }
        
        private void Split()
        {
            _nodes[0] = new Quadtree(level + 1, _subBounds[0]);
            _nodes[1] = new Quadtree(level + 1, _subBounds[1]);
            _nodes[2] = new Quadtree(level + 1, _subBounds[2]);
            _nodes[3] = new Quadtree(level + 1, _subBounds[3]);
        }
        
        private int GetIndex(Collider collider)
        {
            int index = -1;
            var inBounds = collider.bounds;

            if (inBounds.center.z > _bounds.center.z)
            {
                if (inBounds.center.x > _bounds.center.x)
                {
                    index = 0;
                }
                else
                {
                    index = 1;
                }
            }
            else
            {
                if (inBounds.center.x > _bounds.center.x)
                {
                    index = 3;
                }
                else
                {
                    index = 2;
                }
            }
 
            return index;
        }
        
        public void Insert(Collider collider)
        {
            if (AddToSubLevel(collider)) return;
 
            AddToCurrentLevel(collider);
        }

        private bool AddToSubLevel(Collider collider)
        {
            if (_nodes[0] != null)
            {
                int index = GetIndex(collider);
                
                if (index != -1)
                {
                    _nodes[index].Insert(collider);
 
                    return true;
                }
            }

            return false;
        }

        private void AddToCurrentLevel(Collider collider)
        {
            _objects.Add(collider);
            
            if (_objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
            {
                if (_nodes[0] == null)
                { 
                    Split(); 
                }
 
                int i = 0;
                while (i < _objects.Count)
                {
                    int index = GetIndex(_objects[i]);
                    if (index != -1)
                    {
                        var atObject = _objects[i];
                        _objects.RemoveAt(i);
                        _nodes[index].Insert(atObject);
                    }
                    else
                    {
                        ++i;
                    }
                }
            }
        }

        public List<Collider> Retrieve(List<Collider> returnObjects, Collider collider)
        {
            int index = GetIndex(collider);
            if (index != -1 && _nodes[0] != null)
            {
                _nodes[index].Retrieve(returnObjects, collider);
            }
 
            returnObjects.AddRange(_objects);
 
            return returnObjects;
        }
    }
}
