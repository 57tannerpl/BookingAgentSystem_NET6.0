using Microsoft.Extensions.DependencyInjection.Extensions;
using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Models;
using SettlementBookingAgent_NET6._0.API.Repositories.Settings;
using System.Xml.Serialization;

namespace SettlementBookingAgent_NET6._0.API
{
    public static class Extensions
    {
        public static BookingDto AsDto(this Booking item)
        {
            return new BookingDto()
            {
                ClientName = item.ClientName,
                BookingTime = item.BookingTime,
                Organizer = item.Organizer,
                Attendee = item.Attendee,
                PurchaseType = item.PurchaseType,
                CreatedAt = item.CreatedAt,
            };
        }


        public static SBAApiSettings XMLReadingSettings()
        {
            var file = Directory.GetCurrentDirectory() + "\\Configs\\SettlementBookingAgentAPI_Settings.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(SBAApiSettings));

            using (FileStream fileStream = new FileStream(file, FileMode.Open))
            {
                return (SBAApiSettings)serializer.Deserialize(fileStream);
            }
        }

        public static IServiceCollection Decorate<TService, TDecorator>(this IServiceCollection services)
            where TService : class
            where TDecorator : class, TService
        {
            var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
            if (descriptor == null)
            {
                throw new InvalidOperationException($"{typeof(TService).Name} is not registered in the service collection");
            }

            var objectFactory = ActivatorUtilities.CreateFactory(typeof(TDecorator), new[] { typeof(TService) });

            services.Replace(ServiceDescriptor.Describe(
                typeof(TService),
                sp => (TService)objectFactory(sp, new[] { sp.CreateInstance(descriptor) }),
                descriptor.Lifetime));

            return services;
        }

        private static object CreateInstance(this IServiceProvider provider, ServiceDescriptor descriptor)
        {
            return descriptor.ImplementationInstance ??
                   descriptor.ImplementationFactory?.Invoke(provider) ??
                   ActivatorUtilities.GetServiceOrCreateInstance(provider, descriptor.ImplementationType);
        }
    }
}
