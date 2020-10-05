using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Models;

namespace Lingkail.VMS.Data
{
    /// <summary>
    /// refer to https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-3.1&tabs=visual-studio
    /// also https://dzone.com/articles/step-by-step-aspnet-core-restful-web-service-devel
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(SenaVMSContext context)
        {
            context.Database.EnsureCreated();

            //look for trafficinfos
            if(context.TrafficInfos.Any())
            {
                return; //db has been seeded
            }

            var trafficInfos = new TrafficInfo[]
            {
                new TrafficInfo{InfoProviderID=1, Board="1",Event="1",TravelTime="32",PointA="VZ001",PointB="TIMES SQUARE", NowDateTime=DateTime.Now},
                new TrafficInfo{InfoProviderID=2, Board="1",Event="1",TravelTime="18",PointA="VZ001",PointB="MID VALLEY",NowDateTime=DateTime.Now},
                new TrafficInfo{InfoProviderID=1, Board="2",Event="1",TravelTime="42",PointA="VZ002",PointB="LOT 10", NowDateTime=DateTime.Now},
                new TrafficInfo{InfoProviderID=2, Board="2",Event="1",TravelTime="8",PointA="VZ002",PointB="FAHRENHEIT88",NowDateTime=DateTime.Now}
            };
            foreach(TrafficInfo t in trafficInfos)
            {
                context.TrafficInfos.Add(t);
            }
            context.SaveChanges();
        }
    }
}
