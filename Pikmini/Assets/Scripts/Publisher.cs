using System;
using System.Collections;
using System.Collections.Generic;
using Pikmini;
using UnityEngine;

public class Publisher : IPublisher
{
    List<Action<Vector3>> collection;

    public Publisher()
    {
        collection = new List<Action<Vector3>>();
    }
        
    public void Unsubscribe(Action<Vector3> notifier)
    {
        collection.Remove(notifier);
    }

    public void Subscribe(Action<Vector3> notifier)
    {
        collection.Add(notifier);
    }

    public void Notify(Vector3 destination)
    {
        foreach(var notifier in collection)
        {
            notifier(destination);
        }
    }
}
