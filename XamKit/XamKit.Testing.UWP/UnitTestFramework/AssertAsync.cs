namespace XamKit.Testing.UWP.UnitTestFramework
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    public class AssertAsync : Assert
    {
        public static async Task ThrowsExceptionAsync<TException>(Func<Task> action, bool allowDerivedTypes = true)
            where TException : Exception
        {
            try
            {
                await action();
                Fail($"The expected '{typeof(TException).Name}' exception was not thrown.");
            }
            catch (TException ex)
            {
                if (!allowDerivedTypes && ex.GetType() != typeof(TException))
                {
                    Fail(
                        $"The exception thrown was not of the expected type. Expected: {typeof(TException).Name}. Result: {ex.GetType().Name}.");
                }
            }
        }
    }
}