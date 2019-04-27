using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Signal : ScriptableObject
{

    public List<SignalListener> Listeners;

    public void Raise()
    {
        for(int i = Listeners.Count -1; i >= 0; i--)
        {
            Listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener _listener)
    {
        Listeners.Add(_listener);
    }

    public void DeRegisterListener(SignalListener _listener)
    {
        Listeners.Remove(_listener);
    }
}
