using System;
using System.Threading;

namespace SloReviewTool.Model
{
    //
    // This class enables Thread Local Storage for C# objects
    //
    public class ThreadContext<ContextT> where ContextT : class
    {
        // Static 
        static LocalDataStoreSlot slot_;

        static ThreadContext()
        {
            slot_ = Thread.AllocateNamedDataSlot(typeof(ContextT).Name);
        }

        static public void Set(ContextT context)
        {
            Thread.SetData(slot_, context);
        }

        static public ContextT ForThread()
        {
            var context = Thread.GetData(slot_) as ContextT;
            if (context == null) throw new InvalidOperationException("ThreadContext.ForThread() called on thread without first calling Set()");

            return context;
        }

        static bool IsSet()
        {
            return (Thread.GetData(slot_) as ContextT) != null;
        }
    }
}
