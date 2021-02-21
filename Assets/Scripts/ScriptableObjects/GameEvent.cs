using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<EventListenerComponent> eventListeners =
            new List<EventListenerComponent>();

        public void Raise()
        {
            for (int i = 0; i < eventListeners.Count; ++i)
                eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(EventListenerComponent listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(EventListenerComponent listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}