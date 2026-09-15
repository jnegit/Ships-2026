using GA.Collections;
using Xunit;

public class DoublyLinkedListTests
{
    [Fact]
    public void TestAdd()
    {
        GA.Collections.DoublyLinkedList<int> list = new GA.Collections.DoublyLinkedList<int>();
        list.Add(0);
        list.Add(2);
        list.Add(-1);

        Assert.Equal(3, list.Count);
        Assert.Equal(new[] { 0, 2, -1 }, list);
    }

    [Fact]
    public void TestAddAfter()
    {
        GA.Collections.DoublyLinkedList<int> list = new GA.Collections.DoublyLinkedList<int>();
        list.Add(1);
        list.Add(3);
        list.AddAfter(8, 1);

        Assert.Equal(3, list.Count);
        Assert.Equal(new[] { 1, 8, 3 }, list);
    }

    [Fact]
    public void TestClear()
    {
        GA.Collections.DoublyLinkedList<string> list = new GA.Collections.DoublyLinkedList<string> { "First", "Second", "Third" };
        list.Clear();

        Assert.Equal(0, list.Count);
        Assert.Empty(list);
    }

    [Fact]
    public void TestContains()
    {
        GA.Collections.DoublyLinkedList<string> list = new GA.Collections.DoublyLinkedList<string> { "First", "Second", "Third" };
        bool listedNode = list.Contains("Third");
        bool nonListedNode = list.Contains("Foo");

        Assert.Equal(true, listedNode);
        Assert.Equal(false, nonListedNode);
    }

    [Fact]
    public void TestGetEnumerator()
    {
        GA.Collections.DoublyLinkedList<int> list = new GA.Collections.DoublyLinkedList<int> { 1, 2, 3 };
        var constructedList = new GA.Collections.DoublyLinkedList<int>();
        var enumerator = list.GetEnumerator();
        while (enumerator.MoveNext())
        {
            constructedList.Add(enumerator.Current);
        }
        Assert.Equal(new[] { 1, 2, 3 }, constructedList);
    }

    [Fact]
    public void TestGetEnumeratorReversed()
    {
        GA.Collections.DoublyLinkedList<int> list = new GA.Collections.DoublyLinkedList<int> { 1, 2, 3 };
        var reversedList = new GA.Collections.DoublyLinkedList<int>();
        var enumerator = list.GetEnumeratorReversed();
        while (enumerator.MoveNext())
        {
            constructedList.Add(enumerator.Current);
        }
        Assert.Equal(new[] { 3, 2, 1 }, reversedList);
    }

    [Fact]
    public void TestRemove()
    {
        GA.Collections.DoublyLinkedList<int> list = new GA.Collections.DoublyLinkedList<int> { 1, 2, 3 };
        bool removedMiddleNode = list.Remove(2);
        bool removedTail = list.Remove(3);
        bool removedHead = list.Remove(1);

        // Assert
        Assert.True(removedMiddleNode);
        Assert.True(removedTail);
        Assert.True(removedHead);
        Assert.Equal(0, list.Count);
        Assert.Empty(list);
    }
}