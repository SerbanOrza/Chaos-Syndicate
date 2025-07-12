using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroupConnection {
    public List<DestructibleStructure> elements = new List<DestructibleStructure>();
    // you can add groups or individual elements
    public List<int> affectedGroups = new List<int>();
    public List<DestructibleStructure> affectedObjs = new List<DestructibleStructure>();
}

public class Groups : MonoBehaviour
{
    public int groupsLeft;
    public List<GroupConnection> groups = new List<GroupConnection>();
    public Dictionary<DestructibleStructure,HashSet<int>> obj_to_groups = new Dictionary<DestructibleStructure,HashSet<int>>();
        
    void Start()
    {
        MyUtil.instance.addGroupToParent(gameObject);
        groupsLeft=groups.Count;
        for (int i = 0; i < groups.Count; i++)
            foreach (DestructibleStructure obj in groups[i].elements)
                try
                {
                    if (obj_to_groups.ContainsKey(obj) == false)
                        obj_to_groups.Add(obj, new HashSet<int>());
                    obj_to_groups[obj].Add(i); // add group i to the object obj
                }
                catch (Exception e){}
    }

    public List<DestructibleStructure> removeObjFromGroups(DestructibleStructure obj)
    {
        //it returns a list with the objects that we need to break.
        
        List<DestructibleStructure> obj_to_break = new List<DestructibleStructure>();
        
        if (obj is null || obj_to_groups.TryGetValue(obj, out var itsGroups) == false)
            return obj_to_break;
        
        foreach (int i in itsGroups)
        {
            int size = groups[i].elements.Count;
            try
            {
                groups[i].elements.Remove(obj);
            }
            catch (Exception e){}
            
            if (size == 1 && groups[i].elements.Count == 0) //we have an empty group now. break
            {
                groupsLeft--;
                //break the connected groups
                foreach (int other_group_id in groups[i].affectedGroups)
                    foreach (DestructibleStructure other_obj in groups[other_group_id].elements)
                        //mark this element to break it later
                        obj_to_break.Add(other_obj);
                //break the individual elements
                foreach (DestructibleStructure other_obj in groups[i].affectedObjs)
                    obj_to_break.Add(other_obj);
                    
            }
        }
        if(groupsLeft==0)
            Destroy(gameObject,1);
        Debug.Log(obj_to_break.Count);
        return obj_to_break; //it may contain duplicates but it is fine
    }
}
