namespace EmployeePortal.Services
{
    public class SpinnerService
        {
            public event Action? OnShow;
            public event Action? OnHide;

            public void Show() => OnShow?.Invoke();
            public void Hide() => OnHide?.Invoke();

            public async Task RunAsync(Func<Task> action)
            {
                Show();
                try
                {
                    await action();
                }
                finally
                {
                    Hide();
                }
            }
    }
}
