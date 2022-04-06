using Core.Runtime;
using UnityEngine;

public readonly struct InfoEvent : IEvent
{
	public readonly string Info;
	public InfoEvent(string info) => Info = info;
}

public class EventBusDemoBehaviour : MonoBehaviour
{
	private void OnEnable() => EventBus.Subscribe<InfoEvent>(OnInfoEvent);
	private void OnDisable() => EventBus.UnSubscribe<InfoEvent>(OnInfoEvent);
	private void OnInfoEvent(InfoEvent infoEvent) => Debug.Log(infoEvent.Info, this);


	[ContextMenu(nameof(PublishInfoEvent))]
	private void PublishInfoEvent()
	{
		EventBus.Publish<InfoEvent>(new InfoEvent("InfoEvent: DemoRun..."));
	}
}