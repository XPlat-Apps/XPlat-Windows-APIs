namespace XPlat.UI.Core
{
    /// <summary>Defines the priority for window event dispatches.</summary>
    public enum CoreDispatcherPriority
    {
        /// <summary>Low priority. Delegates are processed if there are no higher priority events pending in the queue.</summary>
        Low = -1,

        /// <summary>Normal priority. Delegates are processed in the order they are scheduled.</summary>
        Normal = 0,

        /// <summary>High priority. Delegates are invoked immediately for all synchronous requests. Asynchronous requests are queued and processed before any other request type.Do not use this priority level in your app. It is reserved for system events. Using this priority can lead to the starvation of other messages, including system events.</summary>
        High = 1,
    }
}