var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Fitnez_Web>("web");

builder.AddProject<Projects.Fitnez_App>("app");

builder.AddProject<Projects.Fitnez_Identity>("identity");

builder.Build().Run();