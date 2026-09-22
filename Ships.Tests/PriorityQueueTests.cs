using GA.Collections;
using Xunit;

public class PriorityQueueTests
{
	[Fact]
	public void TestBasicNewEmptyQueue()
	{
		GA.Collections.PriorityQueue<int> queue = new GA.Collections.PriorityQueue<int>();
		Assert.Equal(0, queue.Count);
		Assert.True(queue.IsConsistent());
	}

	[Fact]
	public void TestBasicEnqueue()
	{
		GA.Collections.PriorityQueue<int> queue = new GA.Collections.PriorityQueue<int>();
		queue.Enqueue(4);
		Assert.Equal(1, queue.Count);
		Assert.True(queue.IsConsistent());
		queue.Enqueue(5);
		Assert.Equal(2, queue.Count);
		Assert.True(queue.IsConsistent());
		queue.Enqueue(7);
		Assert.Equal(3, queue.Count);
		Assert.True(queue.IsConsistent());
	}

	[Fact]
	public void TestBasicPeek()
	{
		GA.Collections.PriorityQueue<int> queue = new GA.Collections.PriorityQueue<int>();
		Assert.Throws<InvalidOperationException>(() => queue.Peek());
		queue.Enqueue(35);
		Assert.Equal(1, queue.Count);
		Assert.Equal(35, queue.Peek());
		queue.Enqueue(2);
		Assert.Equal(2, queue.Count);
		Assert.Equal(2, queue.Peek());
		queue.Enqueue(14);
		Assert.Equal(3, queue.Count);
		Assert.Equal(2, queue.Peek());
	}

	[Fact]
	public void TestBasicDequeue()
	{
		GA.Collections.PriorityQueue<int> queue = new GA.Collections.PriorityQueue<int>();
		Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
		queue.Enqueue(2);
		queue.Enqueue(3);
		queue.Enqueue(1);
		Assert.Equal(1, queue.Dequeue());
		Assert.Equal(2, queue.Count);
		Assert.True(queue.IsConsistent());
		Assert.Equal(2, queue.Dequeue());
		Assert.Equal(1, queue.Count);
		Assert.True(queue.IsConsistent());
		Assert.Equal(3, queue.Dequeue());
		Assert.Equal(0, queue.Count);
		Assert.True(queue.IsConsistent());
	}

	[Fact]
	public void TestBasicContains()
	{
		GA.Collections.PriorityQueue<int> queue = new GA.Collections.PriorityQueue<int>();
		queue.Enqueue(1);
		queue.Enqueue(2);
		Assert.True(queue.Contains(2));
		Assert.True(queue.Contains(1));
		Assert.False(queue.Contains(12));
	}

	[Fact]
	public void TestBasicClear()
	{
		GA.Collections.PriorityQueue<int> queue = new GA.Collections.PriorityQueue<int>();
		queue.Enqueue(1);
		queue.Enqueue(2);
		queue.Enqueue(3);
		queue.Clear();
		Assert.Equal(0, queue.Count);
		Assert.False(queue.Contains(1));
		Assert.False(queue.Contains(2));
		Assert.False(queue.Contains(3));
		Assert.True(queue.IsConsistent());
	}
}