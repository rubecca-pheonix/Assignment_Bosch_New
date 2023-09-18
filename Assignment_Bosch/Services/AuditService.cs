using System;
using Assignment_Bosch.Integration;
using Assignment_Bosch.Models;

namespace Assignment_Bosch.Services
{
    public class AuditService
    {
        public readonly AuthContext authContext;
        private readonly ILogger<AuditService> _logger;

        public AuditService(AuthContext _authContext, ILogger<AuditService> logger)
        {
            authContext = _authContext;
            _logger = logger;
        }

        public void SaveAudit(string correlationId, string payload, string type)
        {
            try
            {
                var request = new AuditLog
                {
                    CorrelationId = correlationId,
                    Payload = payload,
                    Type = type

                };

                authContext.AuditLogs.Add(request);
                authContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}

