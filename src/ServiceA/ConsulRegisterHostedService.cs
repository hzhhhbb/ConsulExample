using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;

namespace ServiceA
{
    public class ConsulRegisterHostedService : IHostedService
    {
        private readonly string ServiceId;
        private readonly IConsulClient ConsulClient;

        public ConsulRegisterHostedService(IConsulClient consulClient)
        {
            this.ServiceId = new Guid().ToString();
            this.ConsulClient = consulClient;
        }

        /// <summary>
        ///     Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var uri = new Uri("http://localhost:5000");
            var registration = new AgentServiceRegistration
            {
                ID = this.ServiceId,
                Name = "Service",
                Address = uri.Host,
                Port = uri.Port,
                Tags = new[] {"api"},
                Check = new AgentServiceCheck
                {
                    // 心跳地址
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/healthz",
                    // 超时时间
                    Timeout = TimeSpan.FromSeconds(2),
                    // 检查间隔
                    Interval = TimeSpan.FromSeconds(10)
                }
            };
            // 首先移除服务，避免重复注册
            await this.ConsulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await this.ConsulClient.Agent.ServiceRegister(registration, cancellationToken);
        }

        /// <summary>
        ///     Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this.ConsulClient.Agent.ServiceDeregister(this.ServiceId, cancellationToken);
        }
    }
}