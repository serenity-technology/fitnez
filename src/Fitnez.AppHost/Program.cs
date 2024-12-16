var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Fitnez_Web>("fitnez-web");

builder.Build().Run();