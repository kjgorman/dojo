﻿using System.Collections.Generic;
using Snail.Core.Shipping.Routes;

namespace Snail.Core.Ports
{
    public interface ILocationProvider
    {
        /// <summary>
        /// All the locations
        /// </summary>
        /// <returns></returns>
        IEnumerable<Location> All();

        /// <summary>
        /// The location for the specified id, or null if not found
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        Location ById(long locationId);

        /// <summary>
        /// All locations in the specified city
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        IEnumerable<Location> ByCity(string cityName);

        /// <summary>
        /// All locations within the specified port
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        IEnumerable<Location> ByPort(string portName);
    }
}
