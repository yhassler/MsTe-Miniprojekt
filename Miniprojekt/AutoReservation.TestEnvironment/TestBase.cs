using AutoReservation.Dal;

namespace AutoReservation.TestEnvironment
{
    public abstract class TestBase
    {
        private static bool firstTestInExecution = true;
        private static object lockObject = new object();

        /// <summary>
        /// Constructor, runs before every run of a test method.
        /// Initializes Test data for each test run.
        /// </summary>
        protected TestBase()
        {
            InitializeTestEnvironment();
        }

        /// <summary>
        /// This method initializes the test environment including the database.
        /// </summary>
        private void InitializeTestEnvironment()
        {
            lock (lockObject)
            {
                if (firstTestInExecution)
                {
                    using (AutoReservationContext context = new AutoReservationContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        context.InitializeTestData();
                        firstTestInExecution = false;
                        return;
                    }
                }
            }

            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Database.EnsureCreated();
                context.InitializeTestData();
            }
        }
    }
}
