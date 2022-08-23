namespace MVVMLibrary
{
    public static class UIContext
    {
        public static SynchronizationContext? Current { get; set; }

        public static void InitializeContext(SynchronizationContext context)
        {
            Current = context;
        }
    }
}