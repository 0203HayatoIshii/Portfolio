using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;


namespace FEditor
{
	/// <summary>
	/// インスペクターのような設定画面を持つウィンドウ
	/// </summary>
	/// <remarks>制作者 : 石井隼人</remarks>
	public partial class CollectionView : View, IEnumerable<UnityEngine.Object>
    {
		//*************************************************************************************************
		// プライベート変数
		//*************************************************************************************************
		private static readonly string UP_BUTTON_NAME = "ToUpButton";
        private static readonly string DOWN_BUTTON_NAME = "ToDownButton";
        private static readonly string REMOVE_BUTTON_NAME = "RemoveButton";

        private readonly LinkedList<CollectionNode> PROPERTIES;

		//*************************************************************************************************
		// パブリック関数
		//*************************************************************************************************
		public CollectionView(ICollection<UnityEngine.Object> objects, GUIStyle style = null) : base("Collection", WidgetScale.HD, style)
        {
            PROPERTIES = new LinkedList<CollectionNode>();
            foreach (var c in objects)
            {
                var temp = AddChild(new CollectionNode(c, WidgetScale.Zero));
                var node = PROPERTIES.AddLast(temp);
                temp.MyNode = node;
            }
        }
        public CollectionView(string name, WidgetScale scale, ICollection<UnityEngine.Object> objects, GUIStyle style = null) : base(name, scale, style)
        {
            PROPERTIES = new LinkedList<CollectionNode>();
            foreach (var c in objects)
            {
                Add(c);
            }
        }

        public override void Draw()
        {
            for(LinkedListNode<CollectionNode> c = PROPERTIES.First; c != null; c = c.Next)
            {
                c.Value.Draw();
            }
        }

        public override bool Notify(IWidget sender, string message)
        {
            // ノードを上に
            if (sender.Name == UP_BUTTON_NAME)
            {
                if (sender.Master is not CollectionNode node)
                    return base.Notify(sender, message);

                var prev = node.MyNode.Previous;
                if (prev != null)
                {
                    PROPERTIES.Remove(node.MyNode);
                    PROPERTIES.AddBefore(prev, node.MyNode);
                }
                return true;
            }

            // ノードを下に
            if (sender.Name == DOWN_BUTTON_NAME)
            {
                if (sender.Master is not CollectionNode node)
                    return base.Notify(sender, message);

                var next = node.MyNode.Next;
                if (next != null)
                {
                    PROPERTIES.Remove(node.MyNode);
                    PROPERTIES.AddAfter(next, node.MyNode);
                }
                return true;
            }

            // ノードを削除
            if (sender.Name == REMOVE_BUTTON_NAME)
            {
                if (sender.Master is not CollectionNode node)
                    return base.Notify(sender, message);

                PROPERTIES.Remove(node.MyNode);
                RemoveChild(node);
                return true;
            }

            // どれでもなければ親に任せる
            return base.Notify(sender, message);
        }

        public void Add(UnityEngine.Object @object)
        {
            var temp = AddChild(new CollectionNode(@object, WidgetScale.Zero));
            var node = PROPERTIES.AddLast(temp);
            temp.MyNode = node;
        }
	} // CollectionView


	public partial class CollectionView
    {
        private class CollectionNode : View
        {
			//*************************************************************************************************
			// パブリックプロパティ
			//*************************************************************************************************
			public LinkedListNode<CollectionNode> MyNode { get; set; }
            public UnityEngine.Object Target { get; }

			//*************************************************************************************************
			// パブリック関数
			//*************************************************************************************************
			public CollectionNode(UnityEngine.Object @object, WidgetScale scale) : base("collection", scale)
            {
                Target = @object;

                const float BUTTON_SCALE = 25.0f;

                var propertyScale = scale;
                propertyScale.maxX = -BUTTON_SCALE;
                propertyScale.minX = -BUTTON_SCALE;
                AddChild(new PropertyField(@object, @object.GetType().Name, propertyScale));

                var buttonScale = new WidgetScale(BUTTON_SCALE, BUTTON_SCALE);
                AddChild(new Button("↑", UP_BUTTON_NAME, buttonScale));
                AddChild(new Button("↓", DOWN_BUTTON_NAME, buttonScale));
                AddChild(new Button("-", REMOVE_BUTTON_NAME, buttonScale));
            }

            public override void Draw()
            {
                EditorGUILayout.BeginHorizontal(Style);
                EditorGUILayout.BeginVertical(Style);
                GetChild(0).Draw();
                EditorGUILayout.EndVertical();
                GetChild(1).Draw();
                GetChild(2).Draw();
                GetChild(3).Draw();
                EditorGUILayout.EndHorizontal();
            }
		} // CollectionNode
	} // CollectionView

	public partial class CollectionView
    {
        private class Iterator : IEnumerator<UnityEngine.Object>
        {
			//*************************************************************************************************
			// プライベート変数
			//*************************************************************************************************
			private readonly IEnumerator<CollectionNode> ITERATOR;
            private bool disposedValue;

			//*************************************************************************************************
			// パブリックプロパティ
			//*************************************************************************************************
			public Object Current { get => ITERATOR.Current.Target; }
            object IEnumerator.Current { get => ITERATOR.Current.Target; }

			//*************************************************************************************************
			// パブリック関数
			//*************************************************************************************************
			public Iterator(IEnumerator<CollectionNode> iterator) => ITERATOR = iterator;

			public bool MoveNext() => ITERATOR.MoveNext();
            public void Reset() => ITERATOR.Reset();

			public void Dispose()
			{
				// このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
				Dispose(disposing: true);
				System.GC.SuppressFinalize(this);
			}

			//*************************************************************************************************
			// 継承メンバ
			//*************************************************************************************************
			protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    }

                    // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                    // TODO: 大きなフィールドを null に設定します
                    disposedValue = true;
                }
            }

            // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
            // ~Iterator()
            // {
            //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            //     Dispose(disposing: false);
            // }
		} // Iterator

		public IEnumerator<Object> GetEnumerator() => new Iterator(PROPERTIES.GetEnumerator());
		IEnumerator IEnumerable.GetEnumerator() => new Iterator(PROPERTIES.GetEnumerator());
	} // CollectionView
} // FEditor
