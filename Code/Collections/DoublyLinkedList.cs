using System.Collections;
using System.Collections.Generic;

namespace GA.Collections
{
	public class DoublyLinkedList<T> : ICollection<T>
	{
		public class Node
		{
			public T Value { get; set; }
			public Node Next { get; set; }
			public Node Previous { get; set; }

			public Node() : this(default(T))
			{
			}

			public Node(T value, Node next = null, Node previous = null)
			{
				Value = value;
				Next = next;
				Previous = previous;
			}

		}

		/// <summary>
		/// The head of the doubly linked list. When the list is empty, this will be null.
		/// </summary>
		protected Node Head { get; set; } = null;

		/// <summary>
		/// The tail of the doubly linked list. When the list is empty, this will be null.
		/// </summary>
		protected Node Tail { get; set; } = null;

		public int Count { get; private set; } = 0;

		public virtual bool IsReadOnly => false;

		public void AddHead(T item)
		{
			Node node = new Node(item);

			if (Head == null)
			{
				Head = node;
				Tail = node;
				Count = 1;
				return;
			}

			Head.Previous = node;
			node.Next = Head;
			Head = node;
			Count++;
		}

		public void Add(T item)
		{
			if (IsReadOnly)
			{
				throw new System.NotSupportedException("The collection is read-only.");
			}

			if (Head == null)
			{
				AddHead(item);
				return;
			}
			Node node = new Node(item);
			Tail.Next = node;
			node.Previous = Tail;
			Tail = node;
			Count++;
		}

		public void AddAfter(T item, Node listedNode)
		{
			if (listedNode == Tail)
			{
				Add(item);
				return;
			}

			Node node = new Node(item);
			node.Next = listedNode.Next;
			node.Previous = listedNode;
			listedNode.Next = node;

			if (node.Next != null)
			{
				node.Next.Previous = node;
			}

			Count++;
		}

		public void Clear()
		{
			if (IsReadOnly)
			{
				throw new System.NotSupportedException("The collection is read-only.");
			}

			Head = null;
			Tail = null;
			Count = 0;
		}

		public bool Contains(T item)
		{
			Node current = Head;
			while (current != null)
			{
				if (EqualityComparer<T>.Default.Equals(current.Value, item))
				{
					return true;
				}

				current = current.Next;
			}

			return false;
		}

		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			throw new System.NotImplementedException("Not implemented for this example either XD");
		}

		public IEnumerator<T> GetEnumerator()
		{
			Node current = Head;
			while (current != null)
			{
				yield return current.Value;
				current = current.Next;
			}
		}

		public IEnumerator<T> GetEnumeratorReversed()
		{
			Node current = Tail;
			while (current != null)
			{
				yield return current.Value;
				current = current.Previous;
			}
		}

		public bool Remove(T item)
		{
			if (IsReadOnly)
			{
				throw new System.NotSupportedException("This collection is read-only");
			}
			Node node = new Node(item);
			Node current = Head;

			while (current != null)
			{
				if (EqualityComparer<T>.Default.Equals(current.Value, item))
				{
					if (current.Previous != null)
					{
						// Removing any other element than the first.
						current.Previous.Next = current.Next;
					}
					else
					{
						// Removing the first element.
						RemoveHead();
						return true;
					}

					if (current.Next != null)
					{
						current.Next.Previous = current.Previous;
					}
					else
					{
						// Removing the last element.
						RemoveTail();
						return true;
					}

					Count--;
					return true;
				}

				current = current.Next;
			}

			return false;
		}

		/// <summary>
		/// Remove the first node in the list.
		/// </summary>
		public void RemoveHead()
		{
			if (IsReadOnly)
			{
				throw new System.NotSupportedException("This collection is read-only");
			}
			if (Head == null)
			{
				throw new System.InvalidOperationException("Cannot remove from empty list");
			}

			Head = Head.Next;
			if (Head == null)
			{
				Tail = null;
				Count = 0;
				return;
			}

			Head.Previous = null;
			Count--;
		}

		/// <summary>
		/// Remove the last node in the list.
		/// </summary>
		public void RemoveTail()
		{
			if (IsReadOnly)
			{
				throw new System.NotSupportedException("This collection is read-only");
			}
			if (Tail == null)
			{
				throw new System.InvalidOperationException("Cannot remove from empty list");
			}

			Tail = Tail.Previous;
			if (Tail == null)
			{
				Head = null;
				Count = 0;
			}

			Tail.Next = null;
			Count--;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}