using System;

namespace Core.Runtime
{
	/// <summary>
	/// Contract/Tag For Any Event To Communicate Through <see cref="EventBus"/>
	/// </summary>
	public interface IEvent
	{
		// Just A Tag...
	}

	/// <summary>
	/// <see cref="EventBus"/> Is Core & Responsible For Handling <see cref="IEvent"/>'s Dispatch & LifeCycle...
	/// </summary>
	public static class EventBus
	{
		/// <summary>
		/// Subscribe The Given Callback In It's Specific Publishing Channel In <see cref="EventBus"/>
		/// </summary>
		/// <param name="callback">Active Callback For <see cref="TEvent"/>'s Channel.</param>
		/// <typeparam name="TEvent">typeOf(<see cref="TEvent"/>)</typeparam>
		public static void Subscribe<TEvent>(Action<TEvent> callback)
			where TEvent : struct, IEvent
			=> Publisher<TEvent>.Subscribe(callback);

		/// <summary>
		/// UnSubscribe The Given Callback From It's Specific Publishing Channel In <see cref="EventBus"/>
		/// </summary>
		/// <param name="callback">Active Callback For <see cref="TEvent"/>'s Channel.</param>
		/// <typeparam name="TEvent">typeOf(<see cref="TEvent"/>)</typeparam>
		public static void UnSubscribe<TEvent>(Action<TEvent> callback)
			where TEvent : struct, IEvent
			=> Publisher<TEvent>.UnSubscribe(callback);

		/// <summary>
		/// Publish The Given <see cref="@event"/> Over It's Specific Channel In <see cref="EventBus"/>
		/// </summary>
		/// <param name="event"><see cref="TEvent"/>'s Object...</param>
		/// <typeparam name="TEvent">typeOf(<see cref="TEvent"/>)</typeparam>
		public static void Publish<TEvent>(TEvent @event)
			where TEvent : struct, IEvent
			=> Publisher<TEvent>.Publish(@event);

		/// <summary>
		/// Core Publisher For All <see cref="IEvent"/>'s Inside <see cref="EventBus"/> 
		/// </summary>
		/// <typeparam name="TEvent"><see cref="IEvent"/>' Type</typeparam>
		private static class Publisher<TEvent>
			where TEvent : struct, IEvent
		{
			////////////////////////////////////////////////////////////////////////
			private static event Action<TEvent> EventSet = null;

			////////////////////////////////////////////////////////////////////////
			public static void Subscribe(Action<TEvent> listener) => EventSet += listener;

			////////////////////////////////////////////////////////////////////////
			public static void UnSubscribe(Action<TEvent> listener) => EventSet -= listener;

			////////////////////////////////////////////////////////////////////////
			public static void Publish(TEvent @event)
			{
				if (EventSet != null) EventSet(@event);
			}
		}
	}
}