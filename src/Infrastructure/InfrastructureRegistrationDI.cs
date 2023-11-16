using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LMS.Application.Persistence; // Update with your app's namespace
using CourseRepository;
using Infrastructure.Repositories;


namespace Infrastructure
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastrutureToDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<GlobalLMSContext>(
                options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString
                            ("GlobalLMSConnection"),
                        builder =>
                            builder.MigrationsAssembly(
                                typeof(GlobalLMSContext)
                                    .Assembly.FullName)));
            
            services.AddScoped
            (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            
            services.AddScoped<
                ICourseRepository, global::Infrastructure.Repositories.CourseRepository>(); 
            
            services.AddScoped<
                ILectureRepository, global::Infrastructure.Repositories.LectureRepository>(); 
            
            services.AddScoped<
                IEnrollmentRepository, global::Infrastructure.Repositories.EnrollmentRepository>(); 


            return services;
        }
    }
}