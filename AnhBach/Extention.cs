using System.Reflection;

namespace AnhBach
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddMediatR(configuration =>
			{
				configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			});
			return services;
		}
	}
}
