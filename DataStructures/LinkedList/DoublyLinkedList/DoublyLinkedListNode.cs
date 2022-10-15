namespace DataStructures.LinkedList.DoublyLinkedList;

/// <summary>
///     Generic node class for Doubly Linked List.
/// </summary>
/// <typeparam name="T">Generic type.</typeparam>
public class DoublyLinkedListNode<T>
{
    #region properties

    /// <summary>
    ///     Gets the data stored on this node.
    /// </summary>
    public T Data { get; }

    /// <summary>
    ///     Gets or sets the reference to the next node in the Doubly Linked List.
    /// </summary>
    public DoublyLinkedListNode<T>? Next { get; set; }

    /// <summary>
    ///     Gets or sets the reference to the previous node in the Doubly Linked List.
    /// </summary>
    public DoublyLinkedListNode<T>? Previous { get; set; }

    #endregion

    #region constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="DoublyLinkedListNode{T}" /> class.
    /// </summary>
    /// <param name="data">Data to be stored in this node.</param>
    public DoublyLinkedListNode(T data)
    {
        Data = data;
    }

    #endregion
}