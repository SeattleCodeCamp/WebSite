
namespace OCC.Service.Webhost.Tests.Helpers
{
    public static class InMemoryOCCDBExtensions
    {
        public static InMemoryOCCDB WithEvent(this InMemoryOCCDB context, string name)
        {
            DataHelper.InsertEvent(context, name);
            context.SaveChanges();
            return context;
        }

        public static InMemoryOCCDB WithPerson(this InMemoryOCCDB context, string firstName, string lastName)
        {
            DataHelper.InsertPerson(context, firstName, lastName);
            context.SaveChanges();
            return context;
        }

        public static InMemoryOCCDB WithTrack(this InMemoryOCCDB context, string name = "Test Track", string description = "Test Description")
        {
            DataHelper.InsertTrack(context, name, description);
            context.SaveChanges();
            return context;
        }

        public static InMemoryOCCDB WithTask(this InMemoryOCCDB context, string description = "Test Description")
        {
            DataHelper.InsertTask(context, description);
            context.SaveChanges();
            return context;
        }
    }
}
