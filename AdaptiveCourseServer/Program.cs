using AdaptiveCourseServer.CheckSolution;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/LogicScheme/", async (Dictionary<string, List<string>> OrientedGraph) =>
{
    return CheckSolution.Solution(OrientedGraph);
}
);

app.Run();
