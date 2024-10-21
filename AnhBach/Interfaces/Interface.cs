namespace AnhBach.Interfaces
{
	public interface IMyService
	{
		string GetMessage();
	}


	public class MyService : IMyService
	{
		public string GetMessage()
		{
			return "Hello from MyService!";
		}
	}

	public class MyServiceNext : IMyService
	{
		public string GetMessage()
		{
			return "Hello from MyService Next!";
		}
	}
}

