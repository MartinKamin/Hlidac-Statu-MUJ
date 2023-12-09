//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.1.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Ufal.UDPipe {

public class Words : global::System.IDisposable, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerable<Word>
 {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Words(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Words obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(Words obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  ~Words() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          udpipe_csharpPINVOKE.delete_Words(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public Words(global::System.Collections.IEnumerable c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (Word element in c) {
      this.Add(element);
    }
  }

  public Words(global::System.Collections.Generic.IEnumerable<Word> c) : this() {
    if (c == null)
      throw new global::System.ArgumentNullException("c");
    foreach (Word element in c) {
      this.Add(element);
    }
  }

  public bool IsFixedSize {
    get {
      return false;
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public Word this[int index]  {
    get {
      return getitem(index);
    }
    set {
      setitem(index, value);
    }
  }

  public int Capacity {
    get {
      return (int)capacity();
    }
    set {
      if (value < 0 || (uint)value < size())
        throw new global::System.ArgumentOutOfRangeException("Capacity");
      reserve((uint)value);
    }
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsSynchronized {
    get {
      return false;
    }
  }

  public void CopyTo(Word[] array)
  {
    CopyTo(0, array, 0, this.Count);
  }

  public void CopyTo(Word[] array, int arrayIndex)
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

  public void CopyTo(int index, Word[] array, int arrayIndex, int count)
  {
    if (array == null)
      throw new global::System.ArgumentNullException("array");
    if (index < 0)
      throw new global::System.ArgumentOutOfRangeException("index", "Value is less than zero");
    if (arrayIndex < 0)
      throw new global::System.ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (count < 0)
      throw new global::System.ArgumentOutOfRangeException("count", "Value is less than zero");
    if (array.Rank > 1)
      throw new global::System.ArgumentException("Multi dimensional array.", "array");
    if (index+count > this.Count || arrayIndex+count > array.Length)
      throw new global::System.ArgumentException("Number of elements to copy is too large.");
    for (int i=0; i<count; i++)
      array.SetValue(getitemcopy(index+i), arrayIndex+i);
  }

  public Word[] ToArray() {
    Word[] array = new Word[this.Count];
    this.CopyTo(array);
    return array;
  }

  global::System.Collections.Generic.IEnumerator<Word> global::System.Collections.Generic.IEnumerable<Word>.GetEnumerator() {
    return new WordsEnumerator(this);
  }

  global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() {
    return new WordsEnumerator(this);
  }

  public WordsEnumerator GetEnumerator() {
    return new WordsEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class WordsEnumerator : global::System.Collections.IEnumerator
    , global::System.Collections.Generic.IEnumerator<Word>
  {
    private Words collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public WordsEnumerator(Words collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public Word Current {
      get {
        if (currentIndex == -1)
          throw new global::System.InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new global::System.InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new global::System.InvalidOperationException("Collection modified.");
        return (Word)currentObject;
      }
    }

    // Type-unsafe IEnumerator.Current
    object global::System.Collections.IEnumerator.Current {
      get {
        return Current;
      }
    }

    public bool MoveNext() {
      int size = collectionRef.Count;
      bool moveOkay = (currentIndex+1 < size) && (size == currentSize);
      if (moveOkay) {
        currentIndex++;
        currentObject = collectionRef[currentIndex];
      } else {
        currentObject = null;
      }
      return moveOkay;
    }

    public void Reset() {
      currentIndex = -1;
      currentObject = null;
      if (collectionRef.Count != currentSize) {
        throw new global::System.InvalidOperationException("Collection modified.");
      }
    }

    public void Dispose() {
        currentIndex = -1;
        currentObject = null;
    }
  }

  public void Clear() {
    udpipe_csharpPINVOKE.Words_Clear(swigCPtr);
  }

  public void Add(Word x) {
    udpipe_csharpPINVOKE.Words_Add(swigCPtr, Word.getCPtr(x));
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = udpipe_csharpPINVOKE.Words_size(swigCPtr);
    return ret;
  }

  private uint capacity() {
    uint ret = udpipe_csharpPINVOKE.Words_capacity(swigCPtr);
    return ret;
  }

  private void reserve(uint n) {
    udpipe_csharpPINVOKE.Words_reserve(swigCPtr, n);
  }

  public Words() : this(udpipe_csharpPINVOKE.new_Words__SWIG_0(), true) {
  }

  public Words(Words other) : this(udpipe_csharpPINVOKE.new_Words__SWIG_1(Words.getCPtr(other)), true) {
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public Words(int capacity) : this(udpipe_csharpPINVOKE.new_Words__SWIG_2(capacity), true) {
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  private Word getitemcopy(int index) {
    Word ret = new Word(udpipe_csharpPINVOKE.Words_getitemcopy(swigCPtr, index), true);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private Word getitem(int index) {
    Word ret = new Word(udpipe_csharpPINVOKE.Words_getitem(swigCPtr, index), false);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, Word val) {
    udpipe_csharpPINVOKE.Words_setitem(swigCPtr, index, Word.getCPtr(val));
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(Words values) {
    udpipe_csharpPINVOKE.Words_AddRange(swigCPtr, Words.getCPtr(values));
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public Words GetRange(int index, int count) {
    global::System.IntPtr cPtr = udpipe_csharpPINVOKE.Words_GetRange(swigCPtr, index, count);
    Words ret = (cPtr == global::System.IntPtr.Zero) ? null : new Words(cPtr, true);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, Word x) {
    udpipe_csharpPINVOKE.Words_Insert(swigCPtr, index, Word.getCPtr(x));
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, Words values) {
    udpipe_csharpPINVOKE.Words_InsertRange(swigCPtr, index, Words.getCPtr(values));
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    udpipe_csharpPINVOKE.Words_RemoveAt(swigCPtr, index);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    udpipe_csharpPINVOKE.Words_RemoveRange(swigCPtr, index, count);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public static Words Repeat(Word value, int count) {
    global::System.IntPtr cPtr = udpipe_csharpPINVOKE.Words_Repeat(Word.getCPtr(value), count);
    Words ret = (cPtr == global::System.IntPtr.Zero) ? null : new Words(cPtr, true);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    udpipe_csharpPINVOKE.Words_Reverse__SWIG_0(swigCPtr);
  }

  public void Reverse(int index, int count) {
    udpipe_csharpPINVOKE.Words_Reverse__SWIG_1(swigCPtr, index, count);
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, Words values) {
    udpipe_csharpPINVOKE.Words_SetRange(swigCPtr, index, Words.getCPtr(values));
    if (udpipe_csharpPINVOKE.SWIGPendingException.Pending) throw udpipe_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
