var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<EnrollmentWorker>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
// Add services to the container.

builder.Services.AddControllers();


var app = builder.Build();
app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/api/assessments/results", () => Results.Ok(new {
    courseCode = "CS-101",
    studentId = "S-001",
    letterGrade = "A"
}));
// Configure the HTTP request pipeline.
builder.Host.UseDefaultServiceProvider(options =>
{
options.ValidateScopes = true;
options.ValidateOnBuild = true;
});

app.MapGet("/api/enrollments/worker-smoke", (EnrollmentWorker worker) =>
{
    return Results.Ok("processed");
});

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();


builder.Services.AddOptions<PaymentOptions>()
//
.BindConfiguration("Payments")
//
//
.ValidateDataAnnotations()
.ValidateOnStart();



app.Run();
