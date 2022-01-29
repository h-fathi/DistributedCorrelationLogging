# DistributedCorrelationLogging
Distributed correlation logging in multiple microservices by serilog


How to use:
   
  1- Add `UseSerilog(SeriLogger.Configure)` in program.cs
  
  2- Register `ICorrelationIdAccessor` in startup like :
      
      services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();
      
      services.AddTransient<LoggingDelegatingHandler>();
      
      
Enjoy!
