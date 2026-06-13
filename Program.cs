
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<EnrollmentWorker>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
// Add services to the container.



var app = builder.Build();

builder.Host.UseDefaultServiceProvider(options =>
{
options.ValidateScopes = true;
options.ValidateOnBuild = true;
});
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
// app.MapGet("/api/error", () =>
// {
// throw newTmsDatabaseException("Simulated database failure for ProblemDetails testing");
// });
 app.MapGet("/api/assessments/results", () => Results.Ok(new {
     courseCode = "CS-101",
    studentId = "S-001",
    letterGrade = "A"
}));
// Configure the HTTP request pipeline.


// app.MapGet("/api/enrollments/worker-smoke", (EnrollmentWorker worker) =>
// {
//     return Results.Ok("processed");
// });


// app.MapControllers();


// builder.Services.AddOptions<PaymentOptions>()
// //
// .BindConfiguration("Payments")
// //
// //
// .ValidateDataAnnotations()
// .ValidateOnStart();


app.Run();
