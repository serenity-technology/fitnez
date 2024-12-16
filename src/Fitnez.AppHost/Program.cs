var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Fitnez_Web>("fitnez-web");

builder.AddProject<Projects.Fitnez_App>("fitnez-app");

builder.Build().Run();