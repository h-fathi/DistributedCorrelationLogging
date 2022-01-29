# DistributedCorrelationLogging
Distributed correlation logging in multiple microservices by serilog


How to use:
   
  1-add  UseSerilog(SeriLogger.Configure) in program.cs
  
  2-add register ICorrelationIdAccessor in startup like 
      
      services.AddTransient<ICorrelationIdAccessor, CorrelationIdAccessor>();
      
      services.AddTransient<LoggingDelegatingHandler>();
      
      
Enjoy!
