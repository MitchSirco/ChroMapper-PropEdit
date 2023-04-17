using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Beatmap.Base;
using Beatmap.Containers;
using Beatmap.Enums;
using Beatmap.Helper;

using ChroMapper_PropEdit.Components;

namespace ChroMapper_PropEdit {

public class GizmoController {
	public GameObject position_gizmo_go;
	public PositionGizmo position_gizmo;
	
	public List<BaseGrid> editing;
	public List<DragData> queued;
	
	public void Init(MapEditorUI mapEditorUI) {
		var parent = mapEditorUI.MainUIGroup[5];
		
		position_gizmo_go = new GameObject("Position Gizmo");
		position_gizmo_go.SetActive(false);
		position_gizmo = position_gizmo_go.AddComponent<PositionGizmo>();
		
		position_gizmo.onDragBegin += OnTranslateBegin;
		position_gizmo.onDragMove  += OnTranslateMove;
		position_gizmo.onDragEnd   += OnTranslateEnd;
		
		SelectionController.SelectionChangedEvent += () =>  UpdateSelection();
		BeatmapActionContainer.ActionCreatedEvent += (_) => UpdateSelection();
		BeatmapActionContainer.ActionUndoEvent += (_) =>    UpdateSelection();
		BeatmapActionContainer.ActionRedoEvent += (_) =>    UpdateSelection();
	}
	
	public void UpdateSelection() {
		editing = SelectionController.SelectedObjects.Where(o => o is BaseGrid).Select(it => (BaseGrid)it).ToList();
		// TODO: Keybind
		if (editing.Count() > 0) {
			position_gizmo_go.SetActive(true);
			var o = editing.First();
			
			var collection = BeatmapObjectContainerCollection.GetCollectionForType(o.ObjectType);
			collection.LoadedContainers.TryGetValue(o, out var container);
			
			var atsc = UnityEngine.Object.FindObjectOfType<AudioTimeSyncController>();
			position_gizmo_go.transform.parent = container.gameObject.transform.parent;
			position_gizmo_go.transform.position = container.gameObject.transform.position;
			// Window snapping be silly
			position_gizmo_go.transform.rotation = Quaternion.Euler(0, container.gameObject.transform.rotation.eulerAngles.y, 0);
			//position_gizmo.ball_visible = (editing.Count() > 1);
		}
		else {
			position_gizmo_go.SetActive(false);
		}
	}
	
	void OnTranslateBegin() {
		queued = editing.Select(o => {
			var collection = BeatmapObjectContainerCollection.GetCollectionForType(o.ObjectType);
			
			collection.LoadedContainers.TryGetValue(o, out var container);
			
			return new DragData(
				o,
				container
			);
		}).ToList();
	}
	
	void OnTranslateMove(Vector3 delta) {
		foreach (var d in queued) {
			var pos = new Vector3(
				d.o.CustomCoordinate?.x ?? d.o.PosX - 2,
				d.o.CustomCoordinate?.y ?? d.o.PosY,
				d.o.Time * EditorScaleController.EditorScale);
			pos += delta;
			d.o.CustomCoordinate = new Vector2(pos.x, pos.y);
			d.o.Time = pos.z / EditorScaleController.EditorScale;
			d.con?.UpdateGridPosition();
		}
		position_gizmo_go.transform.localPosition += delta;
	}
	
	void OnTranslateEnd() {
		var beatmapActions = new List<BeatmapObjectModifiedAction>();
		foreach (var d in queued) {
			d.con.Dragging = false;
			beatmapActions.Add(new BeatmapObjectModifiedAction(d.o, d.o, d.old, $"Moved a {d.o.ObjectType} with gizmo.", true));
		}
		BeatmapActionContainer.AddAction(
			new ActionCollectionAction(beatmapActions, true, false, $"Moved ({queued.Count()}) objects with gizmo."),
			true);
	}
	
	public struct DragData {
		public BaseGrid o;
		public readonly BaseObject old;
		public ObjectContainer con;
		public DragData(BaseGrid o, ObjectContainer con) {
			this.o = o;
			this.old = BeatmapFactory.Clone(o);
			this.con = con;
			this.con.Dragging = true;
		}
	}
	
	
}

}
